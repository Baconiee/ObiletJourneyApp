using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.ValueObjects
{
    public class GeoLocation
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public int Zoom { get; }

        public GeoLocation(double latitude, double longitude, int zoom)
        {
            Latitude = latitude;
            Longitude = longitude;
            Zoom = zoom;
        }
    }
}
