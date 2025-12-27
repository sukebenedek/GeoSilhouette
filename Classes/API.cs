using GeoSilhouette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.Classes
{
    public static class API
    {
        //public static async Task<List<GeoSilhouette.Country>?> GetLibrary(string steamID) => (await HTTPCommunication<CountryRootObj>.Get($"https://restcountries.com/v3.1/all?fields=name,cca2,population,continents,latlng,unMember"))?.response?.ToList();


        public static async Task<List<Country>?> GetCountries()
        {
            return await HTTPCommunication<List<Country>>
                .Get("https://restcountries.com/v3.1/all?fields=name,cca2,population,continents,latlng,unMember");
        }

    }
}
