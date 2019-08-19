using Cycloid.Common.ParameterBinding;
using Cycloid.Managers;
using Cycloid.Models;
using Cycloid.Services;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.SessionState;

namespace Cycloid.API.Controllers
{
    /// <summary>
    /// The channels controller
    /// </summary>
    [RoutePrefix("v1/channels")]
    public class ChannelsController : ApiController 
    {
        private readonly IChannelsManager _channelsManager;
        public ILogger logger = LogManager.GetCurrentClassLogger();
        public string convertSessionID;

        /// <summary>
        /// The channels controller constructor
        /// </summary>
        /// <param name="channelsManager">The channels manager</param>
        public ChannelsController(IChannelsManager channelsManager)
        {
            _channelsManager = channelsManager;
        }

        /// <summary>
        /// Gets all channels
        /// </summary>
        /// <returns>The channels</returns>
        [HttpGet]
        [ResponseType(typeof(List<Channel>))]
        [Route("")]
        public HttpResponseMessage Get()
        {
            logger.Info("All Channels request at" + Environment.NewLine + DateTime.Now);

            try
            {
                var channelsListWCF = this._channelsManager.GetAllChannels();

                if (channelsListWCF != null)
                {
                    var ChannelsListToSend = JsonConvert.SerializeObject(channelsListWCF);

                    return Request.CreateResponse(HttpStatusCode.OK, ChannelsListToSend);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Channels Not Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); 
            }

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the subscribed channels
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <returns>The subscribed channel ids</returns>
        [HttpGet]
        [ResponseType(typeof(List<Channel>))]
        [Route("subscribed")]
        public HttpResponseMessage GetSubscribedChannels([FromHeader("session-id")]string sessionId)
        {
            logger.Info("All Subscribed Channels request at" + Environment.NewLine + DateTime.Now);

            HttpHeaders headers = Request.Headers;

            var valueSessionID = headers.GetValues("session-id");

            foreach (var v in valueSessionID)
            {
                convertSessionID = v.ToString();
            }

            try
            {
                var deviceId = this._channelsManager.GetDeviceId(convertSessionID);

                if(deviceId == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " The device does not exist");
                }

                var subscribedChannelsListWCF = this._channelsManager.GetAllSubscribedChannels(deviceId);

                if (subscribedChannelsListWCF != null)
                {
                    var ChannelsListToSend = JsonConvert.SerializeObject(subscribedChannelsListWCF);

                    return Request.CreateResponse(HttpStatusCode.OK, ChannelsListToSend);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " There are no subscribed channels found for this device");
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
