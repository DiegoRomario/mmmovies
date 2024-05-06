using Ardalis.Result;
using MMMovies.EmailSending.Domain;
using MongoDB.Driver;

namespace MMMovies.EmailSending.Services;

internal class MongoDbGetEmailsFromOutboxService(IMongoCollection<EmailOutbox> emailCollection) : IGetEmailsFromOutboxService
{
    private readonly IMongoCollection<EmailOutbox> _emailCollection = emailCollection;

    public async Task<Result<EmailOutbox>> GetUnprocessedEmailEntity()
    {
        var filter = Builders<EmailOutbox>.Filter.Eq(entity =>
                entity.DateTimeUtcProcessed, null);
        var unsentEmailEntity = await _emailCollection.Find(filter)
                                                      .FirstOrDefaultAsync();

        if (unsentEmailEntity == null) return Result.NotFound();

        return unsentEmailEntity;
    }
}
