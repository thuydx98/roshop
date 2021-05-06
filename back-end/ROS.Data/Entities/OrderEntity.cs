using ROS.Common.Enums;
using ROS.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ROS.Data.Entities
{
	public partial class OrderEntity : IBaseEntity<string>
	{
		public OrderEntity()
		{
			Details = new HashSet<OrderDetailEntity>();
			Votes = new HashSet<ProductVoteEntity>();
		}

		public string Id { get; set; } = DateTime.UtcNow.ToString("yMMddHHmmss") + "U" + "user id";
		public int? UserId { get; set; }
		public string Receiver { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public string StoreNote { get; set; }
		public decimal ShippingFee { get; set; }
		public string ShippingService { get; set; }
		public decimal Distance { get; set; }
		public string ShippingNote { get; set; }
		public decimal Total { get; set; }
		public OrderStatus Status { get; set; } = OrderStatus.NEW;
		public DateTime OrderedTime { get; set; } = DateTime.UtcNow;
		public DateTime? ShippedTime { get; set; }
		public DateTime? CompletedTime { get; set; }
		public DateTime? CanceledTime { get; set; }

		public virtual UserEntity User { get; set; }
		public virtual ICollection<ProductVoteEntity> Votes { get; set; }
		public virtual ICollection<OrderDetailEntity> Details { get; set; }
	}
}
