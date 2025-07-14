using ObiletJourneyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.Entities
{
    public class Location
    {
        public int Id { get; }
        public int? ParentId { get; }
        public string Type { get; }
        public string Name { get; }
        public GeoLocation GeoLocation { get; }
        public string TimeZoneCode { get; }
        public string WeatherCode { get; }
        public int? Rank { get; }
        public string ReferenceCode { get; }
        public string Keywords { get; }

        public Location(
            int id, int? parentId, string type, string name, GeoLocation geoLocation, string timeZoneCode, string weatherCode, int? rank, string referenceCode, string keywords)
        {
            Id = id;
            ParentId = parentId;
            Type = type;
            Name = name;
            GeoLocation = geoLocation;
            TimeZoneCode = timeZoneCode;
            WeatherCode = weatherCode;
            Rank = rank;
            ReferenceCode = referenceCode;
            Keywords = keywords;
        }
    }
}
