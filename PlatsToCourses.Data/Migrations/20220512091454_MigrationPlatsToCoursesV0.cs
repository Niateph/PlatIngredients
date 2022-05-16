using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatsToCourses.Data.Migrations
{
    public partial class MigrationPlatsToCoursesV0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Plat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Plat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_ProfileADGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADGroupName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ProfileADGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Setting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Ingredient");

            migrationBuilder.DropTable(
                name: "T_Plat");

            migrationBuilder.DropTable(
                name: "T_ProfileADGroup");

            migrationBuilder.DropTable(
                name: "T_Setting");
        }
    }
}
