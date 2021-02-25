using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationContext _notificationContext;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public NotificationController(NotificationContext notificationContext, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _notificationContext = notificationContext;
            _hubContext = hubContext;
        }

        // GET: api/Notifications/notificationcount  
        [Route("notificationcount")]
        [HttpGet]
        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount(string userId)
        {
            var count = (from n in _notificationContext.Notifications
                         where n.UserID == userId
                         select n).CountAsync();
            NotificationCountResult result = new NotificationCountResult
            {
                Count = await count
            };
            return result;
        }

        // GET: api/Notifications/notificationresult  
        [Route("notificationresult")]
        [HttpGet]
        public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage(string userId)
        {
            var results = from not in _notificationContext.Notifications
                          orderby not.Service descending
                          where not.UserID == userId
                          select new NotificationResult
                          {
                              Service = not.Service,
                              TransactionType = not.TransactionType
                          };
            return await results.ToListAsync();
        }

        // DELETE: api/Notifications/deletenotifications  
        [HttpDelete]
        [Route("deletenotifications")]
        public async Task<IActionResult> DeleteNotifications(string userId)
        {
            // any time the 
            await _notificationContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Notification");
            await _notificationContext.SaveChangesAsync();
            // This broadcasts messages to all users logged in
            // await _hubContext.Clients.All.BroadcastMessage();

            // This broadcasts to a particular user
            await _hubContext.Clients.User(userId).BroadcastMessage();

            return NoContent();
        }
    }
}
