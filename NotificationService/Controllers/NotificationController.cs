using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationService.Controllers
{
    public class NotificationController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class MessageNotificationController : Controller
        {

            [HttpGet]
            public IEnumerable<Notification> Get()
            {
                // controller logic goes here
            }
        }

        // There is room to extend the controller with potential notifications for equipment requests, etc.
    }
}
