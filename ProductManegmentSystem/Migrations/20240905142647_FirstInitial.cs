using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManegmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class FirstInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Devices, gadgets, and accessories", "Electronics" },
                    { 2, "Books of all genres and formats", "Books" },
                    { 3, "Home appliances and kitchen tools", "Home & Kitchen" },
                    { 4, "Men's, Women's, and Children's clothing", "Clothing" },
                    { 5, "Equipment and gear for outdoor activities", "Sports & Outdoors" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageFileName", "Price", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Latest 5G smartphone", "1-2-samsung-mobile-phone-free-png-image.png", 799.99m, 50, "Smartphone" },
                    { 2, 1, "Noise-cancelling earbuds", "airpods.JPEG", 149.99m, 150, "Wireless Earbuds" },
                    { 3, 2, "Classic novel by F. Scott Fitzgerald", "book.jpeg", 10.99m, 100, "The Great Gatsby" },
                    { 4, 2, "A Memoir by Tara Westover", "book2.jpeg", 18.99m, 60, "Educated" },
                    { 5, 3, "High-speed blender for smoothies", "blender.jpeg", 79.99m, 30, "Blender" },
                    { 6, 3, "Automatic drip coffee machine", "cofeemaker.jpeg", 49.99m, 40, "Coffee Maker" },
                    { 7, 4, "Cotton crewneck t-shirt", "menTshirt.jpeg", 12.99m, 200, "Men's T-Shirt" },
                    { 8, 4, "Skinny fit jeans for women", "womenjeans.jpeg", 49.99m, 120, "Women's Jeans" },
                    { 9, 5, "Eco-friendly non-slip yoga mat", "YogaMat.jpeg", 24.99m, 100, "Yoga Mat" },
                    { 10, 5, "Professional-grade tennis racket", "racket.jpeg", 119.99m, 50, "Tennis Racket" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
