using Microsoft.EntityFrameworkCore;

namespace MM.Books;

internal class EfBookRepository : IBookRepository
{
  private readonly BookDbContext _dbContext;

  public EfBookRepository(BookDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public async Task<Book?> GetByIdAsync(Guid id)
  {
    var book = await _dbContext.Books.FindAsync(id);
    return  book;
  }

  public async Task<List<Book>> ListAsync()
  {
    var books = await _dbContext.Books.ToListAsync();
    return books;
  }

  public Task AddAsync(Book book)
  {
     _dbContext.Books.Add(book);
     return Task.CompletedTask;
  }

  public Task DeleteAsync(Book book)
  {
    _dbContext.Books.Remove(book);
    return Task.CompletedTask;
  }

  public Task SaveChangesAsync()
  {
    _dbContext.SaveChangesAsync();
    return Task.CompletedTask;
  }
}
