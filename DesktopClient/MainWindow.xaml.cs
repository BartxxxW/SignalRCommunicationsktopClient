using System.Text;
using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7266/messageHub")
                .Build();

            connection.Closed += async (error) =>
            {
                StatusLabel.Content = "Disconnected";
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.Reconnected += async (param) =>
            {
                StatusLabel.Content = "Connected Again ";
            };

            Connect();

        }

        public async void Connect()
        {
            connection.On<string>("ReceiveWebMessage", (message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    ReceivedMessage.Content= message;
                });
            });

            try
            {
                await connection.StartAsync();
                StatusLabel.Content = "Connected";
            }
            catch (Exception ex)
            {
                StatusLabel.Content = "Not Connected";
            }
        }

        private async  void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendToWebClient",
                     SendMessage.Text);
            }
            catch (Exception ex)
            {
                ReceivedMessage.Content = $"Errod during sending message : {ex}";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}