using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class ProductContractUpdate30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractDateTo",
                table: "productContract",
                newName: "DateTo");

            migrationBuilder.RenameColumn(
                name: "ContractDateFrom",
                table: "productContract",
                newName: "DateFrom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "productContract",
                newName: "ContractDateTo");

            migrationBuilder.RenameColumn(
                name: "DateFrom",
                table: "productContract",
                newName: "ContractDateFrom");
        }
    }
}
