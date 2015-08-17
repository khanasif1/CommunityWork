using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PropertyMatch.Model;
using Domain.PropertyMatch.IService;
using Domain.PropertyMatch.Service;
namespace Domain.PropertyMatch.Service
{
    public class CREConcrete : AgencyFactory
    {
        #region PropertyMatcherClient
        /// <summary>
        /// Method to Match new property with  properties in DB
        /// </summary>
        /// <param name="agencyProperty"></param>
        /// <returns></returns>
        public override MatchResult IsMatch(Property agencyProperty, List<Property> databaseProperty)
        {
            var lstProperty = databaseProperty;
            MatchResult result = new MatchResult();
            MatchRuleEngineFactory matchRuleEngine;
            StringBuilder errorSb = new StringBuilder();            
            #region Agency:CRE

            if (agencyProperty.AgencyCode.Equals(Agency.CRE.ToString()))
            {
                try
                {
                    matchRuleEngine = new PropertyNameShuffleRuleConcrete();
                    result.IsValidMatch = matchRuleEngine.AbstractMatchPropertyDetails(agencyProperty, lstProperty);

                }
                catch (Exception ex)
                {
                    errorSb.Append("*****************************Error Processing Agency: Contrary Real Estate*****************************");
                    errorSb.Append("Exception Message:" + ex.Message.ToString());
                    errorSb.Append(Environment.NewLine);
                    errorSb.Append("Exception Stack:" + ex.StackTrace.ToString());
                    errorSb.Append(Environment.NewLine);
                }
            }
            
            #endregion            
            #region Error
            if (errorSb.ToString() != string.Empty)
            {
                result.IsException = true;
                result.ExceptionMessage = errorSb.ToString();
                result.IsValidMatch = false;
            }
            
            #endregion
            return result;
        } 
        #endregion

    }
}
