using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LocationService
{
    public class LocationController:ApiController
    {
        private readonly ILocationManager _locationManager;
        public LocationController()
        {
            _locationManager = new LocationManager();
        }

        [Route("api/v1/ping")]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {Result = "pong"});
        }

        [Route("api/v1/client/{clientId}/location")]
        [HttpGet]
        public HttpResponseMessage GetLocationClient([FromUri] Guid clientId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _locationManager.RetrieveLocations(clientId));
        }

        [Route("api/v1/client/location")]
        [HttpPost]
        public async Task<HttpResponseMessage> SetLocationClient([FromBody] LocationRequest request)
        {
            await _locationManager.UpdateLocation(request);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

   
}
