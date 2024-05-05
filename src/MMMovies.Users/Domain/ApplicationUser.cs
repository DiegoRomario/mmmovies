using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using MMMovies.SharedKernel;

namespace MMMovies.Users.Domain;

public class ApplicationUser : IdentityUser, IDomainEvents
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = [];
    private readonly List<UserStreetAddress> _addresses = [];
    private readonly List<DomainEventBase> _domainEvents = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    void IDomainEvents.ClearDomainEvents() => _domainEvents.Clear();


    public void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);

        var existingMovie = _cartItems.SingleOrDefault(c => c.MovieId == item.MovieId);
        if (existingMovie != null)
        {
            existingMovie.UpdateQuantity(existingMovie.Quantity + item.Quantity);
            existingMovie.UpdateDescription(item.Description);
            existingMovie.UpdateUnitPrice(item.UnitPrice);
            return;
        }
        _cartItems.Add(item);
    }

    internal UserStreetAddress AddAddress(Address address)
    {
        Guard.Against.Null(address);

        // find existing address and just return it
        var existingAddress = _addresses.SingleOrDefault(a => a.StreetAddress == address);
        if (existingAddress != null)
        {
            return existingAddress;
        }

        var newAddress = new UserStreetAddress(Id, address);
        _addresses.Add(newAddress);


        var domainEvent = new AddressAddedEvent(newAddress);
        RegisterDomainEvent(domainEvent);

        return newAddress;
    }
    internal void ClearCart()
    {
        _cartItems.Clear();
    }
}

