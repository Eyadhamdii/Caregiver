using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using System.Net;
using MailKit.Net.Smtp;
using System.Security.Policy;

namespace Caregiver.Repositories.Repository
{
	public class EmailRepo : IEmailRepo
	{
		public async Task<string> SendEmail(string body, string header ,string emailAddress)
		{
			try
			{

				var email = new MimeMessage(); 
				email.From.Add(new MailboxAddress("Caregiver Website", "caregiverteam23@gmail.com"));
				email.To.Add(MailboxAddress.Parse(emailAddress));
				email.Subject = header;

				//string resetUrl = $"http://localhost:5248/api/Auth/UpdatePassword?email={Uri.EscapeDataString(emailAddress)}&token={Uri.EscapeDataString(token)}";

				email.Body = new TextPart(TextFormat.Html) { Text =body};


				using var smtp = new SmtpClient();
				//can't in production
				ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
				//smtp.gmail.com
				smtp.Connect("smtp.gmail.com", 587);
				smtp.Authenticate("caregiverteam23@gmail.com", "jrzg jygk lbkv reqg"); 
				await smtp.SendAsync(email);
				smtp.Disconnect(true);
				return "Success";
			}
			catch
			{
				return "Failed";
			}




		}
	}
}
