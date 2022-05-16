namespace PlatsToCourses.Services;

public class CustomClaims
{
	public const string CustomClaimPrefix = "http://cargoservice.fr";

	public static readonly string CustomClaimProfile = $"{CustomClaimPrefix}/role";

	public static readonly string CustomClaimCompanyName = $"{CustomClaimPrefix}/CompanyName";
}
