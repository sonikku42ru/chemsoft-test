﻿using System;
using System.Windows;
using ChemsoftTest.UI.Views;
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

        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.Run();
        }
        
        public static IHost CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                
                // Регистрация окон
                services.AddSingleton<MainWindow>();
                
                // Регистрация VM
                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = CreateHostBuilder(e.Args);
            base.OnStartup(e);
            var mainWindow = _host.Services.GetService<MainWindow>()!;
            mainWindow.Show();
        }
    }
}