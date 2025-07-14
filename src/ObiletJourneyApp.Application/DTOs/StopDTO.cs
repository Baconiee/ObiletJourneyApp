using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Application.DTOs
{
    public class StopDTO
    {
        public string Name { get; set; }
        public int? Station { get; set; }
        public DateTime Time { get; set; }
        public bool IsOrigin { get; set; }
        public bool IsDestination { get; set; }
    }
}
