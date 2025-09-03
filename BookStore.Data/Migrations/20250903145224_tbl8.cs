using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class tbl8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "BaseEntite");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "BaseEntite",
                newName: "IX_BaseEntite_ParentId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "BaseEntite",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BaseEntite",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Borrow",
                table: "BaseEntite",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BaseEntite",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseEntite",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "BaseEntite",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEntite",
                table: "BaseEntite",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntite_CategoryId",
                table: "BaseEntite",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntite_BaseEntite_CategoryId",
                table: "BaseEntite",
                column: "CategoryId",
                principalTable: "BaseEntite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntite_BaseEntite_ParentId",
                table: "BaseEntite",
                column: "ParentId",
                principalTable: "BaseEntite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntite_BaseEntite_CategoryId",
                table: "BaseEntite");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntite_BaseEntite_ParentId",
                table: "BaseEntite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEntite",
                table: "BaseEntite");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntite_CategoryId",
                table: "BaseEntite");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "BaseEntite");

            migrationBuilder.DropColumn(
                name: "Borrow",
                table: "BaseEntite");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BaseEntite");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseEntite");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "BaseEntite");

            migrationBuilder.RenameTable(
                name: "BaseEntite",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEntite_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Categories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

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
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Borrow = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
    }
}
