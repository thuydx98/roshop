using ROS.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ROS.Data.Entities
{
	public partial class BrandEntity : IBaseEntity<int>, ICreatedEntity, IUpdatedEntity
	{
		public BrandEntity()
		{
			Products = new HashSet<ProductEntity>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public ICollection<ProductEntity> Products { get; set; }
	}
}
