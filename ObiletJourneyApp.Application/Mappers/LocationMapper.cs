using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObiletJourneyApp.Domain.Entities;
using ObiletJourneyApp.Application.DTOs;
using ObiletJourneyApp.Domain.ValueObjects;

namespace ObiletJourneyApp.Application.Mappers
{
    public static class LocationMapper
    {
        public static Location ToDomain(LocationDTO dto)
        {
            return new Location(
                id: dto.Id,
                parentId: dto.ParentId,
                type: dto.Type,
                name: dto.Name,
                geoLocation: new GeoLocation(dto.GeoLocation.Latitude, dto.GeoLocation.Longitude, dto.GeoLocation.Zoom),
                timeZoneCode: dto.TimeZoneCode,
                weatherCode: dto.WeatherCode,
                rank: dto.Rank,
                referenceCode: dto.ReferenceCode,
                keywords: dto.Keywords
            );
        }
    }
}
