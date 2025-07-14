using System;
using System.Collections.Generic;
using System.Linq;
using ObiletJourneyApp.Application.DTOs;
using ObiletJourneyApp.Domain.Entities;
using ObiletJourneyApp.Domain.ValueObjects;

namespace ObiletJourneyApp.Application.Mappers
{
    public static class JourneyMapper
    {
        public static JourneyData ToDomain(JourneyDataDTO dto)
        {
            // Convert Stops
            var stops = dto.Journey.Stops?.Select(s => new Stop(
                name: s.Name,
                station: s.Station,
                time: s.Time,
                isOrigin: s.IsOrigin,
                isDestination: s.IsDestination
            )).ToList() ?? new List<Stop>();

            // Convert Policy
            var policy = new Policy(
                maxSeats: dto.Journey.Policy?.MaxSeats,
                maxSingle: dto.Journey.Policy?.MaxSingle,
                maxSingleMales: dto.Journey.Policy?.MaxSingleMales,
                maxSingleFemales: dto.Journey.Policy?.MaxSingleFemales,
                mixedGenders: dto.Journey.Policy?.MixedGenders ?? false,
                govId: dto.Journey.Policy?.GovId ?? false,
                lht: dto.Journey.Policy?.Lht ?? false
            );

            // Convert inner Journey object
            var journey = new Journey(
                kind: dto.Journey.Kind,
                code: dto.Journey.Code,
                stops: stops,
                origin: dto.Journey.Origin,
                destination: dto.Journey.Destination,
                departure: dto.Journey.Departure,
                arrival: dto.Journey.Arrival,
                duration: dto.Journey.Duration,
                currency: dto.Journey.Currency,
                originalPrice: dto.Journey.OriginalPrice,
                internetPrice: dto.Journey.InternetPrice,
                busName: dto.Journey.BusName,
                policy: policy,
                featureDescriptions: dto.Journey.FeatureDescriptions ?? new List<string>()
            );

            // Convert Feature list
            var features = dto.Features?.Select(f => new Feature(
                id: f.Id,
                priority: f.Priority,
                name: f.Name,
                description: f.Description,
                isPromoted: f.IsPromoted,
                backColor: f.BackColor,
                foreColor: f.ForeColor
            )).ToList() ?? new List<Feature>();

            // Return full JourneyData domain model
            var journeyData = new JourneyData(
                id: dto.Id,
                partnerId: dto.PartnerId,
                partnerName: dto.PartnerName,
                routeId: dto.RouteId,
                busType: dto.BusType,
                totalSeats: dto.TotalSeats,
                availableSeats: dto.AvailableSeats,
                features: features,
                originLocation: dto.OriginLocation,
                destinationLocation: dto.DestinationLocation,
                isActive: dto.IsActive,
                originLocationId: dto.OriginLocationId,
                destinationLocationId: dto.DestinationLocationId,
                isPromoted: dto.IsPromoted,
                cancellationOffset: dto.CancellationOffset,
                hasBusShuttle: dto.HasBusShuttle,
                disableSalesWithoutGovId: dto.DisableSalesWithoutGovId,
                displayOffset: dto.DisplayOffset,
                partnerRating: dto.PartnerRating
            );

            // Set the inner Journey manually (constructor does not accept it)
            journeyData.GetType()
                .GetProperty("Journey")!
                .SetValue(journeyData, journey);

            return journeyData;
        }
    }
}
