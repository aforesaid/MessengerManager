using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MessengerManager.Infrastructure.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatThreads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupChatId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ThreadName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    MessageId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatThreads", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreads_SupChatId",
                table: "ChatThreads",
                column: "SupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreads_ThreadName",
                table: "ChatThreads",
                column: "ThreadName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatThreads");
        }
    }
}
