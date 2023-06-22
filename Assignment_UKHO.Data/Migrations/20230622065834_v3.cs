using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_UKHO.Data.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUnit_Batches_BatchId",
                table: "BusinessUnit");

            migrationBuilder.DropIndex(
                name: "IX_BusinessUnit_BatchId",
                table: "BusinessUnit");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "BusinessUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BatchId",
                table: "BusinessUnit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
