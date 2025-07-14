using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ObiletJourneyApp.Application.DTOs;

namespace ObiletJourneyApp.Application.Models.Requests
{
    public class BusJourneysRequest
    {
        [JsonPropertyName("device-session")]
        public SessionDTO Session { get; set; }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; } = DateTime.Now;
        [JsonPropertyName("language")]
        public string? Language { get; set; } = "en-EN";
        [JsonPropertyName("data")]
        public JourneyRequestData Data { get; set; }
    }

    public class JourneyRequestData
    {
        [JsonPropertyName("origin-id")]
        public int OriginId { get; set; }
        [JsonPropertyName("destination-id")]
        public int DestinationId { get; set; }
        [JsonPropertyName("departure-date")]
        public DateTime? DepartureDate { get; set; }
    }
}
