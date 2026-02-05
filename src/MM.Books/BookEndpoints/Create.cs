using FastEndpoints;

namespace MM.Books.Endpoints;

internal class Create : Endpoint<CreateBookRequest, BookDto>
{
  private readonly IBookService _bookService;
  public Create(IBookService bookService)
  {
    _bookService = bookService;
  }

  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct = default)
  {
    var bookDto = new BookDto(req.Id, req.Title, req.Author, req.Price);
    await _bookService.CreateBookAsync(bookDto);
    await Send.CreatedAtAsync<GetById>(
      new { bookDto.Id }, bookDto, cancellation: ct);
  }


}
