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

namespace ROS.Services.Product.Queries.GetProduct
{
	public class GetProductHandler : IRequestHandler<GetProductRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public GetProductHandler(IUnitOfWork<ReadDbContext> unitOfWork, ILogger<GetProductHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(GetProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var product = await _unitOfWork.GetRepository<ProductEntity>().SingleOrDefaultAsync(
						selector: n => new ProductViewModel(n),
						predicate: n => n.Id == request.Id && !n.Hidden && !n.Deleted,
						cancellationToken: cancellationToken);

				if (product == null)
				{
					return ApiResult.Failed(HttpCode.Notfound);
				}

				return ApiResult.Succeeded(product);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}
	}
}
