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
        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine( "here?");
        }
        public App()
        {
            Console.WriteLine("here we ara");
        }
    }

}
