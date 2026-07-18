#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductBusiness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_branches_branch_id",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "branch_id",
                table: "products",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "ix_products_branch_id",
                table: "products",
                newName: "ix_products_business_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_businesses_business_id",
                table: "products",
                column: "business_id",
                principalTable: "businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_businesses_business_id",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "products",
                newName: "branch_id");

            migrationBuilder.RenameIndex(
                name: "ix_products_business_id",
                table: "products",
                newName: "ix_products_branch_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_branches_branch_id",
                table: "products",
                column: "branch_id",
                principalTable: "branches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
