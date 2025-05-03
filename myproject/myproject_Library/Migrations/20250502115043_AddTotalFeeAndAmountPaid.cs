using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class AddTotalFeeAndAmountPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount_Paid",
                table: "Rental_Transaction",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total_Fee",
                table: "Rental_Transaction",
                type: "numeric(18,0)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount_Paid",
                table: "Rental_Transaction");

            migrationBuilder.DropColumn(
                name: "Total_Fee",
                table: "Rental_Transaction");
        }
    }
}
