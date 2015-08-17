using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.PropertyMatch.Model;
using Newtonsoft.Json.Linq;

namespace Domain.PropertyMatch.Service
{
    /// <summary>
    /// Engine for Location
    /// </summary>
    public class LocationRuleConcrete : MatchRuleEngineFactory
    {

        public static double distance = 0.0;
        #region LocationMatch
        /// <summary>
        /// Method will validate if the address provided is in 
        /// sync with Co-ordinates shared. A variance of
        /// 200m is accepted
        /// </summary>
        /// <param name="matchProperty"></param>
        /// <param name="lstdbProperties"></param>
        /// <returns></returns>
        public override bool AbstractMatchPropertyDetails(Property matchProperty, List<Property> lstdbProperties)
        {
            try
            {
                if (matchProperty.Address == string.Empty || (matchProperty.Latitude == 0 || matchProperty.Longitude == 0))
                    throw new ArgumentNullException("Address or Co-ordinates are missing");
                GetDistance(matchProperty).Wait();
                if (distance < 200)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region GoogleDistanceMatrixApiCall
        /// <summary>
        /// Method to GetDistance between the location Co-ordinate
        /// and Address using Google Distance Matrix service.
        /// Note: The same feature is also available with Bing
        /// </summary>
        /// <param name="matchProperty"></param>
        /// <returns></returns>
        static async Task GetDistance(Property matchProperty)
        {
            string GoogleApiBaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?";
            string GoogleApiConfigParam = "&mode=bicycling&language=fr-FR";
            string addressLatlong = matchProperty.Latitude + "," + matchProperty.Longitude;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response =
                    await
                        client.GetAsync(String.Format("{0}origins={1}&destinations={2}{3}", GoogleApiBaseUrl,
                            matchProperty.Address, addressLatlong, GoogleApiConfigParam));

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    if (responseString != String.Empty)
                    {
                        JObject root = JObject.Parse(responseString);
                        JArray items = (JArray)root["rows"];
                        JArray elements = (JArray)items[0]["elements"];
                        JObject element = (JObject)elements[0];
                        distance = ((dynamic)element).distance.value;
                    }
                }
            }
        }
        #endregion
    }
}
