using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.Entities
{
    public class Session
    {
        public string SessionId { get; }
        public string DeviceId { get; }

        public Session(string sessionId, string deviceId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                throw new ArgumentException("Session ID cannot be null or empty.", nameof(sessionId));
            }
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentException("Device ID cannot be null or empty.", nameof(deviceId));
            }

            SessionId = sessionId;
            DeviceId = deviceId;
        }
    }
}
