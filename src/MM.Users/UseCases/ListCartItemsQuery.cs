using Ardalis.Result;
using MediatR;
using MM.Users.CartEndpoints;

namespace MM.Users.UseCases;

public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;
