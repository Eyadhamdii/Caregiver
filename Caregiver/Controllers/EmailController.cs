using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Drawing;
using System.Net;

namespace Caregiver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{

		[HttpPost]
		public async Task<IActionResult> SendEmail(string resetLink, string emailAddress)
		{

			var email = new MimeMessage();
			email.From.Add(new MailboxAddress("Caregiver Website", "emykhodary2019@gmail.com"));
			email.To.Add(MailboxAddress.Parse(emailAddress));
			email.Subject = "Your Reset Password Link";
			email.Body = new TextPart(TextFormat.Html) { Text = $"<h3> Click on the link and will direct you to the page to enter a new password<h3/>  {resetLink}" };

			using var smtp = new SmtpClient();
			//can't in production
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
			//smtp.gmail.com
			smtp.Connect("smtp.gmail.com", 587);
			smtp.Authenticate("emykhodary2019@gmail.com", "wqdw wtdc vwct qvbj");
			smtp.Send(email);
			smtp.Disconnect(true);
			return Ok();





		}
	}
}
