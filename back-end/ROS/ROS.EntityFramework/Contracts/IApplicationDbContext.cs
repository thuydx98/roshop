using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ROS.EntityFramework.Contracts
{
	public interface IApplicationDbContext
	{
		ChangeTracker ChangeTracker { get; }
		IModel Model { get; }
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}
