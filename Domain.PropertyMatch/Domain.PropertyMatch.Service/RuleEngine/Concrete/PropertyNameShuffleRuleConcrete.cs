using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PropertyMatch.Model;

namespace Domain.PropertyMatch.Service
{
    public class PropertyNameShuffleRuleConcrete : MatchRuleEngineFactory
    {
        #region NameShuffleMatch
        /// <summary>
        /// Method takes in new Property details and compares 
        /// with each item in DB property list ignoring 
        /// word position in Name
        /// </summary>
        /// <param name="matchProperty"></param>
        /// <param name="lstdbProperties"></param>
        /// <returns></returns>
        public override bool AbstractMatchPropertyDetails(Property matchProperty, List<Property> lstdbProperties)
        {
            try
            {
                if (lstdbProperties == null) throw new ArgumentNullException("Not property available in DB ");
                char[] delimiters = new char[] { ' ' };

                lstdbProperties
                    .ForEach(x =>
                        Array.Sort(
                            x.PropertySplit = x.Name.Split(delimiters, StringSplitOptions.RemoveEmptyEntries), StringComparer.InvariantCulture));

                matchProperty.PropertySplit = matchProperty.Name.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                Array.Sort(matchProperty.PropertySplit, StringComparer.InvariantCulture);
                return lstdbProperties
                    .Any(x => x.PropertySplit.SequenceEqual(matchProperty.PropertySplit));
            }
            catch (Exception e)
            {
                throw e;
            }
        } 
        #endregion
    }
}
