using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Project_Managment_API.Hubs
{
    public class ChatHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            // Add any necessary logic when a client connects to the hub
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Add any necessary logic when a client disconnects from the hub
            return base.OnDisconnectedAsync(exception);
        }
    }
}
