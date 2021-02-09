using System.Collections.Generic;
using Lib.Net.Http.WebPush;

namespace Model
{
    public interface IPushSubscriptionsService
    {
        IEnumerable<PushSubscription> GetAll();

        void Insert(PushSubscription subscription);

        void Delete(string endpoint);
    }
}
