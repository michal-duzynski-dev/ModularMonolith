using FastEndpoints;

namespace MM.Books.Endpoints;

internal class UpdatePrice : Endpoint<UpdatePriceRequest, BookDto>
{
  private readonly IBookService _bookService;
  public UpdatePrice(IBookService bookService)
  {
    _bookService = bookService;
  }

  // call to a collection of prices
  public override void Configure()
  {
    Post("/books/{id}/pricehistory");
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(UpdatePriceRequest req, CancellationToken ct = default)
  {
    await _bookService.UpdateBookPriceAsync(req.Id, req.Price);
    
    var updatedBook = await _bookService.GetBookByIdAsync(req.Id);
    await Send.OkAsync(updatedBook);
  }


}
