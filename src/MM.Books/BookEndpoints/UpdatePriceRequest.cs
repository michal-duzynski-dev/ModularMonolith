namespace MM.Books.Endpoints;

public class UpdatePriceRequest
{
  public Guid Id { get; set; }
  public decimal Price { get; set; }
}
