using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services.Mapping;

public class ProfileADGroupProfile : Profile
{
	public ProfileADGroupProfile()
	{
		this.CreateMap<ProfileADGroup, ProfileDto>().ReverseMap();
	}
}
