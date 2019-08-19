using Cycloid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public interface IChannelsService
    {
        object GetChannelsMethod();
        object GetSubscribedChannelsMethod(string sessionid);
    }
}
