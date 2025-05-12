namespace WorkerService.Abstracts
{
    public interface IEmailServiceClient
    {
        Task SendMailAsync(string emailcontent, string to);

    }
}
