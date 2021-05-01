using ROS.Contracts.Entities;
using System;

namespace ROS.Data.Entities
{
	public partial class OrderDetailEntity: ICreatedEntity
	{
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }

		public virtual OrderEntity Order { get; set; }
		public virtual ProductEntity Product { get; set; }
	}
}
