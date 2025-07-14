using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Domain.Entities
{
    public class Feature
    {
        public int Id { get; private set; }
        public byte? Priority { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsPromoted { get; private set; }
        public string? BackColor { get; private set; }
        public string? ForeColor { get; private set; }

        public Feature(
            int id,
            byte? priority,
            string name,
            string? description,
            bool isPromoted,
            string? backColor,
            string? foreColor)
        {
            Id = id;
            Priority = priority;
            Name = name;
            Description = description;
            IsPromoted = isPromoted;
            BackColor = backColor;
            ForeColor = foreColor;
        }
    }
}
