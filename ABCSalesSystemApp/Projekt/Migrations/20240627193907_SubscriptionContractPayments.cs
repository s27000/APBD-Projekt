using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionContractPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscriptionContractPayment",
                columns: table => new
                {
                    IdSubscriptionContractPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubscriptionContract = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptionContractPayment", x => x.IdSubscriptionContractPayment);
                    table.ForeignKey(
                        name: "FK_subscriptionContractPayment_subscriptionContract_IdSubscriptionContract",
                        column: x => x.IdSubscriptionContract,
                        principalTable: "subscriptionContract",
                        principalColumn: "IdSubscriptionContract",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptionContractPayment_IdSubscriptionContract",
                table: "subscriptionContractPayment",
                column: "IdSubscriptionContract");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscriptionContractPayment");
        }
    }
}
