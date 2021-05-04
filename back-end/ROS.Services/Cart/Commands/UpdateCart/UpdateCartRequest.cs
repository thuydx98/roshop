using MediatR;
using ROS.Common.ApiResponse;
using ROS.Services.Cart.Models;
using System;

namespace ROS.Services.Cart.Commands.UpdateCart
{
	public class UpdateCartRequest : IRequest<ApiResult>
	{
		public UpdateCartRequest(string userId, CartItemModel[] items)
		{
			UserId = int.Parse(userId);
			Items = items ?? Array.Empty<CartItemModel>();
		}

		public int UserId { get; set; }
		public CartItemModel[] Items { get; set; }
	}
}
