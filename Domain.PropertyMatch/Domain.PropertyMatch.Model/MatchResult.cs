using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PropertyMatch.Model
{
    public class MatchResult
    {
        public bool IsException { get; set; }
        public string ExceptionMessage { get; set; }
        public bool IsValidMatch { get; set; }
    }
}
