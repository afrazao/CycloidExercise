using Cycloid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public class ChannelsWcfService : IChannelsService
    {
        public object GetChannelsMethod()
        {

            try
            {
                ChannelsService.Service1 clientWCF = new ChannelsService.Service1();

                var channelsListWCF = clientWCF.GetChannels();

                return channelsListWCF;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public object GetSubscribedChannelsMethod(string sessionid)
        {
            try
            {
                ChannelsService.Service1 clientWCFSubscribed = new ChannelsService.Service1();

            var subscribedChannelsListWCF = clientWCFSubscribed.GetSubscribedChannelIds(sessionid);

            return subscribedChannelsListWCF;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
