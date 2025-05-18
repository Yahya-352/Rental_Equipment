using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class FixUserTableStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dc28b3b5-1c3b-4dfe-bf19-e20cebb6397c", "fa2c340c-efc4-43ef-a439-e5abac61c48b" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c3d1172b-73e7-4d9b-a32a-4c62329e79eb", "32efc476-1128-4a87-baa3-5bb7feb573a1" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c2316639-0d80-4e67-a117-4c7d3fc0d792", "7d7fb0fc-1573-4747-a8ab-8e47a67c8c08" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a7433587-c663-4416-b61f-61d2f47c8f4f", "a15ff2ac-f221-4cfb-9a24-2744be11a517" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
