using System.Security.Claims;
using System.Threading.Tasks;
using PlatsToCourses.Common;

namespace PlatsToCourses.Services;

public interface IUserService
{
	UserDto FromClaimUser();

}
