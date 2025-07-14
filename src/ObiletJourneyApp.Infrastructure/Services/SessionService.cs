using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ObiletJourneyApp.Application.DTOs;
using ObiletJourneyApp.Application.Services;
using ObiletJourneyApp.Domain.Entities;
using ObiletJourneyApp.Application.Models.Requests;
using ObiletJourneyApp.Application.Models.Responses;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;


namespace ObiletJourneyApp.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(HttpClient httpClient, ICacheService cache, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClientId()
        {
            var context = _httpContextAccessor.HttpContext;
            var cookie = context?.Request.Cookies["X-Client-Id"];

            if (string.IsNullOrEmpty(cookie))
            {
                var newId = Guid.NewGuid().ToString();
                context?.Response.Cookies.Append("X-Client-Id", newId, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

                return newId;
            }

            return cookie;
        }

        public async Task<SessionDTO> GetSessionAsync()
        {
            string clientId = GetClientId();
            string cacheKey = $"ObiletSession_{clientId}";

            var cachedSession = await _cache.GetAsync<SessionDTO>(cacheKey);
            if (cachedSession != null)
                return cachedSession;

            var request = new SessionRequest
            {
                Type = 2,
                Connection = new Connection { IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1" },
                ApplicationInfo = new ApplicationInfo
                {
                    Version = "1.0.0.0",
                    EquipmentId = "distribusion"
                }
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("client/getsession", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Session API Response: {responseContent}"); // Log response for debugging

                response.EnsureSuccessStatusCode();

                var sessionResponse = JsonSerializer.Deserialize<SessionResponse>(responseContent);
                Console.WriteLine($"Deserialized Status: {sessionResponse?.Status}, Data: {sessionResponse?.Data?.SessionId}, {sessionResponse?.Data?.DeviceId}");

                if (sessionResponse?.Status != "Success" || sessionResponse.Data == null)
                {
                    throw new Exception($"Failed to obtain session. Status: {sessionResponse?.Status}, Response: {responseContent}");
                }

                var sessionDTO = new SessionDTO
                {
                    SessionId = sessionResponse.Data.SessionId,
                    DeviceId = sessionResponse.Data.DeviceId
                };

                await _cache.SetAsync(cacheKey, sessionDTO, TimeSpan.FromMinutes(30));
                return sessionDTO;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error in GetSessionAsync: {ex.Message}");
                throw new Exception("Failed to communicate with the session API.", ex);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error in GetSessionAsync: {ex.Message}");
                throw new Exception("Failed to parse the session API response.", ex);
            }
        }
    }
}
