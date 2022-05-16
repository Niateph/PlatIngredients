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


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{

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
		});

		modelBuilder.Entity<ProfileADGroup>(entity =>
		{
			entity.ToTable("T_ProfileADGroup");

			entity.Property(e => e.Id).ValueGeneratedOnAdd();

			entity.Property(e => e.ADGroupName)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);
		});

		this.OnModelCreatingPartial(modelBuilder);

		modelBuilder.Entity<Plat>(entity =>
		{
			entity.ToTable("T_Plat");
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
			entity.Property(e => e.Nom)
			.IsRequired()
			.HasMaxLength(100);
		});

		modelBuilder.Entity<Ingredient>(entity =>
		{
			entity.ToTable("T_Ingredient");
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
		});
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
