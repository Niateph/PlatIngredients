using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services.Mapping;

public class SettingProfile : Profile
{
	public SettingProfile()
	{
		this.CreateMap<Setting, SettingDto>().ReverseMap();
	}
}
