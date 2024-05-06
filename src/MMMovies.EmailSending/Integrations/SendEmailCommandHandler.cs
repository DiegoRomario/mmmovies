using Ardalis.Result;
using MMMovies.EmailSending.Contracts;
using MMMovies.EmailSending.Services;

namespace MMMovies.EmailSending.Integrations;
internal class SendEmailCommandHandler(ISendEmail emailSender) //:  IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly ISendEmail _emailSender = emailSender;

    public async Task<Result<Guid>> HandleAsync(SendEmailCommand request, CancellationToken ct)
    {
        await _emailSender.SendEmailAsync(request.To,
          request.From,
          request.Subject,
          request.Body);

        return Guid.Empty;
    }
}
