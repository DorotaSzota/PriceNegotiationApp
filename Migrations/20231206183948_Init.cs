using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceNegotiationApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceProposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProposedPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Accepted = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttemptsLeft = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceProposals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProductCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductDescription = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceProposals");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
