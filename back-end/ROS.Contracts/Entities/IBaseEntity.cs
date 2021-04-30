namespace ROS.Contracts.Entities
{
	public interface IBaseEntity<TKey>
	{
		public TKey Id { get; set; }
	}
}
