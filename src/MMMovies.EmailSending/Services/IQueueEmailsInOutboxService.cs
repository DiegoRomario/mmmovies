using MMMovies.EmailSending.Domain;

namespace MMMovies.EmailSending.Services;

internal interface IQueueEmailsInOutboxService
{
    Task QueueEmailForSending(EmailOutbox entity);
}
