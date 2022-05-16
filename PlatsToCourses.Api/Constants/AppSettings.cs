namespace PlatsToCourses.Api.Constants;

/// <summary>
/// Constantes pour les clés du fichier appsettings.json.
/// </summary>
public static class AppSettings
{
	/// <summary>
	/// Clé pour la configuration de Serilog.
	/// </summary>
	public const string Serilog = "Serilog";

	/// <summary>
	/// Clé pour la configuration du CORS.
	/// </summary>
	public const string CORS = "CORS";

	/// <summary>
	/// Clé pour la connexion à la base de données.
	/// </summary>
	public const string ConnectionString = "PlatsToCourses";

	public const string CorsPolicyName = "GeneralCorsPolicy";
}
