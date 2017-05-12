using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Hubs
{
    [HubName("CheckchartHubs")]
    public class Broadcaster : Hub
    {
    }
}
