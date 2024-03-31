using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyModel.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    FamilyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table__41D82F6B84CD1217", x => x.FamilyId);
                });

            migrationBuilder.CreateTable(
                name: "Genus",
                columns: table => new
                {
                    GenusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Charecteristic = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table__110363700F6A1A72", x => x.GenusId);
                    table.ForeignKey(
                        name: "FK_Genus_Family",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "FamilyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genus_FamilyId",
                table: "Genus",
                column: "FamilyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genus");

            migrationBuilder.DropTable(
                name: "Family");
        }
    }
}
