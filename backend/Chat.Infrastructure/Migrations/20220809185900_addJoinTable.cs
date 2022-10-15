using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infrastructure.Migrations
{
    public partial class addJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateChats_AspNetUsers_ToUserId",
                table: "PrivateChats");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropIndex(
                name: "IX_PrivateChats_ToUserId",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "PrivateChats");

            migrationBuilder.AddColumn<int>(
                name: "LastMessageId",
                table: "PrivateChats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastMessageId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserGroup_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPrivateChat",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrivateChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivateChat", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserPrivateChat_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPrivateChat_PrivateChats_PrivateChatId",
                        column: x => x.PrivateChatId,
                        principalTable: "PrivateChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId1",
                table: "UserGroup",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChat_PrivateChatId",
                table: "UserPrivateChat",
                column: "PrivateChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChat_UserId1",
                table: "UserPrivateChat",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserPrivateChat");

            migrationBuilder.DropColumn(
                name: "LastMessageId",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "LastMessageId",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "ToUserId",
                table: "PrivateChats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateChats_ToUserId",
                table: "PrivateChats",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateChats_AspNetUsers_ToUserId",
                table: "PrivateChats",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
