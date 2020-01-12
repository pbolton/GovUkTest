using GovUkApi.Global;
using GovUkApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GovUkApi
{
    public class GovUkApi : IGovUkApi
    {
        private readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Constructor
        /// </summary>
        public GovUkApi()
        {
            httpClient.BaseAddress = new Uri(Constants.ServiceURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets a list of users from the base coordinates within miles
        /// </summary>
        /// <param name="baseCoords"></param>
        /// <param name="milesWithin"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsersInArea(Coords baseCoords = null, double milesWithin = 50)
        {
            //
            // Check if default base coordinates - London
            //
            if (baseCoords == null)
            {
                baseCoords = new Coords { latitude = Constants.LondonLatitude, longitude = Constants.LondonLongitude };
            }

            var users = JsonConvert.DeserializeObject<List<User>>(await httpClient.GetStringAsync("users"));

            return users.Where(_ => getDistanceInMilesFromCoords(_.latitude, _.longitude) <= milesWithin);

            //
            // Local functions
            //
            double getDistanceInMilesFromCoords(double latitude, double longitude)
            {
                var dLat = ConvertDeg2Rad(baseCoords.latitude - latitude);
                var dLon = ConvertDeg2Rad(baseCoords.longitude - longitude);

                var arc = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(ConvertDeg2Rad(latitude)) * Math.Cos(ConvertDeg2Rad(latitude)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                var distanceInKm = Constants.RadiusOfEathInKm * (2 * Math.Atan2(Math.Sqrt(arc), Math.Sqrt(1 - arc)));

                double ConvertDeg2Rad(double deg)
                {
                    return deg * (Math.PI / 180);
                }

                return distanceInKm / Constants.KmMile;
            }
        }
    }
}
