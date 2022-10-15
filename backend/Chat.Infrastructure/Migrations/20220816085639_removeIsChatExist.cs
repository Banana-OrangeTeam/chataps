using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infrastructure.Migrations
{
    public partial class removeIsChatExist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChatExist",
                table: "UserPrivateChat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChatExist",
                table: "UserPrivateChat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
