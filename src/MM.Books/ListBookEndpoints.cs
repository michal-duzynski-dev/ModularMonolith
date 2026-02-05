using FastEndpoints;

namespace MM.Books;

internal class ListBookEndpoints : EndpointWithoutRequest<ListBooksResponse>
{
    private readonly IBookService _bookService;
    public ListBookEndpoints(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var books = _bookService.ListBooks();
        await Send.OkAsync(new ListBooksResponse { Books = books }, cancellation: ct);
    }
}