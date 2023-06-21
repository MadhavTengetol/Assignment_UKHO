using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_UKHO.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Batches_BatchId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Files_FilesId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Acl_AclId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_AclId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_FilesId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AclId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "FilesId",
                table: "Attributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "BatchId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BatchId",
                table: "Acl",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FileAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttributes_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acl_BatchId",
                table: "Acl",
                column: "BatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttributes_FilesId",
                table: "FileAttributes",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acl_Batches_BatchId",
                table: "Acl",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Batches_BatchId",
                table: "Attributes",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acl_Batches_BatchId",
                table: "Acl");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Batches_BatchId",
                table: "Attributes");

            migrationBuilder.DropTable(
                name: "FileAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Acl_BatchId",
                table: "Acl");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Acl");

            migrationBuilder.AddColumn<int>(
                name: "AclId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "BatchId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "FilesId",
                table: "Attributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_AclId",
                table: "Batches",
                column: "AclId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_FilesId",
                table: "Attributes",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Batches_BatchId",
                table: "Attributes",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Files_FilesId",
                table: "Attributes",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Acl_AclId",
                table: "Batches",
                column: "AclId",
                principalTable: "Acl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
