namespace Caregiver.Repositories.IRepository
{
	public interface IEmailRepo
	{
		Task<string> SendEmail(string resetLink, string header, string emailAddress);
	}
}
