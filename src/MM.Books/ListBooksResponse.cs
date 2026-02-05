namespace MM.Books;

public record ListBooksResponse
{
    public required List<BookDto> Books { get; set; }
}