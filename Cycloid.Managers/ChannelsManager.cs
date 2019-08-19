using Cycloid.Models;
using Cycloid.Repositories;
using Cycloid.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class ChannelsManager : IChannelsManager, IDeviceManager
    {
        private readonly IChannelsService channelsService;
        private readonly IDevicesRepository devicesRepository;

        public ChannelsManager(IChannelsService channelsService, IDevicesRepository devicesRepository)
        {
            this.channelsService = channelsService;
            this.devicesRepository = devicesRepository;
        }

        public object GetAllChannels()
        {
            var channelsList = channelsService.GetChannelsMethod();

            return channelsList;

        }

        public object GetAllSubscribedChannels(string sessionid)
        {
            var subscribedChannelsList = channelsService.GetSubscribedChannelsMethod(sessionid);

            return subscribedChannelsList;
        }

        public string GetDeviceId(string sessionId)
        {
            //Testar a API
            var deviceId = sessionId.Replace("session", "device");

            if(deviceId == "device-001" || deviceId == "device-002" || deviceId == "device-003")
            {
                return deviceId;
            }

            return null;

            //Verifica se existe Device
            //var checkDeviceById = this.devicesRepository.GetDevice(sessionId);

            //if(checkDeviceById != null)
            //{
            //    return checkDeviceById.Id;
            //}
            //else
            //{
            //    return null;
            //}
        }
    }
}
