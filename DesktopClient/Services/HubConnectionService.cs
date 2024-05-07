using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace DesktopClient.Services
{
    public class HubConnectionService : IHubConnectionService
    {
        public event EventHandler<string> connectionStatusEvent;
        public event EventHandler<string> receivedMessageEvent;

        public void OnConnectionStatusEvent(object sender, string msg)
        {
            connectionStatusEvent.Invoke(sender, msg);
        }

        public void OnReceivedMessageEvent(object sender, string msg)
        {
            receivedMessageEvent.Invoke(sender, msg);
        }

        HubConnection connection;
        public HubConnectionService(HubConnection _connection)
        {
            connection = _connection;
        }

        public async void SetupHubEvents()
        {
            connection.Closed += async (error) =>
            {

                OnConnectionStatusEvent(this, "Disconnected");
                await TryConnect();
            };

            connection.Reconnected += async (param) =>
            {
                OnConnectionStatusEvent(this, "Connected Again");
            };
        }

        public async Task TryConnect(int attempts=150)
        {
            await Task.Delay(2000);
            try
            {
                await connection.StartAsync();
                OnConnectionStatusEvent(this, "Connected");
            }
            catch
            {
                attempts--;
                if (attempts <= 0)
                    return;

                await TryConnect(attempts);
            }
        }

        public async void Connect()
        {
            connection.On<string>("ReceiveWebMessage", (message) =>
            {
                OnReceivedMessageEvent(this, message);
            });

            await TryConnect();
        }

        public async void SendMessage(string message)
        {
            try
            {
                await connection.InvokeAsync("SendToWebClient",
                     message);
            }
            catch (Exception ex)
            {
                OnReceivedMessageEvent(this, $"Error during sending message : {ex}");
            }
        }
    }
}
