using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HygeeaMind.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        // Modifică aici: adaugă '?' după 'Exception'
        public override async Task OnDisconnectedAsync(Exception? exception) // <--- Aici e modificarea
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}