using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace MM.Users.Domain;

public class ApplicationUser : IdentityUser
{
  public string FullName { get; set; } = string.Empty;
  private readonly List<CartItem> _cartItems = new();
  private readonly List<UserStreetAddress> _addresses = new();
  
  public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();
  public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();
  
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
  
  internal UserStreetAddress AddAddress(Address address)
  {
    Guard.Against.Null(address);

    // find existing address and just return it
    var existingAddress = _addresses.SingleOrDefault(a => a.StreetAddress == address);
    if (existingAddress != null)
    {
      return existingAddress;
    }

    var newAddress = new UserStreetAddress(Id, address);
    _addresses.Add(newAddress);

    return newAddress;
  }

  public void ClearCart()
  {
    _cartItems.Clear();
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
