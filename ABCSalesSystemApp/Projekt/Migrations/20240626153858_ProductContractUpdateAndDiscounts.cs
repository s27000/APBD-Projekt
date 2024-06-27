using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class ProductContractUpdateAndDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualPrice",
                table: "product",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    Discount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount", x => x.Discount);
                });

            migrationBuilder.CreateTable(
                name: "productContract",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractDateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractDateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductUpdateDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateSupportExtension = table.Column<int>(type: "int", nullable: false),
                    IdDiscount = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productContract", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_productContract_client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productContract_discount_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "discount",
                        principalColumn: "Discount");
                    table.ForeignKey(
                        name: "FK_productContract_product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productContract_IdClient",
                table: "productContract",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_productContract_IdDiscount",
                table: "productContract",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_productContract_IdProduct",
                table: "productContract",
                column: "IdProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productContract");

            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualPrice",
                table: "product",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
