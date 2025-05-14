using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace uHelpDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CorrectTimeSpentAndResolvedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fa997ea-8f38-447d-aae6-d997404ba09a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6228d698-c72a-46a0-be6c-3969bd1f6a4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87892b6e-20a6-4cef-b1dd-12d17a9b1c34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddb0f71f-c19c-443c-a4b0-c95484fe9fc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd567a8f-2c90-4463-a700-e03202994c71");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedAt",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpent",
                table: "Tickets",
                type: "time",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04bce3a2-934a-46bd-833c-8f732f94a90e", "5eaaf089-108d-484d-94c9-b5dc6793f9cf", "Customer", "CUSTOMER" },
                    { "10bd5b43-c5d0-481c-8c19-2d07cc5d6cbc", "bb6dd4c5-7ef7-44fb-8016-c067590e0899", "Supervisor", "SUPERVISOR" },
                    { "52da6d24-a524-4fd8-a2b8-a22a05e9ce49", "4f3008ba-0a07-43a0-96a8-7f577ad96601", "Admin", "ADMIN" },
                    { "7b8cd834-abe2-436b-a52f-a088b2ea2c33", "fccfd0d0-86c1-4079-913a-a5f061ec47a8", "Auditor", "AUDITOR" },
                    { "e2290fa5-2f27-4887-ad8d-572da9b7ac9f", "2f09afa4-efc7-4109-97c2-2de0e040e463", "SupportAgent", "SUPPORTAGENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04bce3a2-934a-46bd-833c-8f732f94a90e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10bd5b43-c5d0-481c-8c19-2d07cc5d6cbc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52da6d24-a524-4fd8-a2b8-a22a05e9ce49");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b8cd834-abe2-436b-a52f-a088b2ea2c33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2290fa5-2f27-4887-ad8d-572da9b7ac9f");

            migrationBuilder.DropColumn(
                name: "ResolvedAt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TimeSpent",
                table: "Tickets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fa997ea-8f38-447d-aae6-d997404ba09a", "1923953d-8799-4354-af8a-daaf4bd76c9d", "Auditor", "AUDITOR" },
                    { "6228d698-c72a-46a0-be6c-3969bd1f6a4a", "587af609-ade7-480b-aa62-10f6eeb16f23", "Supervisor", "SUPERVISOR" },
                    { "87892b6e-20a6-4cef-b1dd-12d17a9b1c34", "b37a6bcf-0731-4e93-8e8b-504e1cfc3562", "Admin", "ADMIN" },
                    { "ddb0f71f-c19c-443c-a4b0-c95484fe9fc8", "1dbc1f74-3c32-4919-84bf-5da9cec3a08b", "SupportAgent", "SUPPORTAGENT" },
                    { "fd567a8f-2c90-4463-a700-e03202994c71", "58573d8b-d13a-4d79-948d-fa5d0f6d16ee", "Customer", "CUSTOMER" }
                });
        }
    }
}
