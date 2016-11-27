using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace LocationService
{
    public interface ILocationManager
    {
        Task UpdateLocation(LocationRequest request);

        Task<IEnumerable<LocationResponse>> RetrieveLocations(Guid clientId, DateTime? startDate = null,
            DateTime? endDate = null);
    }

    public class LocationManager : ILocationManager
    {
        private readonly MongoClient _client;

        public LocationManager()
        {
            _client = new MongoClient("mongodb://localhost:27017");


        }

        public IMongoDatabase MongoContext => _client.GetDatabase("Location");

        public IMongoCollection<ClientLocation> ClientLocations
            => MongoContext.GetCollection<ClientLocation>(typeof (ClientLocation).Name);
    

    public async Task UpdateLocation(LocationRequest request)
        {
            var clientLocation = new ClientLocation
                                 {
                                     Id = Guid.NewGuid(),
                                     ClientId = request.ClientId,
                                     CurrentTimeStamp = request.CurrentTimeStamp,
                                     Coordinates =
                                         GeoJson.Point(new GeoJsonObjectArgs<GeoJson2DGeographicCoordinates>(),
                                             new GeoJson2DGeographicCoordinates(request.Longitude, request.Latitude))
                                 };
            await ClientLocations.InsertOneAsync(clientLocation);
        }

        public async Task<IEnumerable<LocationResponse>> RetrieveLocations(Guid clientId, DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var list = new List<FilterDefinition<ClientLocation>>();

            list.Add(Builders<ClientLocation>.Filter.Where(a => a.ClientId == clientId));

            if (startDate.HasValue)
            {
                list.Add(Builders<ClientLocation>.Filter.Where(a=>a.CurrentTimeStamp>=startDate.Value));
            }
            if (endDate.HasValue)
            {
                list.Add(Builders<ClientLocation>.Filter.Where(a=>a.CurrentTimeStamp<=endDate.Value));
            }

            var locations =
                await ClientLocations.FindAsync(Builders<ClientLocation>.Filter.And(list));
            var listAsync = await locations.ToListAsync();
            return  listAsync.GroupBy(a => a.ClientId).Select(a => new LocationResponse()
                                                                           {
                                                                               ClientId = a.Key,
                                                                               Datas = a.Select(d => new LocationData()
                                                                                                     {
                                                                                                         CurrentTimeStamp
                                                                                                             =
                                                                                                             d
                                                                                                             .CurrentTimeStamp,
                                                                                                         Longitude =
                                                                                                             d
                                                                                                             .Coordinates
                                                                                                             .Coordinates
                                                                                                             .Longitude,
                                                                                                         Latitude =
                                                                                                             d
                                                                                                             .Coordinates
                                                                                                             .Coordinates
                                                                                                             .Latitude
                                                                                                     }).ToArray()
                                                                           }).ToList();

        } 
       
    }
}
