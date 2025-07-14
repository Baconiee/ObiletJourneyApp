using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Application.DTOs
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        public byte? Priority { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPromoted { get; set; }
        public string? BackColor { get; set; }
        public string? ForeColor { get; set; }
    }
}
