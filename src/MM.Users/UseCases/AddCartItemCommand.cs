using Ardalis.Result;
using MediatR;

namespace MM.Users.UseCases;

public record AddCartItemCommand(string Email, Guid BookId, int Quantity) : IRequest<Result>;
