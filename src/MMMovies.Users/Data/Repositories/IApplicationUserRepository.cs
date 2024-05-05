using MMMovies.Users.Domain;

namespace MMMovies.Users.Data.Repositories;

public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
}
