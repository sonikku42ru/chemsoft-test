using System;
using System.IO;
using System.Windows;
using ChemsoftTest.Core.Database;
using ChemsoftTest.Core.Database.Repositories;
using ChemsoftTest.Core.DataHandlers;
using ChemsoftTest.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChemsoftTest.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static IHost _host;
        public IConfiguration Configuration { get; private set; }

        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.Run();
        }
        
        public static IHost CreateHostBuilder(string[] args, string connection) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                // Регистрация контекста БД
                services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));
                
                // Регистрация репозитория БД
                services.AddScoped<PersonRepository>();

                services.AddScoped<PersonDataHandler>();
                
                // Регистрация окон
                services.AddSingleton<MainWindow>();
                
                // Регистрация VM
                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            _host = CreateHostBuilder(e.Args, Configuration.GetConnectionString("AppDb"));
            base.OnStartup(e);
            var mainWindow = _host.Services.GetService<MainWindow>()!;
            mainWindow.Show();
        }
    }
}