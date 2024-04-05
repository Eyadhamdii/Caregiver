namespace Caregiver.Repositories.IRepository
{
	public interface IEmailRepo
	{
		Task<string> SendEmail(string body, string header, string emailAddress);
	}
}
