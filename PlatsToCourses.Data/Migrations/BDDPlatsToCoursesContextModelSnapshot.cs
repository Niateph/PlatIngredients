// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlatsToCourses.Data;

#nullable disable

namespace PlatsToCourses.Data.Migrations
{
    [DbContext(typeof(BDDPlatsToCoursesContext))]
    partial class BDDPlatsToCoursesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("French_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlatsToCourses.Data.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Prix")
                        .HasMaxLength(10)
                        .HasColumnType("real");

                    b.Property<string>("Unit")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("T_Ingredient", (string)null);
                });

            modelBuilder.Entity("PlatsToCourses.Data.Entities.Plat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("T_Plat", (string)null);
                });

            modelBuilder.Entity("PlatsToCourses.Data.Entities.PlatIngredient", b =>
                {
                    b.Property<int>("PlatId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<float>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<string>("Unit")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("PlatId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("T_PlatIngredient", (string)null);
                });

            modelBuilder.Entity("PlatsToCourses.Data.Entities.ProfileADGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ADGroupName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("T_ProfileADGroup", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            ADGroupName = "GROUP_AD_USER",
                            ProfileId = 1
                        },
                        new
                        {
                            Id = 3,
                            ADGroupName = "ROLDAN_PLM_SU",
                            ProfileId = 2
                        },
                        new
                        {
                            Id = 4,
                            ADGroupName = "GROUP_AD_READONLY",
                            ProfileId = 3
                        });
                });

            modelBuilder.Entity("PlatsToCourses.Data.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("T_Setting", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Key = "MonSetting",
                            Value = "Bleu"
                        },
                        new
                        {
                            Id = 2,
                            Key = "UserDocumentation",
                            Value = "https://www.mon-intranoo.fr/transverse/procedures/OUTILS/MyPLM/Documents/Forms/AllItems.aspx"
                        });
                });

            modelBuilder.Entity("PlatsToCourses.Data.Entities.PlatIngredient", b =>
                {
                    b.HasOne("PlatsToCourses.Data.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlatsToCourses.Data.Entities.Plat", null)
                        .WithMany()
                        .HasForeignKey("PlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
