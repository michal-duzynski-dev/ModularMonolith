using Ardalis.Result;
using MediatR;
using MM.OrderProcessing.Data;
using MM.OrderProcessing.Endpoints;

namespace MM.OrderProcessing.UseCases;

internal class ListOrdersForUserHandler : IRequestHandler<ListOrdersForUserQuery,Result<List<OrderSummary>>>
{
  private readonly IOrderRepository _orderRepository;
  
  public ListOrdersForUserHandler(IOrderRepository orderRepository)
  {
    _orderRepository = orderRepository;
  }
  public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
  {
    var x = request.EmailAddress;
    // var user = await _orderRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    //
    // if(user == null)
    // {
    //   return Result.Unauthorized();
    // }

    var orders = await _orderRepository.ListAsync();

    var summaries = orders.Select(o => new OrderSummary
    {
      OrderId = o.Id, UserId = o.UserId, Total = o.OrderItems.Sum(oi => oi.UnitPrice),
    }).ToList();

    return Result.Success(summaries); 
  }
}
