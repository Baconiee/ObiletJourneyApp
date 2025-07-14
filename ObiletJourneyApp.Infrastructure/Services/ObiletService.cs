using System.Text;
using System.Text.Json;
using ObiletJourneyApp.Application.DTOs;
using ObiletJourneyApp.Application.Models.Requests;
using ObiletJourneyApp.Application.Models.Responses;
using ObiletJourneyApp.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using ObiletJourneyApp.Domain.Entities;

namespace ObiletJourneyApp.Infrastructure.Services
{
    public class ObiletService : IObiletService
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cache;

        public ObiletService(HttpClient httpClient, ICacheService cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<LocationDTO>> GetBusLocationsAsync((string sessionId, string deviceId) session, string? data, DateTime? date)
        {
            string cacheKey = "ObiletBusLocations";

            if (string.IsNullOrEmpty(data))
            {
                var cachedData = await _cache.GetAsync<List<LocationDTO>>(cacheKey);
                if (cachedData != null)
                    return cachedData;
            }

            var request = new BusLocationsRequest
            {
                Data = data,
                Session = new SessionDTO
                {
                    SessionId = session.sessionId,
                    DeviceId = session.deviceId
                },
                Date = date,
                Language = "tr-TR"
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("location/getbuslocations", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                System.Console.WriteLine($"Response Status: {response.StatusCode}");
                System.Console.WriteLine($"Response Body: {responseContent}");

                response.EnsureSuccessStatusCode();

                var locationsResponse = JsonSerializer.Deserialize<BusLocationsResponse>(responseContent);

                if (locationsResponse == null || locationsResponse.BusLocationsData == null || !locationsResponse.BusLocationsData.Any())
                {
                    throw new Exception("No bus locations returned from the API.");
                }

                if (locationsResponse.Status != "Success")
                {
                    throw new Exception($"Failed to obtain bus locations.");
                }

                var validLocations = locationsResponse.BusLocationsData
                    .Where(loc => loc.GeoLocation != null && loc.GeoLocation.Latitude != 0 && loc.GeoLocation.Longitude != 0)
                    .Select(loc => new LocationDTO
                    {
                        Id = loc.Id,
                        ParentId = loc.ParentId,
                        Type = loc.Type,
                        Name = loc.Name,
                        GeoLocation = loc.GeoLocation ?? new GeoLocationDTO(),
                        TimeZoneCode = loc.TimeZoneCode,
                        WeatherCode = loc.WeatherCode,
                        Rank = loc.Rank,
                        ReferenceCode = loc.ReferenceCode,
                        Keywords = loc.Keywords
                    }).ToList();

                if (!validLocations.Any())
                {
                    throw new Exception("No valid bus locations with proper geo-location data found.");
                }

                if (string.IsNullOrEmpty(data))
                {
                    await _cache.SetAsync(cacheKey, validLocations, TimeSpan.FromMinutes(30));
                }

                return validLocations;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to communicate with the bus locations API.", ex);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine($"Deserialization error details: {ex.Message}");
                throw new Exception("Failed to parse the bus locations API response.", ex);
            }
        }


        public async Task<List<JourneyDataDTO>> GetBusJourneysAsync((string sessionId, string deviceId) session, int originId, int destinationId, DateTime departureDate)
        {
            var request = new BusJourneysRequest
            {
                Data = new JourneyRequestData
                {
                    OriginId = originId,
                    DestinationId = destinationId,
                    DepartureDate = departureDate
                },
                Session = new SessionDTO
                {
                    SessionId = session.sessionId,
                    DeviceId = session.deviceId
                },
                Date = DateTime.Now,
                Language = "tr-TR"
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("journey/getbusjourneys", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine($"Response Status: {response.StatusCode}");
                System.Console.WriteLine($"Response Body: {responseContent}");
                response.EnsureSuccessStatusCode();

                var journeysResponse = JsonSerializer.Deserialize<BusJourneysResponse>(responseContent);

                if (journeysResponse == null || journeysResponse.JourneyData == null || !journeysResponse.JourneyData.Any())
                {
                    throw new Exception("No bus locations returned from the API.");
                }

                if (journeysResponse.Status != "Success")
                {
                    throw new Exception($"Failed to obtain bus locations:");
                }

                var validJourneys = journeysResponse.JourneyData
                    .Where(j => j.Id != null && j.IsActive == true)
                    .Select(j => new JourneyDataDTO
                    {
                        Id = j.Id,
                        PartnerId = j.PartnerId,
                        PartnerName = j.PartnerName,
                        RouteId = j.RouteId,
                        BusType = j.BusType,
                        TotalSeats = j.TotalSeats,
                        AvailableSeats = j.AvailableSeats,
                        Journey = new JourneyDTO
                        {
                            Kind = j.Journey.Kind,
                            Code = j.Journey.Code,
                            Origin = j.Journey.Origin,
                            Destination = j.Journey.Destination,
                            Departure = j.Journey.Departure,
                            Arrival = j.Journey.Arrival,
                            Duration = j.Journey.Duration,
                            Currency = j.Journey.Currency,
                            OriginalPrice = j.Journey.OriginalPrice,
                            InternetPrice = j.Journey.InternetPrice,
                            BusName = j.Journey.BusName,
                            Policy = new PolicyDTO
                            {
                                MaxSeats = j.Journey.Policy?.MaxSeats,
                                MaxSingle = j.Journey.Policy?.MaxSingle,
                                MaxSingleMales = j.Journey.Policy?.MaxSingleMales,
                                MaxSingleFemales = j.Journey.Policy?.MaxSingleFemales,
                                MixedGenders = j.Journey.Policy?.MixedGenders ?? false,
                                GovId = j.Journey.Policy?.GovId ?? false,
                                Lht = j.Journey.Policy?.Lht ?? false
                            },
                            FeatureDescriptions = j.Journey.FeatureDescriptions ?? new List<string>(),
                            Stops = j.Journey.Stops?.Select(s => new StopDTO
                            {
                                Name = s.Name,
                                Station = s.Station,
                                Time = s.Time,
                                IsOrigin = s.IsOrigin,
                                IsDestination = s.IsDestination
                            }).ToList() ?? new List<StopDTO>()
                        },
                        Features = j.Features?.Select(f => new FeatureDTO
                        {
                            Id = f.Id,
                            Priority = f.Priority,
                            Name = f.Name,
                            Description = f.Description,
                            IsPromoted = f.IsPromoted,
                            BackColor = f.BackColor,
                            ForeColor = f.ForeColor
                        }).ToList() ?? new List<FeatureDTO>(),
                        OriginLocation = j.OriginLocation,
                        DestinationLocation = j.DestinationLocation,
                        IsActive = j.IsActive,
                        OriginLocationId = j.OriginLocationId,
                        DestinationLocationId = j.DestinationLocationId,
                        IsPromoted = j.IsPromoted,
                        CancellationOffset = j.CancellationOffset,
                        HasBusShuttle = j.HasBusShuttle,
                        DisableSalesWithoutGovId = j.DisableSalesWithoutGovId,
                        DisplayOffset = j.DisplayOffset,
                        PartnerRating = j.PartnerRating
                    }).ToList();

                return validJourneys;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching journeys: " + ex.Message);
                return new List<JourneyDataDTO>();
            }
        }
    }
}