namespace MM.Books;

internal class BookService : IBookService
{
  private readonly IBookRepository _bookRepository;

  public BookService(IBookRepository bookRepository)
  {
    _bookRepository = bookRepository;
  }
    public async Task<List<BookDto>> ListBooksAsync()
    {
      var books = (await _bookRepository.ListAsync())
        .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price))
        .ToList();
      
        return books;
        // return
        // [
        //     new BookDto(Guid.NewGuid(), "1984", "George Orwell"),
        //     new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald"),
        //     new BookDto(Guid.NewGuid(), "The Catcher in the Rye", "J.D. Salinger")
        // ];
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
      var book = await _bookRepository.GetByIdAsync(id);
      return new BookDto(book!.Id, book!.Title, book!.Author, book!.Price);
    }

    public async Task CreateBookAsync(BookDto book)
    {
      var newBook = new Book(book.Id, book.Title, book.Author, book.Price);
      await _bookRepository.AddAsync(newBook);
      await _bookRepository.SaveChangesAsync();
    }

    public async Task UpdateBookPriceAsync(Guid id, decimal newPrice)
    {
      var book = await _bookRepository.GetByIdAsync(id);
      book!.UpdatePrice(newPrice);
      await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
      var book = await _bookRepository.GetByIdAsync(id);
      if (book is not null)
      {
        await _bookRepository.DeleteAsync(book);
        await _bookRepository.SaveChangesAsync();
      }
    }
}
