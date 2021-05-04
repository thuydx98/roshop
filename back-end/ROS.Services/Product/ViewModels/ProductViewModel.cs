using ROS.Data.Entities;
using System;

namespace ROS.Services.Product.ViewModels
{
	public class ProductViewModel
	{
		public ProductViewModel()
		{
		}

		public ProductViewModel(ProductEntity product)
		{
			Id = product.Id;
			Name = product.Name;
			Code = product.Code;
			AvatarUrl = product.AvatarUrl;
			Price = product.Price;
			Rate = product.Rate;
			CreatedAt = product.CreatedAt;
		}

		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string AvatarUrl { get; set; }
		public decimal? Rate { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
}
