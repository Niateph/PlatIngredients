using System.Security.Claims;

namespace PlatsToCourses.Services;

public interface IProfileService
{
	List<ProfileDto> GetAll();

	ProfileEnum GetUserRole(ClaimsPrincipal principal);
}
