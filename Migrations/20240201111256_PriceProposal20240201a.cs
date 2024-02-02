using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceNegotiationApp.Migrations
{
    /// <inheritdoc />
    public partial class PriceProposal20240201a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProductPriceVisible",
                table: "PriceProposals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPriceVisible",
                table: "PriceProposals");
        }
    }
}
