using FootballClubApp.Application;
using FootballClubApp.Infrastructure;
using FootballClubApp.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public IHost Host { get; set; }
        public IConfiguration Configuration { get; set; }
        public App()
        {
            Host = new HostBuilder().ConfigureServices((context, services) =>
            {
                ConfigureServices(services);                
            }).Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = Host.Services.GetService<MainWindow>();

            mainWindow.Show();
        }

    }
}
