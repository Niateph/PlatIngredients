using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services.Mapping;

public class PlatProfile : Profile
{
	public PlatProfile()
	{
		this.CreateMap<Plat, PlatDto>().ReverseMap();
	}
}
