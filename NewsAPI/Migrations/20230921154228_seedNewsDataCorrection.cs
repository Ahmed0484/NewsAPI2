using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NewsAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedNewsDataCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "CategoryId", "Content", "ImageUrl", "PublishDate", "Title", "Writer" },
                values: new object[,]
                {
                    { 1, 2, "news of worldnews of worldnews of worldnews of worldnews of world", "nop", new DateTime(2023, 9, 21, 18, 42, 28, 715, DateTimeKind.Local).AddTicks(3423), "news of Egypt", "Ali" },
                    { 2, 1, "news of worldnews of worldnews of worldnews of worldnews of world", "nop", new DateTime(2023, 9, 21, 18, 42, 28, 715, DateTimeKind.Local).AddTicks(3441), "news of world", "Ali" },
                    { 3, 3, "news of worldnews of worldnews of worldnews of worldnews of world", "nop", new DateTime(2023, 9, 21, 18, 42, 28, 715, DateTimeKind.Local).AddTicks(3443), "news of Climate", "Ali" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
