using MMMovies.SharedKernel;

namespace MMMovies.Users.Domain;

internal sealed class AddressAddedEvent(UserStreetAddress newAddress) : DomainEventBase
{
    public UserStreetAddress NewAddress { get; } = newAddress;
}
