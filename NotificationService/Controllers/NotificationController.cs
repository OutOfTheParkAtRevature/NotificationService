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
        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()
        {
            var count = (from n in _notificationContext.Notifications
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
        public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage()
        {
            var results = from not in _notificationContext.Notifications
                          orderby not.Service descending
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
        public async Task<IActionResult> DeleteNotifications()
        {
            await _notificationContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Notification");
            await _notificationContext.SaveChangesAsync();
            await _hubContext.Clients.All.BroadcastMessage();
            // TODO: change 'someuser' to target specific logged in user
            await _hubContext.Clients.User("SomeUser").BroadcastMessage();

            return NoContent();
        }
    }
}
