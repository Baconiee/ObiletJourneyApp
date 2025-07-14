using ObiletJourneyApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ObiletJourneyApp.Application.Models.Requests
{
    public class BusLocationsRequest
    {
        [JsonPropertyName("data")]
        public string? Data { get; set; }
        [JsonPropertyName("device-session")]
        public SessionDTO Session { get; set; }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; } = DateTime.Now.AddDays(1);
        [JsonPropertyName("language")]
        public string? Language { get; set; } = "en-EN";
    }
}
