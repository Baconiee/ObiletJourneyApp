using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.ValueObjects
{
    public sealed class Policy
    {
        public int? MaxSeats { get; private set; }
        public int? MaxSingle { get; private set; }
        public int? MaxSingleMales { get; private set; }
        public int? MaxSingleFemales { get; private set; }
        public bool MixedGenders { get; private set; }
        public bool GovId { get; private set; }
        public bool Lht { get; private set; }

        public Policy(
            int? maxSeats,
            int? maxSingle,
            int? maxSingleMales,
            int? maxSingleFemales,
            bool mixedGenders,
            bool govId,
            bool lht)
        {
            MaxSeats = maxSeats;
            MaxSingle = maxSingle;
            MaxSingleMales = maxSingleMales;
            MaxSingleFemales = maxSingleFemales;
            MixedGenders = mixedGenders;
            GovId = govId;
            Lht = lht;
        }
    }
}
