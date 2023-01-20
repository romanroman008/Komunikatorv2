using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Komunikator.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ip = table.Column<string>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Online = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserModelId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    DateRecived = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Owner = table.Column<string>(type: "TEXT", nullable: false),
                    UserModelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageModelId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "UserModelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserModelId",
                table: "Messages",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
