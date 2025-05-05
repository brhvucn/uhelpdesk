using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uHelpDesk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = "admin-user-id",
                UserName = "admin@yourdomain.com",
                NormalizedUserName = "ADMIN@YOURDOMAIN.COM",
                Email = "admin@yourdomain.com",
                NormalizedEmail = "ADMIN@YOURDOMAIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null, "SecureP@ssword123")
            };

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] {
            "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
            "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp"
                },
                values: new object[] {
            adminUser.Id, adminUser.UserName, adminUser.NormalizedUserName,
            adminUser.Email, adminUser.NormalizedEmail,
            adminUser.EmailConfirmed, adminUser.PasswordHash,
            adminUser.SecurityStamp, adminUser.ConcurrencyStamp
                });
        }
    }
}
