using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GeoJsonObjectModel;

namespace LocationService
{
    public class ClientLocation
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Coordinates { get; set; }

        public DateTime CurrentTimeStamp { get; set; }
    }
}
