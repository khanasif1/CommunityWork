using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PropertyMatch.Model
{
    public class Property
    {
        public string Address { get; set; }
        public string AgencyCode { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String[] PropertySplit { get; set; }
    }

}
