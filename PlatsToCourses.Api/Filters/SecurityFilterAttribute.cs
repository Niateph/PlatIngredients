using System.Data;

using Microsoft.AspNetCore.Authorization;

namespace PlatsToCourses.Api.Filters;

/// <summary>
/// This filter is use to handle security. It transforms
/// the ProfileEnum values provided to the corresponding roles (NT groups).
/// </summary>
public class SecurityFilterAttribute : AuthorizeAttribute
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="profiles"></param>
	public SecurityFilterAttribute(params ProfileEnum[] profiles) : base()
	{
		this.Roles = GetProfileNames(profiles);
	}

	private static string GetProfileNames(ProfileEnum[] profiles)
	{
		return string.Join(',', profiles.Select(p => Enum.GetName(typeof(ProfileEnum), p)).ToArray());
	}
}
