namespace MM.Books;

internal interface IBookService
{
    Task<List<BookDto>> ListBooksAsync();
    Task<BookDto> GetBookByIdAsync(Guid id);
    Task CreateBookAsync(BookDto book);
    Task UpdateBookPriceAsync(Guid id, decimal newPrice);
    Task DeleteBookAsync(Guid id);
}
