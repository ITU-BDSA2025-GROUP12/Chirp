using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chirp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayNameAndIsDeletedToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeps_AspNetUsers_Id",
                table: "Cheeps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cheeps",
                table: "Cheeps");

            migrationBuilder.DropIndex(
                name: "IX_Cheeps_Id",
                table: "Cheeps");

            migrationBuilder.RenameColumn(
                name: "CheepId",
                table: "Cheeps",
                newName: "AuthorId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cheeps",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Cheeps",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cheeps",
                table: "Cheeps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cheeps_AuthorId",
                table: "Cheeps",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeps_AspNetUsers_AuthorId",
                table: "Cheeps",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeps_AspNetUsers_AuthorId",
                table: "Cheeps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cheeps",
                table: "Cheeps");

            migrationBuilder.DropIndex(
                name: "IX_Cheeps_AuthorId",
                table: "Cheeps");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Cheeps",
                newName: "CheepId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cheeps",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "CheepId",
                table: "Cheeps",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cheeps",
                table: "Cheeps",
                column: "CheepId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheeps_Id",
                table: "Cheeps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeps_AspNetUsers_Id",
                table: "Cheeps",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
