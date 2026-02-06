using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using MM.Users.UseCases;

namespace MM.Users.CartEndpoints;

internal class AddItem : Endpoint<AddCartItemRequest>
{
  private readonly IMediator _mediator;

  public AddItem(IMediator mediator)
  {
    _mediator = mediator;
  }
  
  public override void Configure()
  {
   Post("/cart");
   Claims("EmailAddress");
  }

  public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
  {
    var email = User.FindFirstValue("EmailAddress");
    var command = new AddCartItemCommand(email!, req.BookId, req.Quantity);
    var result = await _mediator.Send(command, ct);
    if(result.Status == ResultStatus.Unauthorized)
      await Send.UnauthorizedAsync();
    else
     await Send.OkAsync();
  }
}
