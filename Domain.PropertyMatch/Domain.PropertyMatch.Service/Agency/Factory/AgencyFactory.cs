using Domain.PropertyMatch.IService;
using Domain.PropertyMatch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PropertyMatch.Service
{
    public abstract class AgencyFactory : IPropertyMatcher
    {
        public abstract MatchResult IsMatch(Model.Property agencyProperty, List<Model.Property> databaseProperty);
        
    }
}
