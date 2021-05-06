using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Order.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Order.Queries.GetOrderById
{
	public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public GetOrderByIdHandler(IUnitOfWork<ReadDbContext> unitOfWork, ILogger<GetOrderByIdHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
		{
            try
            {
                var order = await _unitOfWork.GetRepository<OrderEntity>().SingleOrDefaultAsync(
                    selector: n => new OrderViewModel(n),
                    predicate: n => n.Id == request.Id,
                    include: n => n.Include(i => i.Details),
                    cancellationToken: cancellationToken);

                if (order == null)
                {
                    return ApiResult.Failed(HttpCode.Notfound);
                }

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
