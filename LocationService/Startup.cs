using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Filters;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

namespace LocationService
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();
            //Add json media formatter CamelCase
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();
        
            //allow cors;
            builder.UseCors(CorsOptions.AllowAll);

            builder.Map("/signalr", SetupSignalR);

            //default exception filter 
            config.Filters.Add(new ExceptionFilter());

            //setup the config file defined
            builder.UseWebApi(config);
        }

       

        private static void SetupSignalR(IAppBuilder map)
        {
            map.UseCors(CorsOptions.AllowAll);
            var hubConfig = new HubConfiguration()
                            {
                                EnableDetailedErrors = true,
                                EnableJSONP = true,
                                EnableJavaScriptProxies = true
                            };
            map.RunSignalR(hubConfig);
        }
    }

    public class ExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Debug.WriteLine(actionExecutedContext.Exception);
            Console.WriteLine(actionExecutedContext.Exception);
            actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
