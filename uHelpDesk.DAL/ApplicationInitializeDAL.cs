using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.DAL
{
    public static class ApplicationInitializeDAL
    {
        public static IServiceCollection InitializeDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<uHelpDeskDbContext>(options =>
    options.UseSqlServer(connectionString,
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<uHelpDeskDbContext>();



            //services.AddScoped<IAsyncInventoryBrandRepository, InventoryBrandRepository>();
            return services;
        }
    }
}
