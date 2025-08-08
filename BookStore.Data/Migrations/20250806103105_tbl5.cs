using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class tbl5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntites_BaseEntites_CategoryId",
                table: "BaseEntites");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntites_BaseEntites_ParentId",
                table: "BaseEntites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEntites",
                table: "BaseEntites");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntites_CategoryId",
                table: "BaseEntites");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "BaseEntites");

            migrationBuilder.DropColumn(
                name: "Borrow",
                table: "BaseEntites");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BaseEntites");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseEntites");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "BaseEntites");

            migrationBuilder.RenameTable(
                name: "BaseEntites",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEntites_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Borrow = table.Column<bool>(type: "bit", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "BaseEntites");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "BaseEntites",
                newName: "IX_BaseEntites_ParentId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BaseEntites",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Borrow",
                table: "BaseEntites",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BaseEntites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseEntites",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "BaseEntites",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEntites",
                table: "BaseEntites",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntites_CategoryId",
                table: "BaseEntites",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntites_BaseEntites_CategoryId",
                table: "BaseEntites",
                column: "CategoryId",
                principalTable: "BaseEntites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntites_BaseEntites_ParentId",
                table: "BaseEntites",
                column: "ParentId",
                principalTable: "BaseEntites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
