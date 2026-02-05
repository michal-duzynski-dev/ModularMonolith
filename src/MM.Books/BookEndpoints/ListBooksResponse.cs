namespace MM.Books.Endpoints;

public record ListBooksResponse
{
    public required List<BookDto> Books { get; set; }
}
