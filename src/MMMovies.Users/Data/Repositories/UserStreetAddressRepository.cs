using Microsoft.EntityFrameworkCore;
using MMMovies.Users.Domain;

namespace MMMovies.Users.Data.Repositories;

internal class UserStreetAddressRepository(UsersDbContext _dbContext) : IUserStreetAddressRepository
{
    private readonly UsersDbContext _dbContext = _dbContext;

    public Task<UserStreetAddress?> GetById(Guid userStreetAddressId)
    {
        return _dbContext.UserStreetAddresses.SingleOrDefaultAsync(a => a.Id == userStreetAddressId);
    }
}
