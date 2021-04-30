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
		private Dictionary<(Type type, string name), object> _repositories;

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
			return (IRepository<TEntity>)GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context));
		}

		#region Private Methods
		private object GetOrAddRepository(Type type, object repository)
		{
			_repositories ??= new Dictionary<(Type type, string Name), object>();

			if (_repositories.TryGetValue((type, repository.GetType().FullName), out repository))
			{
				return repository;
			}

			_repositories.Add((type, repository.GetType().FullName), repository);

			return repository;
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

				createdEntity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.UserId();
				createdEntity.CreatedAt = DateTime.UtcNow;
			}

			foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
			{
				if (!(entry?.Entity is IUpdatedEntity updatedEntity)) continue;

				updatedEntity.UpdatedBy = _httpContextAccessor?.HttpContext?.User?.UserId();
				updatedEntity.UpdatedAt = DateTime.UtcNow;
			}
		}
		#endregion
	}
}
