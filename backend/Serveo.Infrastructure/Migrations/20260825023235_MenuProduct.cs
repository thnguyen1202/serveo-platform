using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_items_products_product_id",
                table: "menu_items");

            migrationBuilder.AddColumn<int>(
                name: "display_order",
                table: "menu_items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_visible",
                table: "menu_items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "price_override",
                table: "menu_items",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_items_products_product_id",
                table: "menu_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_items_products_product_id",
                table: "menu_items");

            migrationBuilder.DropColumn(
                name: "display_order",
                table: "menu_items");

            migrationBuilder.DropColumn(
                name: "is_visible",
                table: "menu_items");

            migrationBuilder.DropColumn(
                name: "price_override",
                table: "menu_items");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_items_products_product_id",
                table: "menu_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id");
        }
    }
}
