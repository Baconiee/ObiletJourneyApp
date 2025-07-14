using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObiletJourneyApp.Domain.Entities;
using ObiletJourneyApp.Application.DTOs;

namespace ObiletJourneyApp.Application.Services
{
    public interface IObiletService
    {
        Task<List<LocationDTO>> GetBusLocationsAsync((string sessionId, string deviceId) session, string? data, DateTime? date);
        Task<List<JourneyDataDTO>> GetBusJourneysAsync((string sessionId, string deviceId) session, int originId, int destinationId, DateTime departureDate);
    }
}
