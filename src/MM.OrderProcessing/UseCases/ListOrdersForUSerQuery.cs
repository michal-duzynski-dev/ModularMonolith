using Ardalis.Result;
using MediatR;
using MM.OrderProcessing.Endpoints;

namespace MM.OrderProcessing.UseCases;

public class ListOrdersForUserQuery(string emailAddress) : IRequest<Result<List<OrderSummary>>>
{
  public string EmailAddress { get; } = emailAddress;
}
