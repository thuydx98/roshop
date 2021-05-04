using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Cart.ViewModels;
using ROS.Services.Product.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Cart.Queries.GetCart
{
	public class GetCartHandler : IRequestHandler<GetCartRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public GetCartHandler(IUnitOfWork<ReadDbContext> unitOfWork, ILogger<GetCartHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(GetCartRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var cart = await _unitOfWork.GetRepository<CartItemEntity>().GetListAsync(
					selector: n => new CartItemViewModel(n),
					predicate: n => n.UserId == request.UserId,
					orderBy: n => n.OrderByDescending(o => o.AddedAt),
					include: n => n.Include(i => i.Product),
					cancellationToken: cancellationToken);

				return ApiResult.Succeeded(cart);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}
	}
}
