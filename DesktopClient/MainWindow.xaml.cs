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
using DesktopClient.Services;
using System.Reflection;
using System.Windows.Interop;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IHubConnectionService hubConnectionService;

        public MainWindow(IHubConnectionService _hubConnectionService)
        {
            InitializeComponent();

            hubConnectionService = _hubConnectionService;
            hubConnectionService.SetupHubEvents();
            hubConnectionService.connectionStatusEvent += ConnectionStatusHandler;
            hubConnectionService.receivedMessageEvent += ReceivedMsgHandler;

            hubConnectionService.Connect();
        }

        public async void ReceivedMsgHandler(object sender, string msg)
        {
            this.Dispatcher.Invoke(() =>
            {
                ReceivedMessage.Content = msg;
            });
        }

        public async void ConnectionStatusHandler(object sender, string msg)
        {
            this.Dispatcher.Invoke(() =>
            {
                StatusLabel.Content = msg;
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text;

            if (hubConnectionService != null)
            {
                hubConnectionService.SendMessage(newText);
            }
            
        }

    }
}