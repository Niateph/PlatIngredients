using AutoMapper.QueryableExtensions;

namespace PlatsToCourses.Services;

public class SettingService : ISettingService
{
	private readonly BDDPlatsToCoursesContext context;
	private readonly IMapper mapper;

	public SettingService(BDDPlatsToCoursesContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	public List<SettingDto> GetAll()
	{
		return this.context.Settings.ProjectTo<SettingDto>(this.mapper.ConfigurationProvider).ToList();
	}
}
