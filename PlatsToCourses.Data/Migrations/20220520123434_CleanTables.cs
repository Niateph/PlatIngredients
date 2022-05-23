using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatsToCourses.Data.Migrations
{
    public partial class CleanTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prix = table.Column<float>(type: "real", maxLength: 10, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "T_PlatIngredient",
                columns: table => new
                {
                    PlatId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", maxLength: 10, nullable: false, defaultValue: 0f),
                    Unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PlatIngredient", x => new { x.PlatId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_T_PlatIngredient_T_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "T_Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_PlatIngredient_T_Plat_PlatId",
                        column: x => x.PlatId,
                        principalTable: "T_Plat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "T_ProfileADGroup",
                columns: new[] { "Id", "ADGroupName", "ProfileId" },
                values: new object[,]
                {
                    { 2, "GROUP_AD_USER", 1 },
                    { 3, "ROLDAN_PLM_SU", 2 },
                    { 4, "GROUP_AD_READONLY", 3 }
                });

            migrationBuilder.InsertData(
                table: "T_Setting",
                columns: new[] { "Id", "Comment", "Key", "Value" },
                values: new object[,]
                {
                    { 1, null, "MonSetting", "Bleu" },
                    { 2, null, "UserDocumentation", "https://www.mon-intranoo.fr/transverse/procedures/OUTILS/MyPLM/Documents/Forms/AllItems.aspx" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_PlatIngredient_IngredientId",
                table: "T_PlatIngredient",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_PlatIngredient");

            migrationBuilder.DropTable(
                name: "T_ProfileADGroup");

            migrationBuilder.DropTable(
                name: "T_Setting");

            migrationBuilder.DropTable(
                name: "T_Ingredient");

            migrationBuilder.DropTable(
                name: "T_Plat");
        }
    }
}
