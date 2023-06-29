
using FootballClubApp.Application.Interfaces;
using FootballClubApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FootballClubAppContext>(options =>
               options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IFootballClubAppContext>(provider => provider.GetRequiredService<FootballClubAppContext>());
            services.AddScoped<DbInitializer>();

            return services;
        }
    }
}
