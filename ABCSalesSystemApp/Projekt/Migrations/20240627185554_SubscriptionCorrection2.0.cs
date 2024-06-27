using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionCorrection20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionRenewelInMonths",
                table: "subscription",
                newName: "SubscriptionDurationInMonths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionDurationInMonths",
                table: "subscription",
                newName: "SubscriptionRenewelInMonths");
        }
    }
}
