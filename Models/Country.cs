using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeoSilhouette
{
    public class Country
    {
        [JsonPropertyName("name")]
        public NameWrapper wrapper { get; set; } = new NameWrapper();

        public string cca2 { get; set; } = string.Empty;
        public int population { get; set; }
        public List<string> continents { get; set; } = new List<string>();
        public List<double> latlng { get; set; } = new List<double>();
        public bool unMember { get; set; }

        public string SilhouetteImage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(cca2))
                    return null;

                return $"https://raw.githubusercontent.com/djaiss/mapsicon/master/all/{cca2.ToLower()}/1024.png";
            }
        }

        public string realName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(wrapper.common))
                    return null;

                return wrapper.common;
            }
        }

        public class NameWrapper
        {
            public string common { get; set; } = string.Empty;
            public string official { get; set; } = string.Empty;
        }


        private static readonly HttpClient _http = new HttpClient();

        public async Task<bool> ImageExistsAsync()
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, this.SilhouetteImage);
                using var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static string GetDirection(Country o, Country t)
        {
            var origin = o.latlng;
            var target = t.latlng;

            // Safety check: Ensure both lists have at least 2 items (Lat, Lng)
            if (origin == null || origin.Count < 2 || target == null || target.Count < 2)
                return "Unknown";

            // Convert degrees to radians
            double lat1 = ToRadians(origin[0]);
            double lon1 = ToRadians(origin[1]);
            double lat2 = ToRadians(target[0]);
            double lon2 = ToRadians(target[1]);

            double dLon = lon2 - lon1;

            // Calculate Bearing using the formula
            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);

            // Calculate initial bearing in degrees (0° to 360°)
            double brng = Math.Atan2(y, x);
            double degrees = (ToDegrees(brng) + 360) % 360;

            // Convert degrees to cardinal direction strings
            return DegreesToCardinal(degrees);
        }

        private static double ToRadians(double degrees) => degrees * (Math.PI / 180);
        private static double ToDegrees(double radians) => radians * (180 / Math.PI);

        private static string DegreesToCardinal(double degrees)
        {
            string[] cardinals =
{
    "North ⇑",
    "North East ⇗",
    "East ⇒",
    "South East ⇘",
    "South ⇓",
    "South West ⇙",
    "West ⇐",
    "North West ⇖",
    "North ⇑"
};


            // Divide by 45 to get the index (8 sectors of 45 degrees)
            // Adding 22.5 shifts the sectors so "North" covers 337.5° to 22.5°
            int index = (int)Math.Round(((double)degrees % 360) / 45);

            return cardinals[index];
        }

        public static int GetDistance(Country o, Country t)
        {
            // Safety check: Ensure both countries and their coordinates exist
            if (o?.latlng == null || o.latlng.Count < 2 ||
                t?.latlng == null || t.latlng.Count < 2)
            {
                return 500000; // :)
            }

            double R = 6371; // Radius of the Earth in km
            double dLat = ToRadians(t.latlng[0] - o.latlng[0]);
            double dLon = ToRadians(t.latlng[1] - o.latlng[1]);

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(o.latlng[0])) * Math.Cos(ToRadians(t.latlng[0])) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c; // Distance in km

            // Return the rounded integer with "km" appended
            return (int)Math.Round(distance);
        }
    }
}
