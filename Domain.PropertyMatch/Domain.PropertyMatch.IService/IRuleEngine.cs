using Domain.PropertyMatch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PropertyMatch.IService
{
    public interface IRuleEngine
    {
        /// <summary>
        /// Interface open for Testing and extending rules
        /// </summary>
        /// <param name="matchProperty"></param>
        /// <param name="lstdbProperties"></param>
        /// <returns></returns>
        bool AbstractMatchPropertyDetails(Property matchProperty, List<Property> lstdbProperties);
    }
}
