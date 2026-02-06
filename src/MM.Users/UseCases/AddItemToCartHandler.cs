using Ardalis.Result;
using MediatR;
using MM.Users.Data;

namespace MM.Users.UseCases;

public class AddItemToCartHandler : IRequestHandler<AddCartItemCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository;
  
  public AddItemToCartHandler(IApplicationUserRepository applicationUserRepository)
  {
    _userRepository = applicationUserRepository;
  }
  public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.Email);
    if(user == null)
    {
      return Result.Unauthorized();
    }
    var newCartItem = new CartItem(request.BookId, "desc", request.Quantity, 1.00m);
    
    user.AddCartItem(newCartItem);
    await _userRepository.SaveChangesAsync();

    return Result.Success();
  }
}
