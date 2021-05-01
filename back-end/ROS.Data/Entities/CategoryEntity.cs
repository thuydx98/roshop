using ROS.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ROS.Data.Entities
{
	public partial class CategoryEntity : IBaseEntity<int>, ICreatedEntity, IUpdatedEntity
	{
		public CategoryEntity()
		{
			ProductCategories = new HashSet<ProductCategoryEntity>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public ICollection<ProductCategoryEntity> ProductCategories { get; set; }
	}
}
