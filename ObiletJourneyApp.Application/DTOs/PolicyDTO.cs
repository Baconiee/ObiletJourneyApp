using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Application.DTOs
{
    public sealed class PolicyDTO
    {
        public int? MaxSeats { get; set; }
        public int? MaxSingle { get; set; }
        public int? MaxSingleMales { get; set; }
        public int? MaxSingleFemales { get; set; }
        public bool MixedGenders { get; set; }
        public bool GovId { get; set; }
        public bool Lht { get; set; }
    }
}
