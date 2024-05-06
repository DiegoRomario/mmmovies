using MMMovies.EmailSending.Domain;

namespace MMMovies.EmailSending.Endpoints.Responses;

public class GetEmailsResponse
{
    public int Count { get; set; }
    public List<EmailOutbox> Emails { get; internal set; } = new();
}
