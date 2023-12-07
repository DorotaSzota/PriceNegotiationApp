using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceNegotiationApp.Migrations
{
    /// <inheritdoc />
    public partial class PriceProposalRework3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PriceProposals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PriceProposals_UserId",
                table: "PriceProposals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceProposals_Users_UserId",
                table: "PriceProposals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceProposals_Users_UserId",
                table: "PriceProposals");

            migrationBuilder.DropIndex(
                name: "IX_PriceProposals_UserId",
                table: "PriceProposals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PriceProposals");
        }
    }
}
