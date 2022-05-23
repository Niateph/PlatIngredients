using System.Diagnostics;
using System.Reflection;
using CommonPackages.Jwt.Infrastructure;
using CommonPackages.Ldap.Infrastructure;
using CommonPackages.Mail.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;

using Microsoft.OpenApi.Models;
using PlatsToCourses.Api.Constants;
using PlatsToCourses.Api.Options;
using PlatsToCourses.Data;
using PlatsToCourses.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

var applicationOptions = new ApplicationOptions();
builder.Configuration.GetSection("Application").Bind(applicationOptions);
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("Application"));


// Add azure telemetry
builder.Services.AddSingleton<ITelemetryInitializer, EnrichedTelemetry>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
{
	module.EnableSqlCommandTextInstrumentation = false;
});

// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy(AppSettings.CorsPolicyName, builder =>
	{
		builder
			.WithOrigins(applicationOptions.CorsUri)
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials()
			.WithExposedHeaders("Content-Disposition");
	});
});

builder.Services.AddDbContext<BDDPlatsToCoursesContext>(options => options
	.UseSqlServer(builder.Configuration.GetConnectionString(AppSettings.ConnectionString)).LogTo(log=>Debug.WriteLine(log)));

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(SettingProfile)));
builder.Services.AddControllers();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatsToCourses.Api", Version = "v1" });

	// Set the comments path for the Swagger JSON and UI
	string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);

	c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
	{
		Description = "Authorization header using the user token (Example: 'Bearer 698B3E57-F7F9-4F07-86A0-XXXXXXXXXXXX')",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = JwtBearerDefaults.AuthenticationScheme
							}
						},
						Array.Empty<string>()
					}
	});
});
#endregion

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

// Enregistrement des services métiers
AddBusinessServices(builder.Services);

// CARGO Common Packages
builder.Services.AddScoped<ILdapService, LdapService>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddJwtService(builder.Configuration)
		.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddNegotiate()
		.AddJwtBearer(x =>
		{
			x.SaveToken = true;
			x.TokenValidationParameters = TokenValidationParametersHelper.Create(builder.Configuration);
		});

builder.Services.AddHealthChecks().AddUrlGroup(new Uri("https://localhost:44352/api/Version"),
				  name: "API Check",
				  failureStatus: HealthStatus.Degraded)
			  .AddSqlServer(builder.Configuration.GetConnectionString(AppSettings.ConnectionString), name: "Database check");

// Si besoin healthchecks dans Azure
//.AddApplicationInsightsPublisher();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatsToCourses.Api v1"));

//app.UseHttpsRedirection();
app.UseFileServer(new FileServerOptions
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
	RequestPath = "/StaticFiles",
	EnableDefaultFiles = true
});

app.UseRouting();
app.UseCors(AppSettings.CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	endpoints.MapHealthChecksUI(setup =>
	{
		setup.AddCustomStylesheet("StaticFiles/healthchecks-ui.css");
	});
	endpoints.MapHealthChecks("/health", new HealthCheckOptions()
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});
});

app.Run();

void AddBusinessServices(IServiceCollection services)
{
	services.AddScoped<IUserService, UserService>();
	services.AddScoped<IProfileService, ProfileService>();
	services.AddScoped<ILogService, LogService>();
	services.AddScoped<ISettingService, SettingService>();
	services.AddScoped<IPlatService, PlatService>();
}
