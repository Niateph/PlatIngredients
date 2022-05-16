using System.Security.Claims;
using System.Security.Principal;
using AutoMapper.QueryableExtensions;

namespace PlatsToCourses.Services;

public class ProfileService : IProfileService
{
	private readonly BDDPlatsToCoursesContext context;
	private readonly IMapper mapper;

	public ProfileService(BDDPlatsToCoursesContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	public List<ProfileDto> GetAll()
	{
		return this.context.ProfileADGroup.ProjectTo<ProfileDto>(this.mapper.ConfigurationProvider).ToList();
	}

	public ProfileEnum GetUserRole(ClaimsPrincipal principal)
	{
		var identity = (WindowsIdentity)principal.Identity;

		// Récupère tous les noms des groupes AD de l'utilisateur
		var userGroupNames = identity.Groups.Select(g => g.Translate(typeof(NTAccount)).ToString().Split('\\').Last()).ToList();

		// Récupère la liste des profiles associés aux groupes AD
		var profiles = this.GetAll();

		// Récupère, si il existe, le groupe AD dont fait partie l'utilisateur
		var profile = profiles.Where(p => userGroupNames.Contains(p.ADGroupName)).FirstOrDefault();
		if (profile != null)
		{
			return profile.ProfileId;
		}

		return ProfileEnum.None;
	}
}
