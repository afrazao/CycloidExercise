using Cycloid.Models;
using Cycloid.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class EventsManager : IEventsManager, IDeviceManager
    {
        private readonly IDevicesRepository devicesRepository;
        private readonly IChannelsManager channelsManager;
        private readonly IProgramsManager programsManager;

        public bool checkSubscription = false;
        public List<Event> getEventsList = new List<Event>();
        public List<Event> getEventsPlayingList = new List<Event>();
        
        public EventsManager(IDevicesRepository devicesRepository, IChannelsManager channelsManager, IProgramsManager programsManager)
        {
            this.devicesRepository = devicesRepository;
            this.channelsManager = channelsManager;
            this.programsManager = programsManager;
        }

        public string GetDeviceId(string sessionId)
        {
            //Testar a API
            var deviceId = sessionId.Replace("session", "device");

            if (deviceId == "device-001" || deviceId == "device-002" || deviceId == "device-003")
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

            //throw new System.NotImplementedException();
        }

        public async Task<List<Event>> GetEventsAsync(string deviceId, string channelId, CancellationToken ct = default(CancellationToken))
        {

            var ProgramsList = this.programsManager.GetAllPrograms();

            var getProgramsList = new List<Program>();       

            foreach (var p in ProgramsList)
            {
                if (p.ChannelId == channelId)
                {
                    getProgramsList.Add(p);
                }
            }

            var subscribedChannelsListWCF = this.channelsManager.GetAllSubscribedChannels(deviceId);

            string[] objString = (string[])subscribedChannelsListWCF;

            for(int i = 0; i <= objString.Length - 1; i++)
            {
                if(objString[i].ToString() == channelId)
                {
                    checkSubscription = true;
                }
            }

            foreach (var e in getProgramsList)
            {
                var checkTime = DateTime.Now.Date;
                var checkYesterday = checkTime.AddDays(-1);
                var checkTomorrow = checkTime.AddDays(1);

                if(e.StartTime >= checkYesterday && e.EndTime <= checkTomorrow)
                {
                    var toAddEvent = new Event
                    {
                        ChannelName = channelId,
                        ProgramTitle = e.Title,
                        ProgramDescription = e.Description,
                        ProgramStartTime = e.StartTime,
                        ProgramEndTime = e.EndTime,
                        IsSubscribed = checkSubscription
                    };

                    getEventsList.Add(toAddEvent);
                }
            }

            return getEventsList;


            //throw new System.NotImplementedException();
        }

        public async Task<List<Event>> GetPlayingEventsAsync(string deviceId, CancellationToken ct = default(CancellationToken))
        {

            var ProgramsList = this.programsManager.GetAllPrograms();

            foreach (var e in ProgramsList)
            {
                var checkTime = DateTime.Now;

                if (e.StartTime <= checkTime && e.EndTime >= checkTime)
                {
                    var toAddEvent = new Event
                    {
                        ChannelName = e.ChannelId,
                        ProgramTitle = e.Title,
                        ProgramDescription = e.Description,
                        ProgramStartTime = e.StartTime,
                        ProgramEndTime = e.EndTime,
                        IsSubscribed = checkSubscription
                    };

                    getEventsPlayingList.Add(toAddEvent);
                }
            }

            return getEventsPlayingList;

            //throw new System.NotImplementedException();
        }
    }
}
