namespace ROS.Contracts.EntityFramework
{
	public interface IRepositoryFactory
	{
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}
