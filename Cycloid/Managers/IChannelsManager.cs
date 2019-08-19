﻿using Cycloid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public interface IChannelsManager
    {
        object GetAllChannels();
        object GetAllSubscribedChannels(string sessionid);
        string GetDeviceId(string sessionId);
    }
}
