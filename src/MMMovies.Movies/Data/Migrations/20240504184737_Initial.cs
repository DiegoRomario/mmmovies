using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MMMovies.Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movies");

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Movies",
                table: "Movies",
                columns: new[] { "Id", "Director", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("17c61e41-3953-42cd-8f88-d3f698869b35"), "Quentin Tarantino", 12.99m, "Kill Bill" },
                    { new Guid("a89f6cd7-4693-457b-9009-02205dbbfe45"), "Quentin Tarantino", 10.99m, "Pulp Fiction" },
                    { new Guid("e4fa19bf-6981-4e50-a542-7c9b26e9ec31"), "Quentin Tarantino", 11.99m, "Reservoir Dogs" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies",
                schema: "Movies");
        }
    }
}
