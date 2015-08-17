using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PropertyMatch.Model
{
    public class PropertyDb
    {
        private PropertyDb() { }
        private static PropertyDb instance { get; set; }

        #region Singelton
        /// <summary>
        /// Single Instance for DB class
        /// </summary>
        public static PropertyDb Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertyDb();
                }
                return instance;
            }
        }
        #endregion
        #region DBData
        /// <summary>
        /// DB entity
        /// </summary>
        /// <returns></returns>
        public List<Property> GetProperties()
        {
            List<Property> lstProperty = new List<Property>
            {
                new Property
                {
                    Name = "Super High Apartments, Sydney",
                    Address = "32 Sir John Young Crescent, Sydney NSW",
                    AgencyCode = "OTBRE",
                    Latitude = 22.00,
                    Longitude = 23.00
                },
                new Property
                {
                    Name = "Perfect Appartment",
                    Address = "Old Street,Parramatta, NSW",
                    AgencyCode = "LRE",
                    Latitude = 14.00180,
                    Longitude = 16.00
                },
                new Property
                {
                    Name = "The Summit Apartments",
                    Address = "Service Road, Bondi , NSW",
                    AgencyCode = "CRE",
                    Latitude = 34.60,
                    Longitude = 19.00180
                }
            };

            return lstProperty;
        }

    }
        #endregion
}
