using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlatsToCourses.Data;
using PlatsToCourses.Services.Mapping;

namespace PlatsToCourses.Tests;

public abstract class TestBase
{
	protected BDDPlatsToCoursesContext Context { get; init; }

	protected IMapper Mapper { get; init; }

	public TestBase()
	{
		var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		string connectionString = config.GetConnectionString("PlatsToCourses");

		var dbContextOptions = new DbContextOptionsBuilder<BDDPlatsToCoursesContext>()
			.UseSqlServer(connectionString)
			.Options;

		this.Context = new BDDPlatsToCoursesContext(dbContextOptions);

		var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new ProfileADGroupProfile()); });
		this.Mapper = mapperConfiguration.CreateMapper();
	}
}
