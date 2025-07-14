    using ObiletJourneyApp.Domain.ValueObjects;
using ObiletJourneyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.Entities
{
    public class Journey
    {
        public string Kind { get; private set; }
        public string Code { get; private set; }
        public List<Stop> Stops { get; private set; }
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public DateTime Departure { get; private set; } = DateTime.Today.AddDays(1);
        public DateTime Arrival { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Currency { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public decimal InternetPrice { get; private set; }
        public string BusName { get; private set; }
        public Policy Policy { get; private set; }
        public List<string> FeatureDescriptions { get; private set; }

        public Journey(
            string kind,
            string code,
            List<Stop> stops,
            string origin,
            string destination,
            DateTime departure,
            DateTime arrival,
            TimeSpan duration,
            string currency,
            decimal originalPrice,
            decimal internetPrice,
            string busName,
            Policy policy,
            List<string> featureDescriptions)
        {
            Kind = kind;
            Code = code;
            Stops = stops ?? throw new ArgumentNullException(nameof(stops));
            Origin = origin;
            Destination = destination;
            Departure = departure;
            Arrival = arrival;
            Duration = duration;
            Currency = currency;
            OriginalPrice = originalPrice;
            InternetPrice = internetPrice;
            BusName = busName;
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
            FeatureDescriptions = featureDescriptions;

            if (Departure < DateTime.Now)
            {
                throw new BusinessRuleViolationException("Departure time cannot be in the past.");
            }
            if (Departure >= Arrival)
            {
                throw new BusinessRuleViolationException("Arrival time must be after departure time.");
            }
        }
    }
}
