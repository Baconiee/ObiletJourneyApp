using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObiletJourneyApp.Domain.Entities;
using ObiletJourneyApp.Application.DTOs;

namespace ObiletJourneyApp.Application.Services
{
    public interface ISessionService
    {
        Task<SessionDTO> GetSessionAsync();
    }
}
