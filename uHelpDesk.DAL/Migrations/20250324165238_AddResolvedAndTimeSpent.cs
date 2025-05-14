using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace uHelpDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddResolvedAndTimeSpent : Migration
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
                    { "5fa997ea-8f38-447d-aae6-d997404ba09a", "1923953d-8799-4354-af8a-daaf4bd76c9d", "Auditor", "AUDITOR" },
                    { "6228d698-c72a-46a0-be6c-3969bd1f6a4a", "587af609-ade7-480b-aa62-10f6eeb16f23", "Supervisor", "SUPERVISOR" },
                    { "87892b6e-20a6-4cef-b1dd-12d17a9b1c34", "b37a6bcf-0731-4e93-8e8b-504e1cfc3562", "Admin", "ADMIN" },
                    { "ddb0f71f-c19c-443c-a4b0-c95484fe9fc8", "1dbc1f74-3c32-4919-84bf-5da9cec3a08b", "SupportAgent", "SUPPORTAGENT" },
                    { "fd567a8f-2c90-4463-a700-e03202994c71", "58573d8b-d13a-4d79-948d-fa5d0f6d16ee", "Customer", "CUSTOMER" }
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
    }
}
