namespace Caregiver.Repositories.IRepository
{
	public interface IEmailService
	{
		Task<string> SendEmail(string resetLink, string emailAddress);
	}
}
