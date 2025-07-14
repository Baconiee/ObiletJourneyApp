    using ObiletJourneyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Application.DTOs
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public GeoLocationDTO GeoLocation { get; set; }
        public string TimeZoneCode { get; set; }
        public string WeatherCode { get; set; }
        public int? Rank { get; set; }
        public string ReferenceCode { get; set; }
        public string Keywords { get; set; }
    }
}
