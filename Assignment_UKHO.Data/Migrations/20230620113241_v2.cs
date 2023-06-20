using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_UKHO.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Attributes_AttributesId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AttributesId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AttributesId",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "FilesId",
                table: "Attributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_FilesId",
                table: "Attributes",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Files_FilesId",
                table: "Attributes",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Files_FilesId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_FilesId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "FilesId",
                table: "Attributes");

            migrationBuilder.AddColumn<int>(
                name: "AttributesId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Files_AttributesId",
                table: "Files",
                column: "AttributesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Attributes_AttributesId",
                table: "Files",
                column: "AttributesId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
