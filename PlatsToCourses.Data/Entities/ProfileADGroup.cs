using PlatsToCourses.Common;

namespace PlatsToCourses.Data.Entities;

public class ProfileADGroup
{
	public int Id { get; set; }
	public string ADGroupName { get; set; }
	public ProfileEnum ProfileId { get; set; }
}