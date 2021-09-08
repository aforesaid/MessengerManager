using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MessengerManager.Infrastructure.Data.Migrations
{
    public partial class Add_Messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    ChatThreadName = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Sent = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatThreadName",
                table: "Messages",
                column: "ChatThreadName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Date",
                table: "Messages",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Owner",
                table: "Messages",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Sent",
                table: "Messages",
                column: "Sent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
