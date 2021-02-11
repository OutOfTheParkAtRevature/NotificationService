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
        /// <summary>
        /// saves changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitSave()
        {
            await _notificationContext.SaveChangesAsync();
        }
        /// <summary>
        /// return a list of all notifications
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await notifications.ToListAsync();
        }
        /// <summary>
        /// adds a notification to the notifications DbSet
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async Task AddNotification(Notification notification)
        {
            await notifications.AddAsync(notification);
        }

        // no async method for remove
        /// <summary>
        /// deletes a notification from the notification dbset
        /// </summary>
        /// <param name="notification"></param>
        public void DeleteNotification(Notification notification)
        {
            notifications.Remove(notification);
        }
    }
}
