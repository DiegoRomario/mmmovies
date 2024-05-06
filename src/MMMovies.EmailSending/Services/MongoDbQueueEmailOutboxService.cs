using MMMovies.EmailSending.Domain;
using MongoDB.Driver;

namespace MMMovies.EmailSending.Services;

internal class MongoDbQueueEmailOutboxService(IMongoCollection<EmailOutbox> emailCollection) : IQueueEmailsInOutboxService
{
    private readonly IMongoCollection<EmailOutbox> _emailCollection = emailCollection;

    public async Task QueueEmailForSending(EmailOutbox entity)
    {
        await _emailCollection.InsertOneAsync(entity);
    }
}
