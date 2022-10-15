using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infrastructure.Migrations
{
    public partial class AddPrivateChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivateChatId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrivateChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateChats_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PrivateChatId",
                table: "Messages",
                column: "PrivateChatId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateChats_ToUserId",
                table: "PrivateChats",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_PrivateChats_PrivateChatId",
                table: "Messages",
                column: "PrivateChatId",
                principalTable: "PrivateChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_PrivateChats_PrivateChatId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "PrivateChats");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PrivateChatId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PrivateChatId",
                table: "Messages");
        }
    }
}
