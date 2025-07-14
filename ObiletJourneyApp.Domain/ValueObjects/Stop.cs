using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.ValueObjects
{
    public class Stop
    {
        public string Name { get; private set; }
        public int? Station { get; private set; }
        public DateTime Time { get; private set; }
        public bool IsOrigin { get; private set; }
        public bool IsDestination { get; private set; }

        public Stop(string name, int? station, DateTime time, bool isOrigin, bool isDestination)
        {
            Name = name;
            Station = station;
            Time = time;
            IsOrigin = isOrigin;
            IsDestination = isDestination;
        }
    }
}
