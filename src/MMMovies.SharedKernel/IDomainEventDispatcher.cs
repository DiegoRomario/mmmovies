namespace MMMovies.SharedKernel;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IDomainEvents> entitiesWithEvents);
}

