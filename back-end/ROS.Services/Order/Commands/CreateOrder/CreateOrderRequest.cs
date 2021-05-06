using MediatR;
using ROS.Common.ApiResponse;
using System.ComponentModel.DataAnnotations;

namespace ROS.Services.Order.Commands.CreateOrder
{
	public class CreateOrderRequest : IRequest<ApiResult>
	{
		public int? UserId { get; set; }

		[Required]
		public string Receiver { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public string Address { get; set; }

		public string StoreNote { get; set; }

		[Required]
		public decimal ShippingFee { get; set; }

		[Required]
		public string ShippingService { get; set; }

		[Required]
		public decimal Distance { get; set; }

		public string ShippingNote { get; set; }

		[Required]
		public CreateOrderDetailRequest[] OrderDetails { get; set; }
	}

	public class CreateOrderDetailRequest
	{
		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Price { get; set; }

		public decimal? OriginalPrice { get; set; }
	}
}
