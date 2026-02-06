using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace MM.Users;

public class ApplicationUser : IdentityUser
{
  public string FullName { get; set; } = string.Empty;
  private readonly List<CartItem> _cartItems = new();
  
  public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();
  
  public void AddCartItem(CartItem cartItem)
  {
    Guard.Against.Null(cartItem);
    var existingBook = _cartItems.SingleOrDefault(b => b.BookId == cartItem.BookId);
    if (existingBook != null)
    {
      existingBook.UpdateDescription(cartItem.Description);
      existingBook.UpdateUnitPrice(cartItem.UnitPrice);
      existingBook.UpdateQuantity(existingBook.Quantity + cartItem.Quantity);
      return;
    }
    
    _cartItems.Add(cartItem);
  }
}

public class CartItem
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public Guid BookId { get; private set; }
  public string Description { get; private set; } = string.Empty;
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
  {
    BookId = Guard.Against.Default(bookId);
    Description = Guard.Against.NullOrEmpty(description);
    Quantity = Guard.Against.Negative(quantity);
    UnitPrice = Guard.Against.Negative(unitPrice);
  }

  public CartItem()
  {
    
  }

  internal void UpdateQuantity(int quantity)
  {
    Quantity = Guard.Against.Negative(quantity);
  }

  public void UpdateDescription(string cartItemDescription)
  {
    Description = Guard.Against.NullOrEmpty(cartItemDescription);
  }

  public void UpdateUnitPrice(decimal cartItemUnitPrice)
  {
    UnitPrice = Guard.Against.Negative(cartItemUnitPrice);
  }
}
