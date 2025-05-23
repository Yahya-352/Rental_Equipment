﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject_Library.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_ID);
                });

            migrationBuilder.CreateTable(
                name: "Equipment_Availability",
                columns: table => new
                {
                    Availability_Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Availability_Status_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment_Availability", x => x.Availability_Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "Equipment_Condition",
                columns: table => new
                {
                    Condition_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition_ID", x => x.Condition_ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Status",
                columns: table => new
                {
                    Payment_Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payment_Status_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Status", x => x.Payment_Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "Request_Status",
                columns: table => new
                {
                    Request_Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_Status_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request_Status", x => x.Request_Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Equipment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Equipment_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Rental_Price = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Availability_Status_ID = table.Column<int>(type: "int", nullable: true),
                    Category_ID = table.Column<int>(type: "int", nullable: true),
                    Condition_ID = table.Column<int>(type: "int", nullable: true),
                    Late_Penalty_Percentage = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Equipment_ID);
                    table.ForeignKey(
                        name: "FK_Equipment_Category",
                        column: x => x.Category_ID,
                        principalTable: "Category",
                        principalColumn: "Category_ID");
                    table.ForeignKey(
                        name: "FK_Equipment_Condition_ID",
                        column: x => x.Condition_ID,
                        principalTable: "Equipment_Condition",
                        principalColumn: "Condition_ID");
                    table.ForeignKey(
                        name: "FK_Equipment_Equipment_Availability",
                        column: x => x.Availability_Status_ID,
                        principalTable: "Equipment_Availability",
                        principalColumn: "Availability_Status_ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Role_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_ID);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.Role_ID,
                        principalTable: "Role",
                        principalColumn: "Role_ID");
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Document_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Upload_Date = table.Column<DateTime>(type: "date", nullable: true),
                    File_Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Storage_Path = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Document_ID);
                    table.ForeignKey(
                        name: "FK_Document_User",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Log_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Exception = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    affected_data = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Source = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Log_ID);
                    table.ForeignKey(
                        name: "FK_Logs_User",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Notification_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message_content = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Notification_ID);
                    table.ForeignKey(
                        name: "FK_Notifications_User",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Rental_Request",
                columns: table => new
                {
                    Request_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Date = table.Column<DateTime>(type: "date", nullable: true),
                    return_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Total_Cost = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    Equipment_ID = table.Column<int>(type: "int", nullable: true),
                    Request_Status_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental_Request", x => x.Request_ID);
                    table.ForeignKey(
                        name: "FK_Rental_Request_Equipment",
                        column: x => x.Equipment_ID,
                        principalTable: "Equipment",
                        principalColumn: "Equipment_ID");
                    table.ForeignKey(
                        name: "FK_Rental_Request_Request_Status",
                        column: x => x.Request_Status_ID,
                        principalTable: "Request_Status",
                        principalColumn: "Request_Status_ID");
                    table.ForeignKey(
                        name: "FK_Rental_Request_User",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Rental_Transaction",
                columns: table => new
                {
                    Transaction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rental_Start_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Rental_Return_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Rental_Period = table.Column<int>(type: "int", nullable: true),
                    Rental_Fee = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Deposit = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Equipment_ID = table.Column<int>(type: "int", nullable: true),
                    Request_ID = table.Column<int>(type: "int", nullable: true),
                    Payment_Status_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental_Transaction", x => x.Transaction_ID);
                    table.ForeignKey(
                        name: "FK_Rental_Transaction_Equipment",
                        column: x => x.Equipment_ID,
                        principalTable: "Equipment",
                        principalColumn: "Equipment_ID");
                    table.ForeignKey(
                        name: "FK_Rental_Transaction_Payment_Status",
                        column: x => x.Payment_Status_ID,
                        principalTable: "Payment_Status",
                        principalColumn: "Payment_Status_ID");
                    table.ForeignKey(
                        name: "FK_Rental_Transaction_Rental_Request",
                        column: x => x.Request_ID,
                        principalTable: "Rental_Request",
                        principalColumn: "Request_ID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Feedback_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Comment_Text = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    Transaction_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Feedback_ID);
                    table.ForeignKey(
                        name: "FK_Feedback_Rental_Transaction",
                        column: x => x.Transaction_ID,
                        principalTable: "Rental_Transaction",
                        principalColumn: "Transaction_ID");
                    table.ForeignKey(
                        name: "FK_Feedback_User",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Return_Record",
                columns: table => new
                {
                    Return_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Actual_Return_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Late_Return_fees = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Addtional_Charges = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Condition_ID = table.Column<int>(type: "int", nullable: true),
                    Transaction_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return_Record", x => x.Return_ID);
                    table.ForeignKey(
                        name: "FK_Return_Record_Condition_ID",
                        column: x => x.Condition_ID,
                        principalTable: "Equipment_Condition",
                        principalColumn: "Condition_ID");
                    table.ForeignKey(
                        name: "FK_Return_Record_Rental_Transaction",
                        column: x => x.Transaction_ID,
                        principalTable: "Rental_Transaction",
                        principalColumn: "Transaction_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_User_ID",
                table: "Document",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Availability_Status_ID",
                table: "Equipment",
                column: "Availability_Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Category_ID",
                table: "Equipment",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Condition_ID",
                table: "Equipment",
                column: "Condition_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Transaction_ID",
                table: "Feedback",
                column: "Transaction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_User_ID",
                table: "Feedback",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_User_ID",
                table: "Logs",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_User_ID",
                table: "Notifications",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Request_Equipment_ID",
                table: "Rental_Request",
                column: "Equipment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Request_Request_Status_ID",
                table: "Rental_Request",
                column: "Request_Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Request_User_ID",
                table: "Rental_Request",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Transaction_Equipment_ID",
                table: "Rental_Transaction",
                column: "Equipment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Transaction_Payment_Status_ID",
                table: "Rental_Transaction",
                column: "Payment_Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Transaction_Request_ID",
                table: "Rental_Transaction",
                column: "Request_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Return_Record_Condition_ID",
                table: "Return_Record",
                column: "Condition_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Return_Record_Transaction_ID",
                table: "Return_Record",
                column: "Transaction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_ID",
                table: "User",
                column: "Role_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Return_Record");

            migrationBuilder.DropTable(
                name: "Rental_Transaction");

            migrationBuilder.DropTable(
                name: "Payment_Status");

            migrationBuilder.DropTable(
                name: "Rental_Request");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Request_Status");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Equipment_Condition");

            migrationBuilder.DropTable(
                name: "Equipment_Availability");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
