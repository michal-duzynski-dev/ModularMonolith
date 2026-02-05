using FastEndpoints;

namespace MM.Books.Endpoints;

internal class List : EndpointWithoutRequest<ListBooksResponse>
{
    private readonly IBookService _bookService;
    public List(IBookService bookService)
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
        var books = await _bookService.ListBooksAsync();
        await Send.OkAsync(new ListBooksResponse { Books = books }, cancellation: ct);
    }
}
