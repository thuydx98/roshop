using ROS.Contracts.Entities;
using System;

namespace ROS.Data.Entities
{
	public partial class ProductCategoryEntity : ICreatedEntity, IUpdatedEntity
	{
		public int CategoryId { get; set; }
		public int ProductId { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public virtual ProductEntity Product { get; set; }
		public virtual CategoryEntity Category { get; set; }
	}
}
