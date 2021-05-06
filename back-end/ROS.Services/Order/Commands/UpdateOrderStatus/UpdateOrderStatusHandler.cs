using MediatR;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Common.Enums;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Order.Commands.UpdateOrderStatus
{
	public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public UpdateOrderStatusHandler(IUnitOfWork<WriteDbContext> unitOfWork, ILogger<UpdateOrderStatusHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(UpdateOrderStatusRequest request, CancellationToken cancellationToken)
		{
            try
            {
                var timeNow = DateTime.Now;
                var order = await _unitOfWork.GetRepository<OrderEntity>().SingleOrDefaultAsync(
                    predicate: n => n.Id == request.OrderId,
                    asNoTracking: false,
                    cancellationToken: cancellationToken);

                if (order == null)
                {
                    return ApiResult.Failed(HttpCode.Notfound);
                }

                switch (request.Status)
                {
                    case OrderStatus.SHIPPING:
                        order.ShippedTime = timeNow;
                        break;

                    case OrderStatus.COMPLETED:
                        order.ShippedTime ??= timeNow;
                        order.CompletedTime = timeNow;
                        break;

                    case OrderStatus.CANCELED:
                        order.CanceledTime = timeNow;
                        break;
                }

                _unitOfWork.GetRepository<OrderEntity>().Update(order);
                await _unitOfWork.CommitAsync();

                return ApiResult.Succeeded(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ApiResult.Failed(HttpCode.InternalServerError);
            }
        }
	}
}
