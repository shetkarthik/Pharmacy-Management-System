using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drug.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Drugs",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Drugs",
                newName: "Price");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Drugs",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Drugs",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Drugs",
                newName: "price");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Drugs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
