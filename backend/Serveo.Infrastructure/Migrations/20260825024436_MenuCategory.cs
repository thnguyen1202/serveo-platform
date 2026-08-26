using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_items_menus_menu_id",
                table: "menu_items");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_items_products_product_id",
                table: "menu_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_items",
                table: "menu_items");

            migrationBuilder.RenameTable(
                name: "menu_items",
                newName: "menu_products");

            migrationBuilder.RenameIndex(
                name: "ix_menu_items_product_id",
                table: "menu_products",
                newName: "ix_menu_products_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_products",
                table: "menu_products",
                columns: new[] { "menu_id", "product_id" });

            migrationBuilder.CreateTable(
                name: "menu_categories",
                columns: table => new
                {
                    menu_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    is_visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_categories", x => new { x.menu_id, x.category_id });
                    table.ForeignKey(
                        name: "fk_menu_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_menu_categories_menus_menu_id",
                        column: x => x.menu_id,
                        principalTable: "menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_menu_categories_category_id",
                table: "menu_categories",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_products_menus_menu_id",
                table: "menu_products",
                column: "menu_id",
                principalTable: "menus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_products_products_product_id",
                table: "menu_products",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_products_menus_menu_id",
                table: "menu_products");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_products_products_product_id",
                table: "menu_products");

            migrationBuilder.DropTable(
                name: "menu_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_products",
                table: "menu_products");

            migrationBuilder.RenameTable(
                name: "menu_products",
                newName: "menu_items");

            migrationBuilder.RenameIndex(
                name: "ix_menu_products_product_id",
                table: "menu_items",
                newName: "ix_menu_items_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_items",
                table: "menu_items",
                columns: new[] { "menu_id", "product_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_menu_items_menus_menu_id",
                table: "menu_items",
                column: "menu_id",
                principalTable: "menus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_items_products_product_id",
                table: "menu_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
