using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using MM.OrderProcessing.UseCases;

namespace MM.OrderProcessing.Endpoints;

internal class ListOrdersForUser : EndpointWithoutRequest<ListOrdersForUserResponse>
{
  private readonly IMediator _mediator;

  public ListOrdersForUser(IMediator mediator)
  {
    _mediator = mediator;
  }
  
  public override void Configure()
  {
    Get("/orders");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var email = User.FindFirstValue("EmailAddress");
    var query = new ListOrdersForUserQuery(emailAddress:email!);
    var result = await _mediator.Send(query, ct);
    
    if(result.Status == ResultStatus.Unauthorized)
      await Send.UnauthorizedAsync(ct);
    else
    {
      var resp = new ListOrdersForUserResponse
      {
        Orders = result.Value.Select(o => new OrderSummary
        {
          OrderId = o.OrderId,
          UserId = o.UserId,
          DateCreated = o.DateCreated,
          DateShipped = o.DateShipped,
          Total = o.Total
        }).ToList()
      };
      
      await Send.OkAsync(resp, ct);
    }
  }
}
