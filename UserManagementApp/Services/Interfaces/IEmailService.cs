namespace UserManagementApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string recipientEmail, string subject, string body);
    }
}
