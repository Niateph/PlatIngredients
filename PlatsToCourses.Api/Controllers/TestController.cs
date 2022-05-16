using CommonPackages.Mail.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlatsToCourses.Api.Options;

namespace PlatsToCourses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
	private readonly IMailService mailService;
	private readonly IOptions<ApplicationOptions> applicationOptions;

	public TestController(IMailService mailService, IOptions<ApplicationOptions> mailOptions)
	{
		this.mailService = mailService;
		this.applicationOptions = mailOptions;
	}

	/// <summary>
	/// Envoie un mail.
	/// </summary>
	[HttpPost]
	public ActionResult SendMail()
	{
		this.mailService.SendHtml(this.applicationOptions.Value.MailServiceDev, "s.duffaud@cargo-services.fr", "Test envoie mail", "Test", this.applicationOptions.Value.SmtpServer);
		return this.Ok();
	}
}
