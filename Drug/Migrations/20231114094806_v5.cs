using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drug.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DosageForm",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugGenericName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugImage",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Carts",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DosageForm",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DrugGenericName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DrugImage",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DrugName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Carts");
        }
    }
}
