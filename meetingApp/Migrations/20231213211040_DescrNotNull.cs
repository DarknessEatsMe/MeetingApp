using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meetingApp.Migrations
{
    /// <inheritdoc />
    public partial class DescrNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "photo_adr",
                table: "photos",
                type: "bytea",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8000)",
                oldMaxLength: 8000);

            migrationBuilder.AlterColumn<string>(
                name: "decsr",
                table: "descriptions",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "photo_adr",
                table: "photos",
                type: "character varying(8000)",
                maxLength: 8000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "decsr",
                table: "descriptions",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
