using CommonPackages.Jwt.Infrastructure;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PlatsToCourses.Services;

/// <summary>
/// Ajoute des informations aux traces Azure Monitor, tel que le nom de l'utilisateur par exemple.
/// </summary>
public class EnrichedTelemetry : ITelemetryInitializer
{
	private readonly IServiceProvider serviceProvider;
	private readonly IHttpContextAccessor httpContextAccessor;

	public EnrichedTelemetry(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
	{
		this.serviceProvider = serviceProvider;
		this.httpContextAccessor = httpContextAccessor;
	}

	public void Initialize(ITelemetry telemetry)
	{
		try
		{
			// Check if we are on a request telemetry 
			if (telemetry is not ISupportProperties enrichedTelemetry)
			{
				return;
			}

			if (this.httpContextAccessor.HttpContext == null)
			{
				return;
			}

			var principal = this.httpContextAccessor.HttpContext.User;
			if (!principal.Identity.IsAuthenticated)
			{
				return;
			}

			// Get current user
			using var scope = this.serviceProvider.CreateScope();
			var tokenService = scope.ServiceProvider.GetService<ITokenService>();
			if (tokenService == null)
			{
				return;
			}

			var user = tokenService.FromClaimUser(principal);
			if (user != null && user.Name != null)
			{
				// TODO Sélectionner les informations pertinentes
				this.AddOrUpdateProperty(enrichedTelemetry, "userLogin", user.Name);
				this.AddOrUpdateProperty(enrichedTelemetry, "company", user.Company);
			}
		}
		catch
		{
			// Ignore error
		}
	}

	private void AddOrUpdateProperty(ISupportProperties telemetry, string key, string value)
	{
		if (telemetry.Properties.ContainsKey(key))
		{
			telemetry.Properties[key] = value;
		}
		else
		{
			telemetry.Properties.Add(key, value);
		}
	}
}
