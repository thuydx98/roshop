using MediatR;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Cart.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Cart.Commands.UpdateCart
{
	public class UpdateCartHandler : IRequestHandler<UpdateCartRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public UpdateCartHandler(IUnitOfWork<WriteDbContext> unitOfWork, ILogger<UpdateCartHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(UpdateCartRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var repository = _unitOfWork.GetRepository<CartItemEntity>();
				var items = await repository.GetListAsync(
					predicate: n => n.UserId == request.UserId,
					cancellationToken: cancellationToken);

				items = items.Select(n =>
				{
					n.Quantity += request.Items.Where(s => s.ProductId == n.ProductId).Sum(s => s.Quantity);
					return n;
				}).ToList();

				var deleteItems = items.Where(n => n.Quantity <= 0);
				var updateItems = items.Where(n => n.Quantity > 0).ToList();
				var newItems = request.Items
					.Where(n => !items.Select(s => s.ProductId).Contains(n.ProductId))
					.Select(n => new CartItemEntity()
					{
						ProductId = n.ProductId,
						Quantity = n.Quantity,
						UserId = request.UserId,
					}).ToList();

				repository.Update(updateItems);
				repository.Delete(deleteItems);
				await repository.InsertAsync(newItems);
				await _unitOfWork.CommitAsync();

				updateItems.AddRange(newItems);

				var result = updateItems
					.OrderByDescending(n => n.AddedAt)
					.Select(item => new CartItemViewModel(item));

				return ApiResult.Succeeded(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}
	}
}
