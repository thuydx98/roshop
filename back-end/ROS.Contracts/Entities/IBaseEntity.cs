using System.ComponentModel.DataAnnotations;

namespace ROS.Contracts.Entities
{
	public interface IBaseEntity<TKey>
	{
		[Key]
		public TKey Id { get; set; }
	}
}
