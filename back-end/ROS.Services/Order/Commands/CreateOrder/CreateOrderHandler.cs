using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Order.Commands.CreateOrder
{
	public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public CreateOrderHandler(IUnitOfWork<WriteDbContext> unitOfWork, IMapper mapper, ILogger<CreateOrderHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.OrderDetails.Length == 0)
				{
					return ApiResult.Failed(HttpCode.BadRequest);
				}

				var product = _mapper.Map<OrderEntity>(request);

				product.Id = DateTime.UtcNow.ToString("yMMddHHmmss") + "U" + request.UserId?.ToString();

				await _unitOfWork.GetRepository<OrderEntity>().InsertAsync(product, cancellationToken);
				await _unitOfWork.CommitAsync();

				return ApiResult.Succeeded();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}
	}
}
