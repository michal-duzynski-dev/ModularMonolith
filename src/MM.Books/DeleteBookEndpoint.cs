using FastEndpoints;

namespace MM.Books;

internal class DeleteBookEndpoint : Endpoint<DeleteBookRequest>
{
  private readonly IBookService _bookService;
  public DeleteBookEndpoint(IBookService bookService)
  {
    _bookService = bookService;
  }

  public override void Configure()
  {
    Delete("/books/{id}");
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct = default)
  {
    await _bookService.DeleteBookAsync(req.Id);
    await Send.NoContentAsync(ct);
  }


}
