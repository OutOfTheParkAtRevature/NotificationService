using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class NotificationContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; }
        public NotificationContext() { }
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options) { }
    }
}
