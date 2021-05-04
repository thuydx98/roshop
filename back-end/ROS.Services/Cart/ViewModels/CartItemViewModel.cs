using ROS.Data.Entities;
using ROS.Services.Product.ViewModels;
using System;

namespace ROS.Services.Cart.ViewModels
{
	public class CartItemViewModel
	{
		public CartItemViewModel(CartItemEntity cartItem)
		{
			ProductId = cartItem.ProductId;
			Quantity = cartItem.Quantity;
			AddedAt = cartItem.AddedAt;
			Product = cartItem.Product != null ? new ProductViewModel(cartItem.Product) : null;
		}

		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public DateTime AddedAt { get; set; }
		public ProductViewModel Product { get; set; }
	}
}
