using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "Doc", "Name", "Type", "UpdatedAt" },
                values: new object[] { 1, "Rua 1", "11111111111", "Daniel", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "Doc", "Name", "Type", "UpdatedAt" },
                values: new object[] { 2, "Rua 2", "22222222222", "Ana Karine", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "PersonId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "PersonId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "(11) 0011-1406" },
                    { 2, 1, "(22) 2201-2226" },
                    { 3, 2, "(00) 0001-0101" }
                });

            migrationBuilder.InsertData(
                table: "TransactionLog",
                columns: new[] { "Id", "AccountId", "BalanceAfter", "TransactionId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1000m, 0, 1000m },
                    { 3, 1, 901m, 0, 99m },
                    { 2, 2, 1000m, 0, 1000m },
                    { 4, 2, 1099m, 0, 99m }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountFromId", "AccountToId", "Value" },
                values: new object[,]
                {
                    { 1, null, 1, 1000m },
                    { 2, null, 2, 1000m },
                    { 3, 1, 2, 99m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransactionLog",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransactionLog",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransactionLog",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransactionLog",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
