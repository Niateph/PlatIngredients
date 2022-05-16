using Microsoft.Extensions.Logging;

namespace PlatsToCourses.Services;

/// <summary>
/// Permet de logguer à la fois dans un fichier text dans le dossier Logs de l'application ainsi que dans Azure Monitor.
/// </summary>
public class LogService : ILogService
{
	private readonly ILogger<LogService> logger;

	public LogService(ILogger<LogService> logger)
	{
		this.logger = logger;
	}

	public void LogError(string message)
	{
		this.logger.LogError(message);
	}

	public void LogError(Exception exception, string message = null)
	{
		this.logger.LogError(exception, message ?? exception.Message);
	}

	public void LogInformation(string message)
	{
		this.logger.LogInformation(message);

		//var user = this.userService.GetUser();
		//using (this.logger.BeginScope(new Dictionary<string, object> {
		//	{ "Email",user?.Email }
		//}))
		//{
		//	this.logger.LogInformation(message);
		//}
	}
}
