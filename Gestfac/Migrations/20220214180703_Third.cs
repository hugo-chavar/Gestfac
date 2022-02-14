using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestfac.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPriceUpdateId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrentPriceUpdateId",
                table: "Products",
                column: "CurrentPriceUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PriceUpdates_CurrentPriceUpdateId",
                table: "Products",
                column: "CurrentPriceUpdateId",
                principalTable: "PriceUpdates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PriceUpdates_CurrentPriceUpdateId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CurrentPriceUpdateId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrentPriceUpdateId",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "CurrentPrice",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
