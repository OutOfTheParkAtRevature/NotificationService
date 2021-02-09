﻿using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Model
{
    public class NotificationProducer : BackgroundService
    {
        private readonly IPushSubscriptionsService _pushSubscriptionsService;
        private readonly PushServiceClient _pushClient;

        // This is a minute delay
        private const int NOTIFICATION_FREQUENCY = 60000;

        public NotificationProducer(IOptions<PushNotificationsOptions> options, IPushSubscriptionsService pushSubscriptionsService, PushServiceClient pushClient)
        {
            _pushSubscriptionsService = pushSubscriptionsService;

            _pushClient = pushClient;
            _pushClient.DefaultAuthentication = new VapidAuthentication(options.Value.PublicKey, options.Value.PrivateKey)
            {
                // TODO: Update to P3 website.
                Subject = "https://angular-pushnotifications.demo.io"
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This currently sends a notification every minute for testing purposes.
            // TODO: Either adjust the notification frequency or create a method to only update when information to expose is received
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(NOTIFICATION_FREQUENCY, stoppingToken);

                SendNotifications(stoppingToken);
            }
        }

        private void SendNotifications(/*We can send in info to expose in the notification here as extra parameters*/ CancellationToken stoppingToken)
        {
            PushMessage notification = new AngularPushNotification
            {
                // Title of the notification
                Title = "New Message",
                // Short description of the notification
                Body = $"Information we intend to expose with the notification",
                // Image associated with notification (probably our logo)
                Icon = "assets/icons/icon-96x96.png"
            }.ToPushMessage();

            foreach (PushSubscription subscription in _pushSubscriptionsService.GetAll())
            {
                // Fire-and-forget 
                _pushClient.RequestPushMessageDeliveryAsync(subscription, notification, stoppingToken);
            }
        }
    }
}
