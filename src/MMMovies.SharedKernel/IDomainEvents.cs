namespace MMMovies.SharedKernel;

public interface IDomainEvents
{
    IEnumerable<DomainEventBase> DomainEvents { get; }
    void ClearDomainEvents();
}

