using ObiletJourneyApp.Domain.ValueObjects;
using ObiletJourneyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ObiletJourneyApp.Application.DTOs
{
    public class JourneyDTO
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("stops")]
        public List<StopDTO> Stops { get; set; }
        [JsonPropertyName("origin")]
        public string Origin { get; set; }
        [JsonPropertyName("destination")]
        public string Destination { get; set; }
        [JsonPropertyName("departure")]
        public DateTime Departure { get; set; } = DateTime.Today.AddDays(1);
        [JsonPropertyName("arrival")]
        public DateTime Arrival { get; set; }
        [JsonPropertyName("duration")]
        public TimeSpan Duration { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("original-price")]
        public decimal OriginalPrice { get; set; }
        [JsonPropertyName("internet-price")]
        public decimal InternetPrice { get; set; }
        [JsonPropertyName("bus-name")]
        public string BusName { get; set; }
        [JsonPropertyName("policy")]
        public PolicyDTO Policy { get; set; }
        [JsonPropertyName("feature-descriptions")]
        public List<string> FeatureDescriptions { get; set; }
    }
}
