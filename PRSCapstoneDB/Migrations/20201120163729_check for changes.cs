using Microsoft.EntityFrameworkCore.Migrations;

namespace PRSCapstoneDB.Migrations
{
    public partial class checkforchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Products_ProductId",
                table: "RequestLine");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Requests_RequestId",
                table: "RequestLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestLine",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine");

            migrationBuilder.RenameTable(
                name: "RequestLine",
                newName: "RequestLines");

            migrationBuilder.RenameIndex(
                name: "IX_RequestLine_ProductId",
                table: "RequestLines",
                newName: "IX_RequestLines_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestLines",
                table: "RequestLines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLines_RequestId",
                table: "RequestLines",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLines_Products_ProductId",
                table: "RequestLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLines_Requests_RequestId",
                table: "RequestLines",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLines_Products_ProductId",
                table: "RequestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLines_Requests_RequestId",
                table: "RequestLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestLines",
                table: "RequestLines");

            migrationBuilder.DropIndex(
                name: "IX_RequestLines_RequestId",
                table: "RequestLines");

            migrationBuilder.RenameTable(
                name: "RequestLines",
                newName: "RequestLine");

            migrationBuilder.RenameIndex(
                name: "IX_RequestLines_ProductId",
                table: "RequestLine",
                newName: "IX_RequestLine_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestLine",
                table: "RequestLine",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Products_ProductId",
                table: "RequestLine",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Requests_RequestId",
                table: "RequestLine",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
