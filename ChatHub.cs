using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Project_Managment_API.Hubs
{
    public class ChatHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
