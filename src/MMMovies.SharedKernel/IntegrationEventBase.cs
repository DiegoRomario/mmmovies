using MediatR;

namespace MMMovies.SharedKernel;

public abstract record IntegrationEventBase : INotification
{
    public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.UtcNow;
}

