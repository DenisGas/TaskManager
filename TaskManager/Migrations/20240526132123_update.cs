using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMyTask_Categories_CategoriesId",
                table: "CategoryMyTask");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMyTask_Tasks_TasksId",
                table: "CategoryMyTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryMyTask",
                table: "CategoryMyTask");

            migrationBuilder.RenameTable(
                name: "CategoryMyTask",
                newName: "TaskCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryMyTask_TasksId",
                table: "TaskCategories",
                newName: "IX_TaskCategories_TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskCategories",
                table: "TaskCategories",
                columns: new[] { "CategoriesId", "TasksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategories_Categories_CategoriesId",
                table: "TaskCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategories_Tasks_TasksId",
                table: "TaskCategories",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategories_Categories_CategoriesId",
                table: "TaskCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategories_Tasks_TasksId",
                table: "TaskCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskCategories",
                table: "TaskCategories");

            migrationBuilder.RenameTable(
                name: "TaskCategories",
                newName: "CategoryMyTask");

            migrationBuilder.RenameIndex(
                name: "IX_TaskCategories_TasksId",
                table: "CategoryMyTask",
                newName: "IX_CategoryMyTask_TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryMyTask",
                table: "CategoryMyTask",
                columns: new[] { "CategoriesId", "TasksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMyTask_Categories_CategoriesId",
                table: "CategoryMyTask",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMyTask_Tasks_TasksId",
                table: "CategoryMyTask",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
