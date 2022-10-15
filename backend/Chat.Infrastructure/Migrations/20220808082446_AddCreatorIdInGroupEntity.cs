using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infrastructure.Migrations
{
    public partial class AddCreatorIdInGroupEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Groups");
        }
    }
}
