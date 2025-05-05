using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace uHelpDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeSpentToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedAt",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSpent",
                table: "Tickets",
                nullable: true);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e9283e4-d9df-4cc8-9cc0-9f84a567f33d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7555c30e-74ea-477c-b341-055c4c3b9fb7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83ebc7bc-f00e-4906-9272-804f86929301");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8605716b-0829-4703-b9f0-ddc1d08725a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd0f11fb-23b1-4823-b61e-e5cec3232728");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e5bb402-134b-4b91-96e5-0f799898f57d", "48bfc5b1-969d-4e0d-8a2d-58772a57ede6", "Auditor", "AUDITOR" },
                    { "24ebceae-51b1-4a94-b6cf-435958b1fbf0", "724c81e3-5cf8-4878-8a1c-11bc5cff1f65", "Customer", "CUSTOMER" },
                    { "52f3ec04-5f04-4c81-881f-6790245757a6", "c9b0839a-9365-47ba-85a4-bc946d175f21", "SupportAgent", "SUPPORTAGENT" },
                    { "64dd916e-c73a-409a-be0f-0ccd06c9ab67", "fcf8bbad-b75f-41fb-bfb6-348a9c1b5c0a", "Admin", "ADMIN" },
                    { "f6f77548-40cb-4245-8fd1-d80f2864776c", "4773f6b6-b445-41ae-9d40-fd3ded217dc1", "Supervisor", "SUPERVISOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedAt",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSpent",
                table: "Tickets",
                nullable: true);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e5bb402-134b-4b91-96e5-0f799898f57d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24ebceae-51b1-4a94-b6cf-435958b1fbf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52f3ec04-5f04-4c81-881f-6790245757a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64dd916e-c73a-409a-be0f-0ccd06c9ab67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6f77548-40cb-4245-8fd1-d80f2864776c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e9283e4-d9df-4cc8-9cc0-9f84a567f33d", "0de805b4-78ca-4d56-94dd-0c52b15f6a9f", "Auditor", "AUDITOR" },
                    { "7555c30e-74ea-477c-b341-055c4c3b9fb7", "33f6e5d7-87f2-482a-9ce4-6b89975896fb", "Customer", "CUSTOMER" },
                    { "83ebc7bc-f00e-4906-9272-804f86929301", "6a1bb694-e282-441f-bc70-7d2efe5d8f9c", "Admin", "ADMIN" },
                    { "8605716b-0829-4703-b9f0-ddc1d08725a4", "5310a0e1-6722-4081-a6b9-78f57d879426", "Supervisor", "SUPERVISOR" },
                    { "bd0f11fb-23b1-4823-b61e-e5cec3232728", "50d20140-3193-4b7b-9387-65ba6156ac87", "SupportAgent", "SUPPORTAGENT" }
                });
        }
    }
}
