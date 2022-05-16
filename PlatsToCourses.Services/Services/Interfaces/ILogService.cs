namespace PlatsToCourses.Services;

public interface ILogService
{
	void LogInformation(string message);

	void LogError(string message);

	void LogError(Exception exception, string message = null);
}
