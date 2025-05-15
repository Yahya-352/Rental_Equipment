using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class AddLatePenaltyPercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "73c7d7ca-069a-4cd3-86d2-70c8d666d14b", "3524ee41-44b2-4211-917c-9f91bb3b41e0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3652f379-e9d8-42c6-bc01-dd495a1d61e6", "977884b6-4cfb-4572-9279-ef71db1c93f0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "32d0ed4c-ea69-4890-a108-d5d4c5c1cd9d", "506faaa7-9a54-45e9-810b-519e21eaf1d8" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "83cf17ca-e168-4516-8b41-e65dd888f859", "0f42fa51-3e9c-4d98-829b-e42ba1269012" });
        }
    }
}
