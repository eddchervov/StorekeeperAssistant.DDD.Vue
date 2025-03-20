using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorekeeperAssistant.Web.Migrations
{
    /// <inheritdoc />
    public partial class add_movementType_in_Moving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInventoryItems_Movings_MovingId",
                table: "WarehouseInventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInventoryItems_MovingId",
                table: "WarehouseInventoryItems");

            migrationBuilder.AddColumn<int>(
                name: "MovementType",
                table: "Movings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "Movings");

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
    }
}
