using MediatR;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Product.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Product.Queries.GetProductByIds
{
	public class GetProductByIdsHandler : IRequestHandler<GetProductByIdsRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public GetProductByIdsHandler(IUnitOfWork<ReadDbContext> unitOfWork, ILogger<GetProductByIdsHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(GetProductByIdsRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var products = await _unitOfWork.GetRepository<ProductEntity>().GetListAsync(
					selector: n => new ProductViewModel(n),
					predicate: n => request.Ids.Contains(n.Id.ToString()),
					cancellationToken: cancellationToken);

				return ApiResult.Succeeded(products);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}
	}
}
