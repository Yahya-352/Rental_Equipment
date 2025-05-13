using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class bd5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message_content",
                table: "Notifications",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Category_ID", "Category_Name" },
                values: new object[,]
                {
                    { 1, "Audio Equipment" },
                    { 2, "Video Equipment" },
                    { 3, "Lighting Equipment" },
                    { 4, "Computer Hardware" },
                    { 5, "Photography Equipment" }
                });
           
            migrationBuilder.InsertData(
                table: "Equipment_Availability",
                columns: new[] { "Availability_Status_ID", "Availability_Status_Name" },
                values: new object[,]
                {
                    { 1, "Available" },
                    { 2, "Rented" },
                    { 3, "Under Maintenance" },
                    { 4, "Reserved" }
                });
            
            migrationBuilder.InsertData(
                table: "Equipment_Condition",
                columns: new[] { "Condition_ID", "Condition_Name" },
                values: new object[,]
                {
                    { 1, "Excellent" },
                    { 2, "Good" },
                    { 3, "Damaged" },
                    { 4, "Under Repair" },
                    { 5, "Retired" }
                });
           

            migrationBuilder.InsertData(
                table: "Payment_Status",
                columns: new[] { "Payment_Status_ID", "Payment_Status_Name" },
                values: new object[,]
                {
                    //KEEP IT SIMPLE 
                    { 1, "Paid" },
                    { 2, "Unpaid" },
                                     //   { 2, "Pending" },

                   // { 3, "Cancelled" },
                    //{ 4, "Refunded" }
                });
           
            migrationBuilder.InsertData(
                table: "Request_Status",
                columns: new[] { "Request_Status_ID", "Request_Status_Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Completed" },
                    { 4, "Rejected" },
                    { 5, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Role_ID", "ConcurrencyStamp", "Role_Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "af1255c9-ef67-4324-beec-eae03bf14713", "User", "USER" },
                    { 2, "0e462049-6047-4e1e-ae6d-96a53cabce296", "Administrator", "ADMINISTRATOR" },
                    { 3, "c6ac44ab-cc96-489f-8e1f-3cef3b89276c", "Manager", "MANAGER" }
                });
            

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "User_ID", "AccessFailedCount", "ConcurrencyStamp", "email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role_ID", "SecurityStamp", "TwoFactorEnabled", "username" },
                values: new object[,]
                {
                    { 1, 0, "e27a817e-08e0-4b11-8dcc-786f2768ce51", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", null, false, null, "229fe287-8a02-46eb-921b-0111359ab1ca", false, "Admin" },
                    { 2, 0, "03ff4be2-d43a-4547-b98d-0f10d1477bed", "user@example.com", true, false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", null, false, null, "99dae363-9bdf-460a-a9fa-08ce79422b86", false, "User" },
                    { 3, 0, "8d7f1f35-e6da-4e2e-8f5d-29e16006d1c4", "manager@example.com", true, false, null, "MANAGER@EXAMPLE.COM", "MANAGER", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", null, false, null, "7ff9c73f-fc3f-4086-8046-b7d674990f2c", false, "Manager" },
                    { 4, 0, "7a8e57cc-e1c1-41cf-8a54-635abe9e68e6", "rehan@example.com", true, false, null, "REHAN@EXAMPLE.COM", "REHAN", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==", null, false, null, "7f714bb0-2a5f-4cad-a9e2-adf4459c2515", false, "Rehan" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 3, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "Document_ID", "Document_Name", "File_Type", "Storage_Path", "Transaction_ID", "Upload_Date", "User_ID" },
                values: new object[,]
                {
                    { 1, "RentalAgreement_1.pdf", "PDF", "/documents/agreements/RentalAgreement_1.pdf", null, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, "PaymentReceipt_1.pdf", "PDF", "/documents/receipts/PaymentReceipt_1.pdf", null, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, "DamageReport_4.pdf", "PDF", "/documents/reports/DamageReport_4.pdf", null, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "UserID_3.jpg", "JPG", "/documents/ids/UserID_3.jpg", null, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Equipment_ID", "Availability_Status_ID", "Category_ID", "Condition_ID", "Cost", "Description", "Equipment_Name", "Rental_Price" },
                values: new object[,]
                {
                    { 1, 1, 5, 1, 3899.99m, "Professional mirrorless camera", "Canon EOS R5", 150.00m },
                    { 2, 2, 5, 1, 2199.99m, "Full-frame mirrorless camera", "Sony A7III", 120.00m },
                    { 3, 1, 3, 2, 1099.99m, "LED light panel", "Aputure 300D", 75.00m },
                    { 4, 2, 1, 1, 999.99m, "Shotgun microphone", "Sennheiser MKH 416", 45.00m },
                    { 5, 3, 4, 2, 2499.99m, "16\" MacBook Pro with M1 Pro", "MacBook Pro 16\"", 120.00m },
                    { 6, 1, 2, 1, 5995.00m, "Cinema camera", "RED Komodo 6K", 350.00m },
                    { 7, 2, 2, 2, 1999.99m, "Professional gimbal stabilizer", "DJI Ronin 2", 85.00m },
                    { 8, 1, 1, 1, 349.99m, "Portable audio recorder", "Zoom H6", 35.00m },
                    { 9, 2, 3, 3, 899.99m, "Portable strobe light", "Godox AD600Pro", 60.00m },
                    { 10, 1, 4, 1, 3999.99m, "Desktop computer for video editing", "Mac Studio", 150.00m }
                });
            
            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Log_ID", "Action", "affected_data", "Exception", "Source", "Timestamp", "User_ID" },
                values: new object[,]
                {
                    { 1, "Insert", "{\"entityType\":\"RentalRequest\",\"id\":1}", null, "RentalRequestController", new DateTime(2025, 4, 14, 9, 32, 15, 0, DateTimeKind.Unspecified), 3 },
                    { 2, "Update", "{\"entityType\":\"RentalRequest\",\"id\":1,\"status\":\"Approved\"}", null, "AdminController", new DateTime(2025, 4, 14, 10, 15, 30, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "Insert", "{\"entityType\":\"RentalTransaction\",\"id\":1}", null, "TransactionController", new DateTime(2025, 4, 14, 10, 20, 45, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "Update", "{\"entityType\":\"Equipment\",\"id\":1,\"status\":\"Available\"}", null, "ReturnController", new DateTime(2025, 4, 20, 16, 45, 22, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "Insert", "{\"entityType\":\"ReturnRecord\",\"id\":1}", null, "ReturnController", new DateTime(2025, 4, 20, 16, 50, 10, 0, DateTimeKind.Unspecified), 1 },
                    { 6, "Insert", "{\"entityType\":\"Feedback\",\"id\":1}", null, "FeedbackController", new DateTime(2025, 4, 21, 14, 30, 0, 0, DateTimeKind.Unspecified), 3 }
                });
            

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Notification_ID", "Message_content", "Status", "Type", "User_ID" },
                values: new object[,]
                {
                    { 1, "Your rental request for Canon EOS R5 has been approved.", "Read", "Approval", 3 },
                    { 2, "Your rental request for Sony A7III has been approved.", "Read", "Approval", 4 },
                    { 3, "Your rental for Zoom H6 is due for return tomorrow.", "Read", "Reminder", 4 },
                    { 4, "Your rental for Aputure 300D has been approved.", "Unread", "Approval", 3 },
                    { 5, "Thank you for your feedback on the Canon EOS R5 rental.", "Unread", "Feedback", 3 },
                    { 6, "Your rental request for Godox AD600Pro has been rejected.", "Unread", "Rejection", 3 }
                });

            migrationBuilder.InsertData(
                table: "Rental_Request",
                columns: new[] { "Request_ID", "Equipment_ID", "Request_Status_ID", "return_Date", "Start_Date", "Total_Cost", "User_ID" },
                values: new object[,]
                {
                    { 1, 1, 3, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 750.00m, 3 },
                    { 2, 2, 3, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 840.00m, 4 },
                    { 3, 3, 2, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.00m, 3 },
                    { 4, 4, 2, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 315.00m, 4 },
                    { 5, 5, 1, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 840.00m, 3 },
                    { 6, 6, 1, new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2450.00m, 4 },
                    { 7, 7, 3, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 595.00m, 3 },
                    { 8, 8, 3, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 175.00m, 4 },
                    { 9, 9, 4, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 480.00m, 3 },
                    { 10, 10, 1, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1050.00m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Rental_Transaction",
                columns: new[] { "Transaction_ID", "Amount_Paid", "Deposit", "Equipment_ID", "Payment_Status_ID", "Rental_Fee", "Rental_Period", "Rental_Return_Date", "Rental_Start_Date", "Request_ID", "Total_Fee" },
                values: new object[,]
                {
                    { 1, 750.00m, 500.00m, 1, 1, 750.00m, 5, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 750.00m },
                    { 2, 840.00m, 500.00m, 2, 1, 840.00m, 7, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 840.00m },
                    { 3, 0.00m, 300.00m, 3, 2, 300.00m, 4, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 300.00m },
                    { 4, 0.00m, 250.00m, 4, 2, 315.00m, 7, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 315.00m },
                    { 5, 595.00m, 400.00m, 7, 1, 595.00m, 7, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 595.00m },
                    { 6, 175.00m, 100.00m, 8, 1, 175.00m, 5, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 175.00m }
                });

            migrationBuilder.InsertData(
                table: "Feedback",
                columns: new[] { "Feedback_ID", "Comment_Text", "Date", "IsVisible", "Rating", "time", "Transaction_ID", "User_ID" },
                values: new object[,]
                {
                    { 1, "Great camera, worked perfectly for my project!", new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, new TimeSpan(0, 14, 30, 0, 0), 1, 3 },
                    { 2, "Camera was excellent but sorry for the late return.", new DateTime(2025, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, new TimeSpan(0, 10, 15, 0, 0), 2, 4 },
                    { 3, "The stabilizer worked well but had some issues with battery life.", new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, new TimeSpan(0, 16, 45, 0, 0), 5, 3 },
                    { 4, "Very sorry about the damage, but the recorder was excellent!", new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, new TimeSpan(0, 9, 0, 0, 0), 6, 4 }
                });

            migrationBuilder.InsertData(
                table: "Return_Record",
                columns: new[] { "Return_ID", "Actual_Return_Date", "Addtional_Charges", "Condition_ID", "Late_Return_Days", "Late_Return_fees", "Transaction_ID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 1, 0, 0.00m, 1 },
                    { 2, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 1, 1, 120.00m, 2 },
                    { 3, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 2, 0, 0.00m, 5 },
                    { 4, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, 3, 0, 0.00m, 6 }
                });
              
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "Document",
                keyColumn: "Document_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Document",
                keyColumn: "Document_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Document",
                keyColumn: "Document_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Document",
                keyColumn: "Document_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment_Availability",
                keyColumn: "Availability_Status_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment_Condition",
                keyColumn: "Condition_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment_Condition",
                keyColumn: "Condition_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Feedback",
                keyColumn: "Feedback_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Feedback",
                keyColumn: "Feedback_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Feedback",
                keyColumn: "Feedback_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Feedback",
                keyColumn: "Feedback_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Log_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Log_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Log_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Log_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Log_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Log_ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Notification_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Notification_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Notification_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Notification_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Notification_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Notification_ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Payment_Status",
                keyColumn: "Payment_Status_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payment_Status",
                keyColumn: "Payment_Status_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rental_Transaction",
                keyColumn: "Transaction_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rental_Transaction",
                keyColumn: "Transaction_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Request_Status",
                keyColumn: "Request_Status_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Return_Record",
                keyColumn: "Return_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Return_Record",
                keyColumn: "Return_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Return_Record",
                keyColumn: "Return_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Return_Record",
                keyColumn: "Return_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Payment_Status",
                keyColumn: "Payment_Status_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rental_Transaction",
                keyColumn: "Transaction_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rental_Transaction",
                keyColumn: "Transaction_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rental_Transaction",
                keyColumn: "Transaction_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rental_Transaction",
                keyColumn: "Transaction_ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Request_Status",
                keyColumn: "Request_Status_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Request_Status",
                keyColumn: "Request_Status_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Role_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Role_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Role_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Category_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment_Availability",
                keyColumn: "Availability_Status_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipment_Condition",
                keyColumn: "Condition_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payment_Status",
                keyColumn: "Payment_Status_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rental_Request",
                keyColumn: "Request_ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Request_Status",
                keyColumn: "Request_Status_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Category_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Equipment_ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Request_Status",
                keyColumn: "Request_Status_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "User_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Category_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Category_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Category_ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Equipment_Availability",
                keyColumn: "Availability_Status_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipment_Availability",
                keyColumn: "Availability_Status_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipment_Condition",
                keyColumn: "Condition_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipment_Condition",
                keyColumn: "Condition_ID",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Message_content",
                table: "Notifications",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
