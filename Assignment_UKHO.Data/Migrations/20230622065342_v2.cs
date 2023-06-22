using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_UKHO.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessUnit",
                table: "Batches");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnit_BatchId",
                table: "BusinessUnit",
                column: "BatchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessUnit_Batches_BatchId",
                table: "BusinessUnit",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUnit_Batches_BatchId",
                table: "BusinessUnit");

            migrationBuilder.DropIndex(
                name: "IX_BusinessUnit_BatchId",
                table: "BusinessUnit");

            migrationBuilder.AddColumn<string>(
                name: "BusinessUnit",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
