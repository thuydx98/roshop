using ROS.Contracts.Entities;
using ROS.Contracts.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ROS.Common.Extensions;

namespace ROS.EntityFramework
{
	public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
	{
		public TContext Context { get; }

		private readonly IHttpContextAccessor _httpContextAccessor;
		private Dictionary<Type, object> _repositories;

		public UnitOfWork(TContext context, IHttpContextAccessor httpContextAccessor = null)
		{
			Context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public int Commit()
		{
			TrackChanges();
			return Context.SaveChanges();
		}

		public async Task<int> CommitAsync()
		{
			TrackChanges();
			return await Context.SaveChangesAsync();
		}

		public void Dispose()
		{
			Context?.Dispose();
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			_repositories ??= new Dictionary<Type, object>();

			if (_repositories.TryGetValue(typeof(TEntity), out object repository))
			{
				return (IRepository<TEntity>)repository;
			}

			repository = new Repository<TEntity>(Context);

			_repositories.Add(typeof(TEntity), repository);

			return (IRepository<TEntity>)repository;
		}

		private void TrackChanges()
		{
			var validationErrors = Context.ChangeTracker
				.Entries<IValidatableObject>()
				.SelectMany(e => e.Entity.Validate(null))
				.Where(r => r != ValidationResult.Success)
				.ToArray();

			if (validationErrors.Any())
			{
				var exceptionMessage = string.Join(Environment.NewLine, validationErrors.Select(error =>
					$"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
				throw new Exception(exceptionMessage);
			}

			foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
			{
				if (!(entry?.Entity is ICreatedEntity createdEntity)) continue;

				createdEntity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.FindFirst("sub")?.Value;
				createdEntity.CreatedAt = DateTime.UtcNow;
			}

			foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
			{
				if (!(entry?.Entity is IUpdatedEntity updatedEntity)) continue;

				updatedEntity.UpdatedBy = _httpContextAccessor?.HttpContext?.User?.FindFirst("sub")?.Value;
				updatedEntity.UpdatedAt = DateTime.UtcNow;
			}
		}
	}
}
