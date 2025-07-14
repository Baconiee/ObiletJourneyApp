using ObiletJourneyApp.Application.DTOs;
using ObiletJourneyApp.Application.Models.Requests;

namespace ObiletJourneyApp.WebUI.Models.ViewModels
{
    public class LocationSelectionViewModel
    {
        public List<LocationDTO> Locations { get; set; } = new();
        public BusJourneysRequest JourneyRequst { get; set; } 
    }
}
