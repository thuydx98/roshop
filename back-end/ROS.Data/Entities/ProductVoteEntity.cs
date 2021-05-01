using ROS.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ROS.Data.Entities
{
	public partial class ProductVoteEntity : IBaseEntity<int>, ICreatedEntity, IUpdatedEntity
	{
		public ProductVoteEntity()
		{
			Photos = new HashSet<ProductVotePhotoEntity>();
		}

		public int Id { get; set; }
		public int AuthorId { get; set; }
		public string OrderId { get; set; }
		public int ProductId { get; set; }
		public int Star { get; set; }
		public string Title { get; set; }
		public string Comment { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public virtual ProductEntity Product { get; set; }
		public virtual UserEntity Author { get; set; }
		public virtual OrderEntity Order { get; set; }
		public virtual ICollection<ProductVotePhotoEntity> Photos { get; set; }
	}
}
