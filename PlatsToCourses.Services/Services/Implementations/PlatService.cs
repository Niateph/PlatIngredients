using AutoMapper.QueryableExtensions;
using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services;
public class PlatService : IPlatService
{
	private readonly BDDPlatsToCoursesContext context;
	private readonly IMapper mapper;

	public PlatService(BDDPlatsToCoursesContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}
	public List<PlatDto> GetAll()
	{
		List<PlatDto> listToSend = new();
		foreach(Plat plat in this.context.Plats)
		{
			PlatDto dto = new();
			dto.Nom = plat.Nom;
			listToSend.Add(dto);
		}
		return listToSend;
		//return this.context.Plats.ProjectTo<PlatDto>(this.mapper.ConfigurationProvider).ToList();
	}

	public Plat AddOne(PlatDto platAAjouter)
	{
		Plat monPlat = new ();
		monPlat = mapper.Map<PlatDto, Plat>(platAAjouter, monPlat);
		this.context.Plats.Add( monPlat );
		this.context.SaveChanges();
		return monPlat;
	}

	public Plat DeleteOne(PlatDto platADelete)
	{
		this.context.Plats.Remove(this.context.Plats.Find(platADelete.id);

	}
}
