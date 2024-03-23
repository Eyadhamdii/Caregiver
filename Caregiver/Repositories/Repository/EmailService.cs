using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using System.Net;
using MailKit.Net.Smtp;
using System.Security.Policy;

namespace Caregiver.Repositories.Repository
{
	public class EmailService : IEmailService
	{
		public async Task<string> SendEmail(string token, string emailAddress)
		{
			try
			{

				var email = new MimeMessage();
				email.From.Add(new MailboxAddress("Caregiver Website", "emykhodary2019@gmail.com"));
				email.To.Add(MailboxAddress.Parse(emailAddress));
				email.Subject = "Your Reset Password Link";
				//string resetUrl = $"http://localhost:5248/api/Auth/UpdatePassword?email={Uri.EscapeDataString(emailAddress)}&token={Uri.EscapeDataString(token)}";


				//email.Body = new TextPart(TextFormat.Html) { Text = $"<h3> Click on the link and will direct you to the page to enter a new password<h3/>  <a{resetUrl}>Click Here<a/>" };
				string resetUrl = $"http://localhost:5248/api/Auth/UpdatePassword?email={Uri.EscapeDataString(emailAddress)}&token={Uri.EscapeDataString(token)}";

				email.Body = new TextPart(TextFormat.Html) { Text = $"<h3> Click on the link and will direct you to the page to enter a new password</h3>  <a href=\"{resetUrl}\">Click Here</a>" };


				using var smtp = new SmtpClient();
				//can't in production
				ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
				//smtp.gmail.com
				smtp.Connect("smtp.gmail.com", 587);
				smtp.Authenticate("emykhodary2019@gmail.com", "wqdw wtdc vwct qvbj");
				await smtp.SendAsync(email);
				smtp.Disconnect(true);
				return "Success";
			}
			catch (Exception ex)
			{
				return "Failed";
			}




		}
	}
}
