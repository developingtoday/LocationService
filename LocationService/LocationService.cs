using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace LocationService
{
    public class LocationService
    {
        private IDisposable _disposable;

        public void Start()
        {
            var adress = @"http://localhost:10280/locationserv";
            _disposable = WebApp.Start(adress);
            Console.WriteLine("Web application started on:{0}",adress);
        }

        public void Stop()
        {
            _disposable?.Dispose();
        }
    }
}
