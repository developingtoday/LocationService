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

        [Route("api/v1/ping")]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {Result = "pong"});
        }
    }
}
