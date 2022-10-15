using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infrastructure.Migrations
{
    public partial class addCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_AspNetUsers_UserId1",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrivateChat_AspNetUsers_UserId1",
                table: "UserPrivateChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPrivateChat",
                table: "UserPrivateChat");

            migrationBuilder.DropIndex(
                name: "IX_UserPrivateChat_UserId1",
                table: "UserPrivateChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_UserId1",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserPrivateChat");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPrivateChat",
                table: "UserPrivateChat",
                columns: new[] { "UserId", "PrivateChatId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_AspNetUsers_UserId",
                table: "UserGroup",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrivateChat_AspNetUsers_UserId",
                table: "UserPrivateChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_AspNetUsers_UserId",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrivateChat_AspNetUsers_UserId",
                table: "UserPrivateChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPrivateChat",
                table: "UserPrivateChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserPrivateChat",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserGroup",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPrivateChat",
                table: "UserPrivateChat",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChat_UserId1",
                table: "UserPrivateChat",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId1",
                table: "UserGroup",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_AspNetUsers_UserId1",
                table: "UserGroup",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrivateChat_AspNetUsers_UserId1",
                table: "UserPrivateChat",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
