using FastEndpoints;
using MMMovies.EmailSending.Domain;
using MMMovies.EmailSending.Endpoints.Responses;
using MongoDB.Driver;

namespace MMMovies.EmailSending.Endpoints;

internal class GetEmails(IMongoCollection<EmailOutbox> emailCollection) : EndpointWithoutRequest<GetEmailsResponse>
{
    private readonly IMongoCollection<EmailOutbox> _emailCollection = emailCollection;

    public override void Configure()
    {
        Get("/emails");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
    CancellationToken ct = default)
    {
        // TODO: Implement paging
        var filter = Builders<EmailOutbox>.Filter.Empty;
        var emailEntities = await _emailCollection.Find(filter)
          .ToListAsync();

        var response = new GetEmailsResponse()
        {
            Count = emailEntities.Count,
            Emails = emailEntities // TODO: Use a separate DTO
        };

        Response = response;
    }

}
