using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class Location
    {
        private Location ()
        {
        }
        public Location(string locationName, double lat, double lon)
        {
            Name = locationName;
            LocationPoint = new LocationPoint
            {
                GeoPoint = new List<double> {lat, lon}
            };
        }

        public string Name { get; set; }
        public LocationPoint LocationPoint { get; set; }

        public static implicit operator Location(Domain.Models.Location source)
        {
            return new Location
            {
                Name = source.Name,
                LocationPoint = new LocationPoint
                {
                    GeoPoint = new List<double> {source.Lat, source.Lon}
                }
            };
        }
    }
    public class LocationPoint
    {
        public List<double> GeoPoint { get; set; }
    }
}