using Microsoft.EntityFrameworkCore;
using MM.Users.Domain;

namespace MM.Users.Data;

public class EfApplicationUserRepository : IApplicationUserRepository
{
  private readonly UsersDbContext _dbContext;
  public EfApplicationUserRepository(UsersDbContext context)
  {
    _dbContext = context;
  }
  public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
  {
    return _dbContext.ApplicationUsers
      .Include(user => user.CartItems)
      .SingleAsync(user => user.Email == email);
  }

  public Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
  {
    return _dbContext.ApplicationUsers
      .Include(user => user.Addresses)
      .SingleAsync(user => user.Email == email);
  }

  public Task SaveChangesAsync()
  {
    return _dbContext.SaveChangesAsync();
  }
}
