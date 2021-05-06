using AutoMapper;
using ROS.Data.Entities;
using ROS.Services.Order.Commands.CreateOrder;

namespace ROS.Infrastructure.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Order
			CreateMap<CreateOrderRequest, OrderEntity>();

		}
	}
}
