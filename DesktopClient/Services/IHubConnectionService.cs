
namespace DesktopClient.Services
{
    public interface IHubConnectionService
    {
        event EventHandler<string> connectionStatusEvent;
        event EventHandler<string> receivedMessageEvent;

        void Connect();
        void OnConnectionStatusEvent(object sender, string msg);
        void OnReceivedMessageEvent(object sender, string msg);
        void SendMessage(string message);
        void SetupHubEvents();
    }
}