using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Repository.Tests
{
    public class RepositoryTests
    {
        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        [Fact]
        public async void TestForCommitSave()
        {
            var options = new DbContextOptionsBuilder<NotificationContext>()
            .UseInMemoryDatabase(databaseName: "p3newsetuptest")
            .Options;

            using (var context = new NotificationContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var notification = new Notification
                {
                    NotificationID = new Guid(),
                    Service = "Message",
                    TransactionType = "Post"
                };

                r.notifications.Add(notification);
                await r.CommitSave();
                Assert.NotEmpty(context.Notifications);
            }
            using (var context = new NotificationContext(options))
            {
                Repo r = new Repo(context, new NullLogger<Repo>());

                Assert.NotEmpty(context.Notifications);
            }
        }
        /// <summary>
        /// Tests both adding and removing notifications methods in the Repo
        /// </summary>
        [Fact]
        public async void TestForAddAndRemoveNotification()
        {
            var options = new DbContextOptionsBuilder<NotificationContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest2")
            .Options;

            var notification = new Notification
            {
                NotificationID = new Guid(),
                Service = "Message",
                TransactionType = "Post"
            };

            using (var context = new NotificationContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());


                await r.AddNotification(notification);
                await r.CommitSave();

                Assert.NotEmpty(await r.GetNotifications());

                r.DeleteNotification(notification);
                await r.CommitSave();


                Assert.Empty(await r.GetNotifications());
            }
        }

    }
}

