using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class db32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f4056825-6612-41b0-ac9e-078b15781387", "ba969a24-4da9-4dd6-bbbd-0eb954f0bdb0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c1a2e459-85f4-41da-a696-08f4a760b5b2", "6ec867dc-3158-422d-bcb2-3e751fdb6d78" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2de4190f-76c6-453b-a2f0-3bf6670fd8c1", "474cf86d-cf5e-4603-a98c-e0ad7c6acaaa" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "801dd6ea-d64b-469d-b5c2-21563ac10481", "1ff705a1-2855-4a09-b6c1-001742fe84dc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9f246da7-0b99-466a-9e8d-86e67ed36129", "06f32aa6-d818-4be5-af71-35a417014200" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "00b69fe9-cfc7-44ea-aea3-a925d69f07cc", "ecd0831e-ccd3-4047-8467-e9703ff08ea2" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c92580cb-3ade-458e-944a-ea2f626f4a51", "c602a009-d15a-4670-a2f9-78bfd69d2171" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "676c8f65-5e5b-490f-8ebd-e44761e0e1fe", "5d3ced29-222b-40f2-a752-602a08f66eb4" });
        }
    }
}
