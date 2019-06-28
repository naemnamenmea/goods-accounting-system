using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsAccountingSystem.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Goods",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Goods",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
