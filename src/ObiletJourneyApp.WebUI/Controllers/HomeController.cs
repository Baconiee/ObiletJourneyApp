using Microsoft.AspNetCore.Mvc;
using ObiletJourneyApp.Application.Services;
using ObiletJourneyApp.WebUI.Models;
using System.Diagnostics;
using ObiletJourneyApp.WebUI.Models.ViewModels;
using ObiletJourneyApp.Application.Models.Requests;

namespace ObiletJourneyApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IObiletService _obiletService;
        private readonly ISessionService _sessionService;

        public HomeController(ILogger<HomeController> logger, IObiletService obiletService, ISessionService sessionService)
        {
            _logger = logger;
            _obiletService = obiletService;
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var session = await _sessionService.GetSessionAsync();
                var sessionTuple = (session.SessionId, session.DeviceId);

                var locations = await _obiletService.GetBusLocationsAsync(sessionTuple, null, null);

                var locationViewModel = new LocationSelectionViewModel
                {
                    Locations = locations
                };
                return View(locationViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching session or bus locations");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchBusLocations([FromBody] SearchRequest request)
        {
            Console.WriteLine($"Search term received: {request.Search}");

            var session = await _sessionService.GetSessionAsync();
            var sessionTuple = (session.SessionId, session.DeviceId);

            var locations = await _obiletService.GetBusLocationsAsync(sessionTuple, request.Search, null);

            var result = locations.Select(loc => new { id = loc.Id, name = loc.Name }).ToList();

            Console.WriteLine("Filtered locations:");
            foreach (var loc in result)
            {
                Console.WriteLine($"ID: {loc.id}, Name: {loc.name}");
            }

            return Json(result);
        }

        public class SearchRequest
        {
            public string Search { get; set; }
        }

        public async Task<IActionResult> SearchBusJourneys(int originId, int destinationId, DateTime departureDate)
        {
            var session = await _sessionService.GetSessionAsync();
            var sessionTuple = (session.SessionId, session.DeviceId);

            var journeys = await _obiletService.GetBusJourneysAsync(sessionTuple, originId, destinationId, departureDate);
            var sortedJourneys = journeys.OrderBy(j => j.Journey.Departure).ToList();

            return View("Journeys", sortedJourneys);
        }

        public IActionResult Journeys()
        {
            return View();
        }
    }
}
