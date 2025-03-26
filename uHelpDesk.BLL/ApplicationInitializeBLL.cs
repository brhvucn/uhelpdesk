using Microsoft.Extensions.DependencyInjection;
using uHelpDesk.BLL.Contracts;

namespace uHelpDesk.BLL;

public static class ApplicationInitializeBLL
{
    public static IServiceCollection InitializeBLL(this IServiceCollection services)
    {
        services.AddScoped<ICustomerFacade, CustomerFacade>();
        return services;
    }

}