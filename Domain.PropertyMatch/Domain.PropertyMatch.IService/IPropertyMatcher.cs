using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PropertyMatch.Model;

namespace Domain.PropertyMatch.IService
{
    #region ClientInterface
    /// <summary>
    /// Interface exposed for consumers, testing and extending agency
    /// </summary>
    public interface IPropertyMatcher
    {
        MatchResult IsMatch(Property agencyProperty, List<Property> databaseProperty);
    } 
    #endregion
}
