namespace Caregiver.Services.IService
{
    public interface IEmailService
    {
        Task<string> SendEmail(string body, string header, string emailAddress);
    }
}
