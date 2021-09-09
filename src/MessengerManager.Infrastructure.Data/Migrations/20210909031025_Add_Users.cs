using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MessengerManager.Infrastructure.Data.Migrations
{
    public partial class Add_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupChatId",
                table: "ChatThreads",
                newName: "TelegramSupChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatThreads_SupChatId",
                table: "ChatThreads",
                newName: "IX_ChatThreads_TelegramSupChatId");

            migrationBuilder.AddColumn<long>(
                name: "VkPeerId",
                table: "ChatThreads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    UniqueId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name_LastName_UniqueId",
                table: "Users",
                columns: new[] { "Name", "LastName", "UniqueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "VkPeerId",
                table: "ChatThreads");

            migrationBuilder.RenameColumn(
                name: "TelegramSupChatId",
                table: "ChatThreads",
                newName: "SupChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatThreads_TelegramSupChatId",
                table: "ChatThreads",
                newName: "IX_ChatThreads_SupChatId");
        }
    }
}
