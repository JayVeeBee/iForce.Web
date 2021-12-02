using iForce.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace iForce.App
{
    public static class DependencyInjection
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services
                .AddScoped<ICustomerAccess, CustomerAccess>()
                .AddScoped<IVehicleAccess, VehicleAccess>();
        }
    }
}
