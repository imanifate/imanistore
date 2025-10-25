using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class tbls1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrowing_BookId_UserId",
                table: "Borrowing");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BookId_UserId",
                table: "Borrowing",
                columns: new[] { "BookId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrowing_BookId_UserId",
                table: "Borrowing");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BookId_UserId",
                table: "Borrowing",
                columns: new[] { "BookId", "UserId" },
                unique: true);
        }
    }
}
