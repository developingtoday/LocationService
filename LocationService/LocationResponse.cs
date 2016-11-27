using System;

namespace LocationService
{
    public class LocationResponse
    {
        public Guid ClientId { get; set; }

        public LocationData[] Datas { get; set; }

    }

    public class LocationData
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CurrentTimeStamp { get; set; }
    }
}