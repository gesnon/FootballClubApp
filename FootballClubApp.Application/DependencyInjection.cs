using FootballClubApp.Application.Interfaces;
using FootballClubApp.Application.Services.Data;
using FootballClubApp.Application.Services.FanClubs;
using FootballClubApp.Application.Services.FootballClubs;
using FootballClubApp.Application.Services.FootballFans;
using FootballClubApp.Application.Services.FootballPlayers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddFluentValidationAutoValidation();
            services.AddTransient<IFanClubService, FanClubService>();
            services.AddTransient<IFootballClubService, FootballClubService>();
            services.AddTransient<IFootballFanService, FootballFanService>();
            services.AddTransient<IFootballPlayerService, FootballPlayerService>();
            services.AddTransient<IDataService, DataService>();
           // services.Configure<EmailConfig>(configuration.GetSection(nameof(EmailConfig)));
            //services.Configure<SecurityConfig>(configuration.GetSection(nameof(SecurityConfig)));
            return services;
        }
    }
}
