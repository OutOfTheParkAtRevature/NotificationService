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
                    NotificationID = 1,
                    MessageID = 1,
                    Status = "okay"
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
                NotificationID = 1,
                MessageID = 2,
                Status = "okay"
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
            //WHY DOESN'T THIS WORK!?
            //using (var context = new NotificationContext(options))
            //{
            //    Repo r = new Repo(context, new NullLogger<Repo>());



            //    Assert.NotEmpty(await r.GetNotifications());
            //    await r.CommitSave();

            //    await r.DeleteNotification(notification);


            //    Assert.Empty(await r.GetNotifications());

            //}
        }

    }
}

