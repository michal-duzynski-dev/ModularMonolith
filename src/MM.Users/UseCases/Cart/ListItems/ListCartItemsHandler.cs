using Ardalis.Result;
using MediatR;
using MM.Users.CartEndpoints;
using MM.Users.Data;

namespace MM.Users.UseCases;

public class ListCartItemsHandler : IRequestHandler<ListCartItemsQuery,Result<List<CartItemDto>>>
{
  private readonly IApplicationUserRepository _userRepository;
  
  public ListCartItemsHandler(IApplicationUserRepository applicationUserRepository)
  {
    _userRepository = applicationUserRepository;
  }
  public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    
    if(user == null)
    {
      return Result.Unauthorized();
    }
    
    var cartItems = user.CartItems.Select(item => 
      new CartItemDto(item.Id, item.BookId, item.Description, item.Quantity, item.UnitPrice)).ToList();

    return Result.Success(cartItems); 
  }
}
