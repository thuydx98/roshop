using ROS.Contracts.Entities;
using System;

namespace ROS.Data.Entities
{
	public partial class CartItemEntity : ICreatedEntity, IUpdatedEntity
	{
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public DateTime AddedAt { get; set; } = DateTime.UtcNow;
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public virtual ProductEntity Product { get; set; }
		public virtual UserEntity User { get; set; }
	}
}
