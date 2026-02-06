using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using MM.Users.UseCases;

namespace MM.Users.CartEndpoints;

internal class ListItems : EndpointWithoutRequest<CartResponse>
{
  private readonly IMediator _mediator;

  public ListItems(IMediator mediator)
  {
    _mediator = mediator;
  }
  
  public override void Configure()
  {
    Get("/cart");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var email = User.FindFirstValue("EmailAddress");
    var query = new ListCartItemsQuery(email!);
    var result = await _mediator.Send(query, ct);
    
    if(result.Status == ResultStatus.Unauthorized)
      await Send.UnauthorizedAsync(ct);
    else
    {
      var resp = new CartResponse
      {
        CartItems = result.Value
      };
      
      await Send.OkAsync(resp, ct);
    }
      
  }
}
