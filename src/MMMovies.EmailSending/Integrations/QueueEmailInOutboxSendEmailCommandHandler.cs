using Ardalis.Result;
using MediatR;
using MMMovies.EmailSending.Contracts;
using MMMovies.EmailSending.Domain;
using MMMovies.EmailSending.Services;

namespace MMMovies.EmailSending.Integrations;

internal class QueueEmailInOutboxSendEmailCommandHandler(IQueueEmailsInOutboxService outboxService) : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly IQueueEmailsInOutboxService _outboxService = outboxService;

    public async Task<Result<Guid>> Handle(SendEmailCommand request,
      CancellationToken ct)
    {
        var newEntity = new EmailOutbox
        {
            Body = request.Body,
            Subject = request.Subject,
            To = request.To,
            From = request.From
        };

        await _outboxService.QueueEmailForSending(newEntity);

        return newEntity.Id;
    }
}
