using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class AddedInToDoItemCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "ToDoItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_CategoryID",
                table: "ToDoItem",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_Category_CategoryID",
                table: "ToDoItem",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_Category_CategoryID",
                table: "ToDoItem");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItem_CategoryID",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "ToDoItem");
        }
    }
}
