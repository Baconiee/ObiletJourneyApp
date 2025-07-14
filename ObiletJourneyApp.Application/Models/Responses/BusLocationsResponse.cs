using ObiletJourneyApp.Application.DTOs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObiletJourneyApp.Application.Models.Responses
{
    public class BusLocationsResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("data")]
        public List<BusLocationsData> BusLocationsData { get; set; } 
    }

    public class BusLocationsData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("parent-id")]
        public int? ParentId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("geo-location")]
        public GeoLocationDTO GeoLocation { get; set; }
        [JsonPropertyName("tz-code")]
        public string TimeZoneCode { get; set; }
        [JsonPropertyName("weather-code")]
        public string WeatherCode { get; set; }
        [JsonPropertyName("rank")]
        public int? Rank { get; set; }
        [JsonPropertyName("reference-code")]
        public string ReferenceCode { get; set; }
        [JsonPropertyName("keywords")]
        public string Keywords { get; set; }
        [JsonPropertyName("city-id")]
        public int? CityId { get; set; }
        [JsonPropertyName("city-name")]
        public string CityName { get; set; }
        [JsonPropertyName("country-id")]
        public int? CountryId { get; set; }
        [JsonPropertyName("country-name")]
        public string CountryName { get; set; }
        [JsonPropertyName("show-country")]
        public bool? ShowCountry { get; set; }
        [JsonPropertyName("long-name")]
        public string LongName { get; set; }
        [JsonPropertyName("is-city-center")]
        public bool? IsCityCenter { get; set; }
    }
}