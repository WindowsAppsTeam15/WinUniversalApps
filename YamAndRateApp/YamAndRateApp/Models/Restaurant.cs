using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamAndRateApp.Models
{
    public class Restaurant
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<string> Specialties { get; set; }

        public IList<Vote> Votes { get; set; }

        public string PhotoUrl { get; set; }

        public Coordinates Coords { get; set; }

        public Category Category { get; set; }
    }
}
