using ROS.Common.Enums;
using ROS.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ROS.Data.Entities
{
	public partial class ProductEntity : IBaseEntity<int>, ICreatedEntity, IUpdatedEntity
	{
		public ProductEntity()
		{
			ProductCategories = new HashSet<ProductCategoryEntity>();
			Votes = new HashSet<ProductVoteEntity>();
			Photos = new HashSet<ProductPhotoEntity>();
			OrderDetails = new HashSet<OrderDetailEntity>();
			CartItems = new HashSet<CartItemEntity>();
		}

		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string AvatarUrl { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public decimal? Rate { get; set; }
		public ProductTypes Type { get; set; } = ProductTypes.NEW;
		public int? CategoryId { get; set; }
		public int? BrandId { get; set; }
		public bool Hidden { get; set; }
		public bool Deleted { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public virtual BrandEntity Brand { get; set; }
		public virtual ICollection<ProductCategoryEntity> ProductCategories { get; set; }
		public virtual ICollection<ProductVoteEntity> Votes { get; set; }
		public virtual ICollection<ProductPhotoEntity> Photos { get; set; }
		public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
		public virtual ICollection<CartItemEntity> CartItems { get; set; }
	}
}
