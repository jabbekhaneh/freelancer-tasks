using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.EF.Migrations
{
    public partial class init_add_password_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjetcTasks_Projects_ProjectId",
                table: "ProjetcTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjetcTasks",
                table: "ProjetcTasks");

            migrationBuilder.RenameTable(
                name: "ProjetcTasks",
                newName: "ProjectTasks");

            migrationBuilder.RenameIndex(
                name: "IX_ProjetcTasks_ProjectId",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTasks",
                table: "ProjectTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                table: "ProjectTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTasks",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "ProjectTasks",
                newName: "ProjetcTasks");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_ProjectId",
                table: "ProjetcTasks",
                newName: "IX_ProjetcTasks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjetcTasks",
                table: "ProjetcTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjetcTasks_Projects_ProjectId",
                table: "ProjetcTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
