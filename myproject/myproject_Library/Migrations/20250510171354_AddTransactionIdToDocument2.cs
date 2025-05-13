using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class AddTransactionIdToDocument2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Transaction_ID",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_Transaction_ID",
                table: "Document",
                column: "Transaction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Rental_Transaction_Transaction_ID",
                table: "Document",
                column: "Transaction_ID",
                principalTable: "Rental_Transaction",
                principalColumn: "Transaction_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Rental_Transaction_Transaction_ID",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_Transaction_ID",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Transaction_ID",
                table: "Document");
        }
    }
}
