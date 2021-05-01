using ROS.Common.Enums;

namespace ROS.Services.Product.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string AvatarUrl { get; set; }
		public decimal? Rate { get; set; }
	}
}
