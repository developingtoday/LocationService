using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace LocationService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
                            {
                                x.Service<LocationService>(s =>
                                                           {
                                                               s.ConstructUsing(name => new LocationService());
                                                                   //create the class that hosts service logic
                                                               s.WhenStarted(a => a.Start()); //start method
                                                               s.WhenStopped(a => a.Stop()); //stop method
                                                           });
                                x.RunAsLocalSystem();
                                    //run as local system, we can change to run to other type but this is good for us.
                                //service description
                                x.SetDescription("Windows location service for angular 2");
                                x.SetDisplayName("AngularMap.LocationService");
                                x.SetServiceName("AngularMap.LocationService");
                            });
        }
    }
}
