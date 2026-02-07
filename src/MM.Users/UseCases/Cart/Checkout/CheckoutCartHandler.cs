using Ardalis.Result;
using MediatR;
using MM.OrderProcessing.Contracts;
using MM.Users.Data;

namespace MM.Users.UseCases.Cart.Checkout;

internal class CheckoutCartHandler : IRequestHandler<CheckoutCardCommand, Result<Guid>>
{
  private readonly IApplicationUserRepository _userRepository;
  private readonly IMediator _mediator;

  public CheckoutCartHandler(IApplicationUserRepository userRepository,
    IMediator mediator)
  {
    _userRepository = userRepository;
    _mediator = mediator;
  }

  public async Task<Result<Guid>> Handle(CheckoutCardCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var items = user.CartItems.Select(item =>
      new OrderItemDetails(item.BookId,
                           item.Quantity,
                           item.UnitPrice,
                           item.Description))
      .ToList();

    var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
      request.ShippingAddressId,
      request.BillingAddressId,
      items);

    var result = await _mediator.Send(createOrderCommand); // synchronous

    if (!result.IsSuccess)
    {
      return result.Map(x => x.OrderId);
    }

    user.ClearCart();
    await _userRepository.SaveChangesAsync();

    return Result.Success(result.Value.OrderId);
  }
}
