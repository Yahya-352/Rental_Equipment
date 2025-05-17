using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class db9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
