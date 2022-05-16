using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services
{
	public interface IPlatService
	{
		List<PlatDto> GetAll();
		Plat AddOne(PlatDto plat);

		Plat RemoveOne(PlatDto plats);

		Plat UpdateOne(PlatDto plats);
	}
}
