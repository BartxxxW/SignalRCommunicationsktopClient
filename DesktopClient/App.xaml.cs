using DesktopClient.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        protected override async void OnStartup(StartupEventArgs e)
        {

            await AppHost!.StartAsync();
            var mainForm = AppHost.Services.GetRequiredService<MainWindow>();
            mainForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext,services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton(s=>new HubConnectionBuilder()
                    .WithUrl(ConfigurationManager.AppSettings["ApiUrl"])
                    .Build());
                    services.AddSingleton<IHubConnectionService, HubConnectionService>();
                })
                .Build();

            
        }
    }

}
