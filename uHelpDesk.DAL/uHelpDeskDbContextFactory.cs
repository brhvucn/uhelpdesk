using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace uHelpDesk.DAL
{
    public class uHelpDeskDbContextFactory : IDesignTimeDbContextFactory<uHelpDeskDbContext>
    {
        public uHelpDeskDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "uHelpDesk.Admin");

            if (!Directory.Exists(basePath))
            {
                throw new DirectoryNotFoundException($"Could not find uHelpDesk.Admin at {basePath}");
            }

            // Gets the environment (Development, Production, etc.)
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json") // This line attempts to load appsettings.json
                .AddJsonFile($"appsettings.{environmentName}.json", optional : true)
                .Build();

            // This creates options for the DbContext, telling it to use the SQL server.
            var optionsBuilder = new DbContextOptionsBuilder<uHelpDeskDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            var dbContext = new uHelpDeskDbContext(optionsBuilder.Options);

            dbContext.Database.EnsureCreated();

            // This creates and returns a new instance of uHelpDeskBbContext, along with these options.
            return new uHelpDeskDbContext(optionsBuilder.Options);
            
        }
    }

}
