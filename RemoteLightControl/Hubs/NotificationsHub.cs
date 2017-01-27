using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RemoteLightControl.Hubs
{
    public class NotificationsHub : Hub
    {
        public void Notify(int lightId, String message)
        {
            Clients.All.notifyLightChange(lightId, message);
        }
    }
}