using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PropertyMatch.Model;

namespace Domain.PropertyMatch.Service
{
    public class PunctuationRuleConcrete : MatchRuleEngineFactory
    {
        #region PunctuationMatch
        /// <summary>
        /// Method takes in new Property details and compares 
        /// with each item in DB property list ignoring punctuations
        /// </summary>
        /// <param name="matchProperty"></param>
        /// <param name="lstdbProperties"></param>
        /// <returns></returns>
        
        public override bool AbstractMatchPropertyDetails(Property matchProperty, List<Property> lstdbProperties)
        {
            try
            {
                if (lstdbProperties == null) throw new ArgumentNullException("Not property available in DB ");

                ///Remove punctuations from Name and Address in DB list
                foreach (var property in lstdbProperties)
                {
                    property.Name = property.Name
                        .Where(c => !char.IsPunctuation(c))
                        .Aggregate(new StringBuilder(),
                            (current, next) => current.Append(next), sb => sb.ToString());
                    property.Address = property.Address
                        .Where(c => !char.IsPunctuation(c))
                        .Aggregate(new StringBuilder(),
                            (current, next) => current.Append(next), sb => sb.ToString());

                }
                /// Remove punctuations from Name and Address in new property entity
                var propertyName = matchProperty.Name
                    .Where(c => !char.IsPunctuation(c))
                    .Aggregate(new StringBuilder(),
                        (current, next) => current.Append(next), sb => sb.ToString());
                var propertyAddress = matchProperty.Address
                    .Where(c => !char.IsPunctuation(c))
                    .Aggregate(new StringBuilder(),
                        (current, next) => current.Append(next), sb => sb.ToString());

                ///compare Name and Address with DB list
                bool resultName = lstdbProperties.Any(property => property.Name != null && property.Name.Replace(" ", String.Empty).ToUpper().Equals(propertyName.Replace(" ", String.Empty).ToUpper()));
                bool resultAddress = lstdbProperties.Any(property => propertyAddress != null && property.Address.Replace(" ", String.Empty).ToUpper().Equals(propertyAddress.Replace(" ", String.Empty).ToUpper()));

                ///If  Name and Address both match return successful match 
                return (resultName && resultAddress);
            }
            catch (Exception e)
            {
                throw e;
            }
        } 
        #endregion

    }
}
