using Cycloid.Common.ParameterBinding;
using Cycloid.Managers;
using Cycloid.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Cycloid.API.Controllers
{
    /// <summary>
    /// The events controller
    /// </summary>
    [RoutePrefix("v1/events")]
    public class EventsController : ApiController
    {
        private readonly IEventsManager _eventsManager;
        public ILogger logger = LogManager.GetCurrentClassLogger();
        public string convertSessionID;

        /// <summary>
        /// The events controller constructor
        /// </summary>
        /// <param name="eventsManager">The events manager</param>
        public EventsController(IEventsManager eventsManager)
        {
            _eventsManager = eventsManager;
        }

        /// <summary>
        /// Gets the events by channel id
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <param name="channelId">The channel id</param>
        /// <returns>The events</returns>
        [HttpGet]
        [ResponseType(typeof(List<Event>))]
        [Route("{channelId}")]
        public async Task<HttpResponseMessage> GetByChannel([FromHeader("session-id")]string sessionId, [FromUri]string channelId)
        {
            logger.Info("All Programs (Yesterday, Today and Tomorrow) of a Channel request at" + Environment.NewLine + DateTime.Now);

            HttpHeaders headers = Request.Headers;

            var valueSessionID = headers.GetValues("session-id");

            foreach (var v in valueSessionID)
            {
                convertSessionID = v.ToString();
            }

            try
            {
                var deviceId = this._eventsManager.GetDeviceId(convertSessionID);

                if (deviceId == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " The device does not exist");
                }

                var getEventsList = await this._eventsManager.GetEventsAsync(deviceId, channelId);

                if (getEventsList != null)
                {
                    var ChannelsListToSend = JsonConvert.SerializeObject(getEventsList);

                    return Request.CreateResponse(HttpStatusCode.OK, ChannelsListToSend);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " There are no events found for this channel");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }




            //throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the events playing at the moment
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <returns>The events</returns>
        [HttpGet]
        [ResponseType(typeof(List<Event>))]
        [Route("now")]
        public async Task<HttpResponseMessage> GetPlaying([FromHeader("session-id")]string sessionId)
        {
            logger.Info("All Programs playing request at" + Environment.NewLine + DateTime.Now);

            HttpHeaders headers = Request.Headers;

            var valueSessionID = headers.GetValues("session-id");

            foreach (var v in valueSessionID)
            {
                convertSessionID = v.ToString();
            }

            try
            {
                var deviceId = this._eventsManager.GetDeviceId(convertSessionID);

                if (deviceId == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " The device does not exist");
                }

                var getEventsPlayingList = await this._eventsManager.GetPlayingEventsAsync(deviceId);

                if (getEventsPlayingList != null)
                {
                    var ChannelsListToSend = JsonConvert.SerializeObject(getEventsPlayingList);

                    return Request.CreateResponse(HttpStatusCode.OK, ChannelsListToSend);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " There are no events found for this channel");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            //throw new NotImplementedException();
        }
    }
}
