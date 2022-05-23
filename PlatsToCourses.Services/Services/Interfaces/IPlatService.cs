using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services
{
	public interface IPlatService
	{
		List<PlatDto> GetAll(int page, int nbByPage);
		Plat AddOne(PlatNewDto plat);

		bool DeleteOne(int id);

		Plat UpdateOne(PlatDto plats);
	}
}
