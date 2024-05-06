using Ardalis.Result;
using MMMovies.EmailSending.Domain;

namespace MMMovies.EmailSending.Services;

internal interface IGetEmailsFromOutboxService
{
    Task<Result<EmailOutbox>> GetUnprocessedEmailEntity();
}
