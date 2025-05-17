using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class db7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category_Description",
                table: "Category",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4c17d3ef-2292-4259-a909-e277caf1ce33", "d988d38d-3a1c-4c3b-b6a3-de8bc506b2d3" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dcae1509-1b8e-476c-b698-d749a567e9dc", "1d7052f5-cf83-4252-b82e-7af40956aa64" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "95dfee66-78de-44fb-930f-ea5368f6121c", "2ab551e1-2f1c-4584-95b9-c97bf99b821b" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f06f50f1-730c-4a79-8af4-0a0f95f411b5", "fb4fcb81-99a1-4261-8507-7c074ef76ff5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category_Description",
                table: "Category");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e27a817e-08e0-4b11-8dcc-786f2768ce51", "229fe287-8a02-46eb-921b-0111359ab1ca" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "03ff4be2-d43a-4547-b98d-0f10d1477bed", "99dae363-9bdf-460a-a9fa-08ce79422b86" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8d7f1f35-e6da-4e2e-8f5d-29e16006d1c4", "7ff9c73f-fc3f-4086-8046-b7d674990f2c" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7a8e57cc-e1c1-41cf-8a54-635abe9e68e6", "7f714bb0-2a5f-4cad-a9e2-adf4459c2515" });
        }
    }
}
