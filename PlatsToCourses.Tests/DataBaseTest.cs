using PlatsToCourses.Services;
using Xunit;

namespace PlatsToCourses.Tests;

public class DataBaseTest : TestBase
{
	[Fact]
	public void CanGetProfileDataFromDatabase()
	{
		var profileService = new ProfileService(this.Context, this.Mapper);
		var profiles = profileService.GetAll();
		Assert.Equal(3, profiles.Count);
	}
}
