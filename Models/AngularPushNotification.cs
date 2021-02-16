using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Lib.Net.Http.WebPush;

namespace Model
{
    public class AngularPushNotification
    {
        private const string WRAPPER_START = "{\"notification\":";
        private const string WRAPPER_END = "}";

        public class NotificationAction
        {
            public string Action { get; }

            public string Title { get; }

            public NotificationAction(string action, string title)
            {
                Action = action;
                Title = title;
            }
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Icon { get; set; }

        public IList<int> Vibrate { get; set; } = new List<int>();

        public IDictionary<string, object> Data { get; set; }

        public IList<NotificationAction> Actions { get; set; } = new List<NotificationAction>();

        // Following logic must remain in this model.

        /// <summary>
        /// Configures settings for JSON serializer.
        /// </summary>
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// Converts a notification to a push message which can be sent to the angular front end.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="timeToLive"></param>
        /// <param name="urgency"></param>
        /// <returns></returns>
        public PushMessage ToPushMessage(string topic = null, int? timeToLive = null, PushMessageUrgency urgency = PushMessageUrgency.Normal)
        {
            return new PushMessage(WRAPPER_START + JsonConvert.SerializeObject(this, _jsonSerializerSettings) + WRAPPER_END)
            {
                Topic = topic,
                TimeToLive = timeToLive,
                Urgency = urgency
            };
        }
    }
}
