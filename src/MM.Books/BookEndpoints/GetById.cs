using FastEndpoints;

namespace MM.Books.Endpoints;

internal class GetById : Endpoint<GetBookByIdRequest, BookDto>
{
  private readonly IBookService _bookService;
  public GetById(IBookService bookService)
  {
    _bookService = bookService;
  }

  public override void Configure()
  {
    Get("/books/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct = default)
  {
    var book = await _bookService.GetBookByIdAsync(req.Id);

    if (book is null)
    {
      await Send.NotFoundAsync(cancellation: ct);
      return;
    }

    await Send.ResponseAsync(book, cancellation: ct);
  }
}
