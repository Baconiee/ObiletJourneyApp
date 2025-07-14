using ObiletJourneyApp.Domain.Exceptions;
using ObiletJourneyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.Entities
{
    public class JourneyData
    {
        public int Id { get; private set; }                
        public int PartnerId { get; private set; }      
        public string PartnerName { get; private set; }
        public int RouteId { get; private set; }
        public string BusType { get; private set; }
        public int TotalSeats { get; private set; }
        public int AvailableSeats { get; private set; }
        public Journey Journey { get; private set; }
        public List<Feature> Features { get; private set; }
        public string OriginLocation { get; private set; }
        public string DestinationLocation { get; private set; }
        public bool IsActive { get; private set; }
        public int OriginLocationId { get; private set; }
        public int DestinationLocationId { get; private set; }
        public bool IsPromoted { get; private set; }
        public int? CancellationOffset { get; private set; }
        public bool HasBusShuttle { get; private set; }
        public bool DisableSalesWithoutGovId { get; private set; }
        public TimeSpan? DisplayOffset { get; private set; }
        public decimal? PartnerRating { get; private set; }

        public JourneyData(
            int id,
            int partnerId,
            string partnerName,
            int routeId,
            string busType,
            int totalSeats,
            int availableSeats,
            List<Feature> features,
            string originLocation,
            string destinationLocation,
            bool isActive,
            int originLocationId,
            int destinationLocationId,
            bool isPromoted,
            int? cancellationOffset,
            bool hasBusShuttle,
            bool disableSalesWithoutGovId,
            TimeSpan? displayOffset,
            decimal? partnerRating)
        {
            Id = id;
            PartnerId = partnerId;
            PartnerName = partnerName;
            RouteId = routeId;
            BusType = busType;
            TotalSeats = totalSeats;
            AvailableSeats = availableSeats;
            Features = features;
            OriginLocation = originLocation;
            DestinationLocation = destinationLocation;
            IsActive = isActive;
            OriginLocationId = originLocationId;
            DestinationLocationId = destinationLocationId;
            IsPromoted = isPromoted;
            CancellationOffset = cancellationOffset;
            HasBusShuttle = hasBusShuttle;
            DisableSalesWithoutGovId = disableSalesWithoutGovId;
            DisplayOffset = displayOffset;
            PartnerRating = partnerRating;

            if (OriginLocationId == DestinationLocationId)
            {
                throw new BusinessRuleViolationException("Origin and destination cannot be the same.");
            }
        }
    }
}
