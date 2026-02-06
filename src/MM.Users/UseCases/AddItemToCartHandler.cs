using Ardalis.Result;
using MediatR;
using MM.Books.Contracts;
using MM.Users.Data;

namespace MM.Users.UseCases;

public class AddItemToCartHandler : IRequestHandler<AddCartItemCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository;
  private readonly IMediator _mediator;

  public AddItemToCartHandler(IApplicationUserRepository applicationUserRepository, IMediator mediator)
  {
    _userRepository = applicationUserRepository;
    _mediator = mediator;
  }
  public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.Email);
    if(user == null)
    {
      return Result.Unauthorized();
    }
    
    var query = new BookDetailsQuery(request.BookId);
    
    var result = await _mediator.Send(query,cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      return Result.NotFound();
    }

    var desc = $"{result.Value.Title} by {result.Value.Author}";
    var newCartItem = new CartItem(request.BookId, desc , request.Quantity, result.Value.Price);
    user.AddCartItem(newCartItem);
    await _userRepository.SaveChangesAsync();

    return Result.Success();
    }
    

  }

