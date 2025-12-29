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


    }
}
