using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using PlatsToCourses.Services.Exceptions;

namespace PlatsToCourses.Api.Filters;

/// <summary>
/// This filter is triggered if an exception occurs, this filter is use to log the exception
/// and to format exception correctly for front
/// </summary>

public class ExceptionLogFilter : IExceptionFilter, IActionFilter
{
	private readonly ILogService logService;

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Constante")]
	private const string NOT_AUTHORIZED_MESSAGE = "Your profile doesn't match requirement to do this action.";
	private IDictionary<string, object> actionArguments;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logService"></param>
	public ExceptionLogFilter(ILogService logService)
	{
		this.logService = logService;
	}

	/// <summary>
	/// IExceptionFilter.OnException implementation.
	/// </summary>
	/// <param name="context"></param>
	public async void OnException(ExceptionContext context)
	{
		try
		{
			// Not authorized (by infrastructure)
			if (context.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
			{
				this.logService.LogError(context.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase);
			}

			// Not authorized (by code)
			else if (context.Exception is ForbiddenException)
			{
				var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
				responseFeature.ReasonPhrase = NOT_AUTHORIZED_MESSAGE;
				context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
				context.HttpContext.Response.ContentType = "application/json";
				string result = JsonSerializer.Serialize(new
				{
					message = NOT_AUTHORIZED_MESSAGE,
					reason = string.Empty,
					stackTrace = string.Empty
				});

				this.logService.LogError(NOT_AUTHORIZED_MESSAGE);
				context.ExceptionHandled = true;
				await context.HttpContext.Response.WriteAsync(result);
			}

			// Application exception
			else if (context.Exception is ApplicationException)
			{
				// For ApplicationException we transfer to the front
				// a BadRequest 400 status with a message retrieve from
				// exception
				context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.HttpContext.Response.ContentType = "application/json";
				string result = JsonSerializer.Serialize(new
				{
					message = context.Exception.Message,
					reason = string.Empty,
					stackTrace = context.Exception.StackTrace
				});

				this.logService.LogError(context.Exception);
				await context.HttpContext.Response.WriteAsync(result);
			}

			// Unknown exception
			else
			{

				// For generic Exception we transfer to the front
				// an internal error 500 status with a message retrieve from
				// exception
				context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
				context.HttpContext.Response.ContentType = "application/json";
				string result = JsonSerializer.Serialize(new
				{
					message = "Unknown error",
					reason = context.Exception.Message,
					stackTrace = context.Exception.StackTrace
				});

				this.logService.LogError(context.Exception);
				await context.HttpContext.Response.WriteAsync(result);
			}
		}
		catch
		{
			// Ignore errors
		}
	}

	/// <summary>
	/// IActionFilter.OnActionExecuted implementation.
	/// </summary>
	/// <param name="context"></param>
	public void OnActionExecuted(ActionExecutedContext context)
	{
		// No action needed
	}

	/// <summary>
	/// IActionFilter.OnActionExecuting implementation.
	/// </summary>
	/// <param name="context"></param>
	public void OnActionExecuting(ActionExecutingContext context)
	{
		this.actionArguments = context.ActionArguments;
	}
}
