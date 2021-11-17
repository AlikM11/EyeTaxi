using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace UserPanel.Services
{
    public class AutoSuggestService
    {
        //query=fish&
        //userLocation=47.668697,-122.376373,5
        //&userCircularMapView=47.668697,-122.376373,100
        //&includeEntityTypes=Business
        //&key=<BingMapKey>

        public static string GetKey()
        {
            var credentialsProvider = new ApplicationIdCredentialsProvider(ConfigurationManager.AppSettings["MapKey"]);
            return credentialsProvider.ApplicationId;
        }

        public string Url(string query, string userLocation, string radius, string key)
        {
            string requestURI = "http://dev.virtualearth.net/REST/v1/Autosuggest?query="
                + query +
                "&userLocation="
                + userLocation + ",5" 
                + "&userCircularMapView="
                + userLocation
                + "," + radius
                + "&includeEntityTypes=Address&key="
                + key;
            return requestURI;
        }

        public string[] GetResponse(string query, string userLocation, string radius)
        {
            var client = new HttpClient();
            dynamic json = JsonConvert.DeserializeObject(client.GetAsync(Url(query, userLocation, radius, GetKey())).Result.Content.ReadAsStringAsync().Result);

            string v = JsonConvert.SerializeObject(json, Formatting.Indented);
            dynamic temp = JsonConvert.DeserializeObject(v);
            string[] address = new string[10];
            int i = 0;
            foreach (dynamic item in temp.resourceSets[0].resources[0].value)
            {
                address[i] = item.address.formattedAddress;
                i++;
            }
            return address;
        }
    }
}
