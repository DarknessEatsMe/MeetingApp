using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace meetingApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adresses");

            migrationBuilder.DropTable(
                name: "descriptions");

            migrationBuilder.DropTable(
                name: "logins");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}
