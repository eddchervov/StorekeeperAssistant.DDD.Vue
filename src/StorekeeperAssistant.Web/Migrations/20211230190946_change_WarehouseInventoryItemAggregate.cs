using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorekeeperAssistant.Web.Migrations;

public partial class change_WarehouseInventoryItemAggregate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_WarehouseInventoryItems_MovingId",
            table: "WarehouseInventoryItems",
            column: "MovingId");

        migrationBuilder.AddForeignKey(
            name: "FK_WarehouseInventoryItems_Movings_MovingId",
            table: "WarehouseInventoryItems",
            column: "MovingId",
            principalTable: "Movings",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_WarehouseInventoryItems_Movings_MovingId",
            table: "WarehouseInventoryItems");

        migrationBuilder.DropIndex(
            name: "IX_WarehouseInventoryItems_MovingId",
            table: "WarehouseInventoryItems");
    }
}
