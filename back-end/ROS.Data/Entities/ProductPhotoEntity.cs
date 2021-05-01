using ROS.Contracts.Entities;
using System;

namespace ROS.Data.Entities
{
	public partial class ProductPhotoEntity : IBaseEntity<int>, ICreatedEntity, IUpdatedEntity
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Url { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public virtual ProductEntity Product { get; set; }
	}
}
