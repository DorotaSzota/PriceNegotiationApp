using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceNegotiationApp.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "PriceProposals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategory",
                table: "PriceProposals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
