using Microsoft.EntityFrameworkCore.Migrations;

namespace MessengerManager.Infrastructure.Data.Migrations
{
    public partial class UpdateMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatThreadName",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Owner",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatThreadName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Messages");

            migrationBuilder.AddColumn<long>(
                name: "MessageId",
                table: "Messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "ChatThreadName",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatThreadName",
                table: "Messages",
                column: "ChatThreadName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Owner",
                table: "Messages",
                column: "Owner");
        }
    }
}
