namespace MM.Books;

internal class BookService : IBookService
{
    public List<BookDto> ListBooks()
    {
        return
        [
            new BookDto(Guid.NewGuid(), "1984", "George Orwell"),
            new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald"),
            new BookDto(Guid.NewGuid(), "The Catcher in the Rye", "J.D. Salinger")
        ];
    }
}