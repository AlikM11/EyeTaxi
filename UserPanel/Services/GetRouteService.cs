using BingMapsRESTService;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UserPanel.Services
{
    class GetRouteService
    {
        public static void GetResponse(Uri uri, Action<Response> callback)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));

                        if (callback != null)
                        {
                            callback(ser.ReadObject(stream) as Response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adress is incorrect");
            }
        }

        public static void Route(string start, string end, string key, Action<Response> callback)
        {
            Uri requestURI = new Uri(string.Format("http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0={0}&wp.1={1}&rpo=Points&key={2}", Uri.EscapeDataString(start), Uri.EscapeDataString(end), key));
            GetResponse(requestURI, callback);

        }

        public static void GetKey(Action<string> callback)
        {
            var credentialsProvider = new ApplicationIdCredentialsProvider(ConfigurationManager.AppSettings["MapKey"]);
            if (callback != null)
            {
                credentialsProvider.GetCredentials((c) =>
                {
                    callback(c.ApplicationId);
                });
            }
        }

        public static Microsoft.Maps.MapControl.WPF.Location GetRoute(string from, string to, LocationCollection Locations)
        {
            var location = new Microsoft.Maps.MapControl.WPF.Location();
            GetKey((c) =>
            {
                Route(from, to, c, (r) =>
                {

                    if (r != null &&
                       r.ResourceSets != null &&
                       r.ResourceSets.Length > 0 &&
                       r.ResourceSets[0].Resources != null &&
                       r.ResourceSets[0].Resources.Length > 0)
                    {

                        Route route = r.ResourceSets[0].Resources[0] as Route;

                        double[][] routePath = route.RoutePath.Line.Coordinates;
                        LocationCollection locs = new LocationCollection();

                        for (int i = 0; i < routePath.Length; i++)
                        {
                            if (routePath[i].Length >= 2)
                            {
                                locs.Add(new Microsoft.Maps.MapControl.WPF.Location(routePath[i][0], routePath[i][1]));
                            }
                        }

                        MapPolyline routeLine = new MapPolyline()
                        {
                            Locations = locs,
                            Stroke = new SolidColorBrush(Colors.Blue),
                            StrokeThickness = 5
                        };

                        foreach (var item in locs)
                        {
                            Locations.Add(item);
                        }

                        location.Latitude = routePath[0][0];
                        location.Longitude = routePath[0][1];
                    }
                    else
                    {
                        MessageBox.Show("No Results found.");
                    }
                });
            });
            return location;
        }

    }
}
