using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;

namespace Repository
{
    public class Repo
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger _logger;
        public DbSet<Notification> notifications;

        public Repo(NotificationContext notificationContext, ILogger<Repo> logger)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            this.notifications = _notificationContext.Notifications;
        }

        public async Task CommitSave()
        {
            await _notificationContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await notifications.ToListAsync();
        }

        public async Task AddNotification(Notification notification)
        {
            await notifications.AddAsync(notification);
        }

        // no async method for remove
        public void DeleteNotification(Notification notification)
        {
            notifications.Remove(notification);
        }
    }
}
