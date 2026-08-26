using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    device_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    client_type = table.Column<int>(type: "int", nullable: false),
                    device_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    browser = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    operating_system = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ip_address = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    user_agent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    refresh_token_hash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    previous_token_hash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    last_activity_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    absolute_expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    revoked_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_sessions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_sessions_user_id_device_id_client_type",
                table: "user_sessions",
                columns: new[] { "user_id", "device_id", "client_type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_sessions");
        }
    }
}
