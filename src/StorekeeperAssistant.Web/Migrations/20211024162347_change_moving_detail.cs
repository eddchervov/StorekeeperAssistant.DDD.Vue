using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorekeeperAssistant.Web.Migrations
{
    public partial class change_moving_detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovingDetails_MovingId",
                table: "MovingDetails",
                column: "MovingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovingDetails_Movings_MovingId",
                table: "MovingDetails",
                column: "MovingId",
                principalTable: "Movings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovingDetails_Movings_MovingId",
                table: "MovingDetails");

            migrationBuilder.DropIndex(
                name: "IX_MovingDetails_MovingId",
                table: "MovingDetails");
        }
    }
}
