using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project_Management_API.Data;
using Project_Managment_API.Hubs;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Managment_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ProjectManagementDbContext _dbContext;
        
        private readonly IApplicationUserRepository _userRepository;



        public ChatController(IHubContext<ChatHub> hubContext, ProjectManagementDbContext dbContext, IApplicationUserRepository userRepository)
        {
            _hubContext = hubContext;
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage(string receiverEmail, string message)
        {
            var senderEmail = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            var tenantId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "TenantId")?.Value;

            // Verify that the sender and receiver belong to the same tenant


            var sender = await _userRepository.GetUserByEmail(senderEmail);
            var receiver = await _userRepository.GetUserByEmail(receiverEmail);
            var receiverId = receiver.Id;
            var senderId = sender.Id;

            if (sender.TenantId != tenantId || receiver.TenantId != tenantId)
            {
                return Unauthorized("Users can only chat within the same tenant.");
            }

            // Store the chat message in the database
            var chatMessage = new ChatMessage
            {
                Content = message,
                SentAt = DateTime.UtcNow,
                SenderId = senderId,
                ReceiverId = receiverId
            };

            _dbContext.ChatMessages.Add(chatMessage);
            await _dbContext.SaveChangesAsync();

            // Send the message to the specified user via SignalR
            await _hubContext.Clients.User(receiverEmail).SendAsync("ReceiveMessage", message);

            return Ok();
        }
    }
}
