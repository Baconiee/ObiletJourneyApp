using Microsoft.Extensions.DependencyInjection;
using ObiletJourneyApp.Application.Services;
using ObiletJourneyApp.Infrastructure.Services;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace ObiletJourneyApp.CompositionRoot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register IHttpContextAccessor for accessing HTTP context in SessionService
            services.AddHttpContextAccessor();

            // HTTP Clients
            services.AddHttpClient<ISessionService, SessionService>(client =>
            {
                client.BaseAddress = new Uri("https://v2-api.obilet.com/api/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
            });

            services.AddHttpClient<IObiletService, ObiletService>(client =>
            {
                client.BaseAddress = new Uri("https://v2-api.obilet.com/api/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
            });

            // Register Redis for distributed caching
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379";
                options.InstanceName = "ObiletApp_";
            });

            // Register ICacheService with CacheService implementation
            services.AddSingleton<ICacheService, CacheService>();

            // Configure session management
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "ObiletApp.Session";
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            return services;
        }
    }
}