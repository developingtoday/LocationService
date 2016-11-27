using System;

namespace LocationService
{
    public class LocationRequest
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Guid ClientId { get; set; }

        public DateTime CurrentTimeStamp { get; set; }

    }
}