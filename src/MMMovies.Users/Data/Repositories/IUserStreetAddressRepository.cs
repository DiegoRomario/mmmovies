using MMMovies.Users.Domain;

namespace MMMovies.Users.Data.Repositories;

public interface IUserStreetAddressRepository
{
    Task<UserStreetAddress?> GetById(Guid userStreetAddressId);
}

