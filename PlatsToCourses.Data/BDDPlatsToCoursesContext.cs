using Microsoft.EntityFrameworkCore;
using PlatsToCourses.Data.Entities;


#nullable disable

namespace PlatsToCourses.Data;

public partial class BDDPlatsToCoursesContext : DbContext
{
	public BDDPlatsToCoursesContext()
	{
	}

	public BDDPlatsToCoursesContext(DbContextOptions<BDDPlatsToCoursesContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Setting> Settings { get; set; }

	public virtual DbSet<ProfileADGroup> ProfileADGroup { get; set; }

	public virtual DbSet<Plat> Plats { get; set; }

	public virtual DbSet<Ingredient> Ingredients { get; set; }

	public virtual DbSet<PlatIngredient> PlatIngredients { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

		modelBuilder.Entity<Setting>(entity =>
		{
			entity.ToTable("T_Setting");

			entity.Property(e => e.Comment).HasMaxLength(256);

			entity.Property(e => e.Id).ValueGeneratedOnAdd();

			entity.Property(e => e.Key)
				.IsRequired()
				.HasMaxLength(256)
				.IsUnicode(false);

			entity.Property(e => e.Value)
				.IsRequired()
				.HasMaxLength(256)
				.IsUnicode(false);

			entity.HasData(new Setting { Id = 1, Key = "MonSetting", Value = "Bleu" }, new Setting { Id = 2, Key = "UserDocumentation", Value = "https://www.mon-intranoo.fr/transverse/procedures/OUTILS/MyPLM/Documents/Forms/AllItems.aspx" });
		});

		modelBuilder.Entity<ProfileADGroup>(entity =>
		{
			entity.ToTable("T_ProfileADGroup");

			entity.Property(e => e.Id).ValueGeneratedOnAdd();

			entity.Property(e => e.ADGroupName)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);

			entity.HasData(new ProfileADGroup { Id = 2, ADGroupName = "GROUP_AD_USER", ProfileId = Common.ProfileEnum.User }, new ProfileADGroup { Id = 3, ADGroupName = "ROLDAN_PLM_SU", ProfileId = Common.ProfileEnum.Administrator }, new ProfileADGroup { Id = 4, ADGroupName = "GROUP_AD_READONLY", ProfileId = Common.ProfileEnum.ReadOnly });
		});

		modelBuilder.Entity<Ingredient>(entity =>
		{
			entity.ToTable("T_Ingredient");
			entity.Property(e => e.Nom)
			.IsRequired()
			.HasMaxLength(50);
			entity.Property(e => e.Prix)
			.HasMaxLength(10);
			entity.Property(e => e.Unit)
			.HasMaxLength(10);
		});


		modelBuilder.Entity<Plat>(entity =>
		{
			entity.ToTable("T_Plat");
			entity.Property(e => e.Nom)
			.IsRequired()
			.HasMaxLength(50);
		});

		modelBuilder.Entity<PlatIngredient>().ToTable("T_PlatIngredient");

		modelBuilder.Entity<PlatIngredient>().HasKey(pi => new { pi.PlatId, pi.IngredientId });


		modelBuilder.Entity<PlatIngredient>()
			.HasOne(pi => pi.Plat)
			.WithMany(pi => pi.PlatIngredients)
			.HasForeignKey(pi => pi.PlatId);
		modelBuilder.Entity<PlatIngredient>()
			.HasOne(pi => pi.Ingredient)
			.WithMany(pi => pi.PlatIngredients)
			.HasForeignKey(pi => pi.IngredientId);

		modelBuilder.Entity<PlatIngredient>().Property(pi => pi.Amount).HasDefaultValue(0).HasMaxLength(10);
		modelBuilder.Entity<PlatIngredient>().Property(pi => pi.Unit).HasDefaultValue("").HasMaxLength(10);	

		this.OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
