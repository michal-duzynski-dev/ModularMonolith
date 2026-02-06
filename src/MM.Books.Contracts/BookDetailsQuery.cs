using Ardalis.Result;
using MediatR;

namespace MM.Books.Contracts;

public record BookDetailsQuery(Guid BookId) : IRequest<Result<BookDetailsResponse>>;

