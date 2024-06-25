using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "firm",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KRS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_firm", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_firm_client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_person_client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "firm");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "client");
        }
    }
}
