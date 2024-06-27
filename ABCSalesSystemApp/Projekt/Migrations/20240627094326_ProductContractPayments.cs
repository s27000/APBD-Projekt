using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class ProductContractPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdContract",
                table: "productContract",
                newName: "IdProductContract");

            migrationBuilder.CreateTable(
                name: "productContractPayment",
                columns: table => new
                {
                    IdProductContractPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProductContract = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productContractPayment", x => x.IdProductContractPayment);
                    table.ForeignKey(
                        name: "FK_productContractPayment_productContract_IdProductContract",
                        column: x => x.IdProductContract,
                        principalTable: "productContract",
                        principalColumn: "IdProductContract",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productContractPayment_IdProductContract",
                table: "productContractPayment",
                column: "IdProductContract");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productContractPayment");

            migrationBuilder.RenameColumn(
                name: "IdProductContract",
                table: "productContract",
                newName: "IdContract");
        }
    }
}
