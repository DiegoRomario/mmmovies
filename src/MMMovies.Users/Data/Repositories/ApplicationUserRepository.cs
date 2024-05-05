using Microsoft.EntityFrameworkCore;
using MMMovies.Users.Domain;

namespace MMMovies.Users.Data.Repositories;

internal class ApplicationUserRepository(UsersDbContext dbContext) : IApplicationUserRepository
{
    private readonly UsersDbContext _dbContext = dbContext;

    public Task<ApplicationUser> GetUserByIdAsync(Guid userId)
    {
        return _dbContext.ApplicationUsers.SingleAsync(user => user.Id == userId.ToString());
    }

    public Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
    {
        return _dbContext.ApplicationUsers
                         .Include(user => user.Addresses)
                         .SingleAsync(user => user.Email == email);

    }

    public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return _dbContext.ApplicationUsers
                         .Include(user => user.CartItems)
                         .SingleAsync(user => user.Email == email);
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
