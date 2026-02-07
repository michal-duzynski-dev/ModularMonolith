using MM.Users.Domain;

namespace MM.Users.Data;

public interface IApplicationUserRepository
{
  Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
  Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
  Task SaveChangesAsync();
}
