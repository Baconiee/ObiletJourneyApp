using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ObiletJourneyApp.Application.DTOs
{
    public class SessionDTO
    {
        [JsonPropertyName("session-id")]
        public string SessionId { get; set; }
        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }
    }
}
