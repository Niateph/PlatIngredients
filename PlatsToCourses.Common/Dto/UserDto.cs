namespace PlatsToCourses.Common;

public class UserDto
{
	public string DisplayName { get; set; }

	public string Email { get; set; }

	public string Company { get; set; }

	public string Profile { get; set; }

	public bool IsAuthenticated { get; set; } = false;

	public string Id { get; set; }
}
