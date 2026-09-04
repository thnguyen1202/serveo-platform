using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serveo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_products",
                table: "menu_products");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_categories",
                table: "menu_categories");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "menu_products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "menu_categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_products",
                table: "menu_products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_categories",
                table: "menu_categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_products_menu_id_product_id",
                table: "menu_products",
                columns: new[] { "menu_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_menu_categories_menu_id_category_id",
                table: "menu_categories",
                columns: new[] { "menu_id", "category_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_products",
                table: "menu_products");

            migrationBuilder.DropIndex(
                name: "ix_menu_products_menu_id_product_id",
                table: "menu_products");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_categories",
                table: "menu_categories");

            migrationBuilder.DropIndex(
                name: "ix_menu_categories_menu_id_category_id",
                table: "menu_categories");

            migrationBuilder.DropColumn(
                name: "id",
                table: "menu_products");

            migrationBuilder.DropColumn(
                name: "id",
                table: "menu_categories");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_products",
                table: "menu_products",
                columns: new[] { "menu_id", "product_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_categories",
                table: "menu_categories",
                columns: new[] { "menu_id", "category_id" });
        }
    }
}
