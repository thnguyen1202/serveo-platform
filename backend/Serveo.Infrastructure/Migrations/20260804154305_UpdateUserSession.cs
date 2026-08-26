using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "browser",
                table: "user_sessions");

            migrationBuilder.DropColumn(
                name: "device_name",
                table: "user_sessions");

            migrationBuilder.DropColumn(
                name: "operating_system",
                table: "user_sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "browser",
                table: "user_sessions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "device_name",
                table: "user_sessions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "operating_system",
                table: "user_sessions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }
    }
}
