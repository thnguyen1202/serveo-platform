using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_refresh_tokens_role_id",
                table: "permissions");

            migrationBuilder.DropIndex(
                name: "ix_permissions_role_id",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "is_granted",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "permissions");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "roles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "roles",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "permissions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "permissions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "module",
                table: "permissions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    permission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_all_branches = table.Column<bool>(type: "bit", nullable: false),
                    joined_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenant_members_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tenant_members_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tenant_members_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_member_branches",
                columns: table => new
                {
                    tenant_member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    branch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant_member_branches", x => new { x.tenant_member_id, x.branch_id });
                    table.ForeignKey(
                        name: "fk_tenant_member_branches_branches_branch_id",
                        column: x => x.branch_id,
                        principalTable: "branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tenant_member_branches_tenant_members_tenant_member_id",
                        column: x => x.tenant_member_id,
                        principalTable: "tenant_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_roles_tenant_id_code",
                table: "roles",
                columns: new[] { "tenant_id", "code" },
                unique: true,
                filter: "[tenant_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_code",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_member_branches_branch_id",
                table: "tenant_member_branches",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_members_role_id",
                table: "tenant_members",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_members_tenant_id_user_id",
                table: "tenant_members",
                columns: new[] { "tenant_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tenant_members_user_id",
                table: "tenant_members",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "tenant_member_branches");

            migrationBuilder.DropTable(
                name: "tenant_members");

            migrationBuilder.DropIndex(
                name: "ix_roles_tenant_id_code",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "ix_permissions_code",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "code",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "description",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "code",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "description",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "module",
                table: "permissions");

            migrationBuilder.AddColumn<bool>(
                name: "is_granted",
                table: "permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "role_id",
                table: "permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                table: "permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_role_id",
                table: "permissions",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_refresh_tokens_role_id",
                table: "permissions",
                column: "role_id",
                principalTable: "refresh_tokens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
