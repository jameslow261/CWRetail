using Microsoft.EntityFrameworkCore.Migrations;

namespace CWRetail.Products.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "IsActive", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, true, "Book product", 1.23m, 0 },
                    { 2, true, "Electronics product", 2.34m, 1 },
                    { 3, true, "Food product", 3.45m, 2 },
                    { 4, true, "Furniture product", 4.56m, 3 },
                    { 5, true, "Toys product", 5.67m, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
