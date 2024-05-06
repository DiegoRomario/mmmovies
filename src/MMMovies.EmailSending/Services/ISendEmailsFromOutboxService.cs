namespace MMMovies.EmailSending.Services;

internal interface ISendEmailsFromOutboxService
{
    Task CheckForAndSendEmails();
}
