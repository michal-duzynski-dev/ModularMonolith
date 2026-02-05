using FastEndpoints;

namespace MM.Books;

internal class CreateBookEndpoint : Endpoint<CreateBookRequest, BookDto>
{
  private readonly IBookService _bookService;
  public CreateBookEndpoint(IBookService bookService)
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
    await Send.CreatedAtAsync<GetBookByIdEndpoint>(
      new { bookDto.Id }, bookDto, cancellation: ct);
  }


}
