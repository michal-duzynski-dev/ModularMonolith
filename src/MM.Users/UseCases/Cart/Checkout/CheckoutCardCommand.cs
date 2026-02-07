using Ardalis.Result;
using MediatR;

namespace MM.Users.UseCases.Cart.Checkout;

public record CheckoutCardCommand(string EmailAddress, Guid ShippingAddressId, Guid BillingAddressId)
  : IRequest<Result<Guid>>;
