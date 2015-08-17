using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PropertyMatch.Model;
using Domain.PropertyMatch.IService;

namespace Domain.PropertyMatch.Service
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MatchRuleEngineFactory : IRuleEngine
   {
       public abstract bool AbstractMatchPropertyDetails(Property matchProperty, List<Property> lstdbProperties);
   }
}

