using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "productContract",
                newName: "DiscountValue");

            migrationBuilder.CreateTable(
                name: "subscriptionContract",
                columns: table => new
                {
                    IdSubscriptionContract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdSubscription = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDiscount = table.Column<int>(type: "int", nullable: true),
                    DiscountValue = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptionContract", x => x.IdSubscriptionContract);
                    table.ForeignKey(
                        name: "FK_subscriptionContract_client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptionContract_discount_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "discount",
                        principalColumn: "IdDiscount");
                    table.ForeignKey(
                        name: "FK_subscriptionContract_subscription_IdSubscription",
                        column: x => x.IdSubscription,
                        principalTable: "subscription",
                        principalColumn: "IdSubscription",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptionContract_IdClient",
                table: "subscriptionContract",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptionContract_IdDiscount",
                table: "subscriptionContract",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptionContract_IdSubscription",
                table: "subscriptionContract",
                column: "IdSubscription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscriptionContract");

            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "productContract",
                newName: "Value");
        }
    }
}
