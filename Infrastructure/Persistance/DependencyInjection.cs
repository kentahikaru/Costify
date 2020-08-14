using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Core.Application;
using Core.Application.Interfaces;
using Infrastructure.Persistance.Repositories;

namespace Infrastructure.Persistance
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CostifyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AzureCostify")));
            services.AddScoped<ICostifyDbContext>(provider => provider.GetService<CostifyDbContext>());

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<ICostRepository,CostRepository>();
            services.AddTransient<ICategoryRepository,CategoryReposoitory>();
        }
    }
}