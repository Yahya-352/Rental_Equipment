using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Late_Penalty_Percentage",
                table: "Equipment");

            migrationBuilder.AlterColumn<string>(
                name: "Comment_Text",
                table: "Feedback",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment_Text",
                table: "Feedback",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Late_Penalty_Percentage",
                table: "Equipment",
                type: "numeric(18,0)",
                nullable: true);
        }
    }
}
