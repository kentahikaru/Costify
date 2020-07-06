using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Core.Application;
using Core.Application.Interfaces;

namespace Infrastructure.Persistance
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CostifyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AzureCostify")));
            services.AddScoped<ICostifyDbContext>(provider => provider.GetService<CostifyDbContext>());
        }
    }
}