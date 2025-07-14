using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ObiletJourneyApp.Application.DTOs;

namespace ObiletJourneyApp.Application.Models.Responses
{
    public class BusJourneysResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("data")]
        public List<JourneyDataDTO> JourneyData { get; set; }
    }
}
