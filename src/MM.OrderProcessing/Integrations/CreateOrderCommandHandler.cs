using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using MM.OrderProcessing.Contracts;
using MM.OrderProcessing.Data;

namespace MM.OrderProcessing.Integrations;
internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,
  Result<OrderDetailsResponse>>
{
  private readonly IOrderRepository _orderRepository;
  private readonly ILogger<CreateOrderCommandHandler> _logger;

  public CreateOrderCommandHandler(IOrderRepository orderRepository,
    ILogger<CreateOrderCommandHandler> logger)
  {
    _orderRepository = orderRepository;
    _logger = logger;
  }

  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request,
    CancellationToken cancellationToken)
  {
    var items = request.OrderItems.Select(oi => new OrderItem(
      oi.BookId, oi.Quantity, oi.UnitPrice, oi.Description));

    var shipAddr = new Address("123 Street", "", "A", "B","12345", "C");
    var billAddr = new Address("345 Street", "", "A", "B","12345", "C");
    
    var newOrder = Order.Factory.Create(request.UserId,
      shipAddr,
      billAddr,
      items);

    await _orderRepository.AddAsync(newOrder);
    await _orderRepository.SaveChangesAsync();

    _logger.LogInformation("New Order Created! {orderId}", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
