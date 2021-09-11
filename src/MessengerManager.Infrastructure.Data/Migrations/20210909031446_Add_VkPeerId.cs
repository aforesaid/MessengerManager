using Microsoft.EntityFrameworkCore.Migrations;

namespace MessengerManager.Infrastructure.Data.Migrations
{
    public partial class Add_VkPeerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VkPeerId",
                table: "Messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_VkPeerId",
                table: "Messages",
                column: "VkPeerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_VkPeerId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "VkPeerId",
                table: "Messages");
        }
    }
}
