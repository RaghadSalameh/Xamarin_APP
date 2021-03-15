using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Xamarin_App.Models;

namespace Xamarin_App
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private const string Url = "your_api_link";
        HttpClient client = new HttpClient();
        Xamarin.Forms.Maps.Map Mymap;
        ActivityIndicator activityIndicator;
        List<Polygon> polygons = new List<Polygon>();
        public MainPage(Field field = null)
        {
            InitializeComponent();
            LoadMap(field);
        }

        public void LoadMap(Field field=null)
        {
            activityIndicator = new ActivityIndicator { IsRunning = false, Color = Color.Orange, IsVisible = false };
            Position position = new Position(51.133481, 10.018343);
            MapSpan mapSpan = new MapSpan(position, 51, 10);
            Mymap = new Xamarin.Forms.Maps.Map(mapSpan)
            {
                MapType = MapType.Hybrid
            };

            if (field == null)
            {
                List<Field> fields = App.Database.GetFields();
                foreach (Field f in fields)
                {
                    CreatePolygon(f);
                }
                Mymap.MapClicked += Map_MapClicked1Async;
            }
            else
            {
                CreatePolygon(field);
            }

            //Show the user the appropriate map area
            if (polygons.Count() > 0)
            {
                SetMapPosition(polygons);
            }

            Content = new StackLayout
            {
                Children =
                {
                    Mymap,
                    activityIndicator
                }
            };
        }

        private async void Map_MapClicked1Async(object sender, MapClickedEventArgs e)
        {
            //Position position = new Position(e.Position.Latitude, e.Position.Longitude);
            ((Xamarin.Forms.Maps.Map)sender).IsEnabled = false;
            Pin pin = new Pin
            {
                Label = "",
                Address = "",
                Type = PinType.SearchResult,
                Position = new Position(e.Position.Latitude, e.Position.Longitude)
            };
            Mymap.Pins.Add(pin);
            //Mymap.MoveToRegion(new MapSpan(position, position.Latitude, position.Longitude));
            var tapped = new TappedPosition { accuracy = 3, fallback_boundary = false, latitude = e.Position.Latitude, longitude = e.Position.Longitude };
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            string contentResponse = await GetTappedPositionPolygonAsync(tapped, true);
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            Mymap.Pins.Remove(pin);
            if (contentResponse == "No Content")
            {
                await DisplayAlert("Alert", "No Content", "Ok");
            }
            else if (contentResponse != "Something went wrong")
            {
                boundary Boundary = JsonConvert.DeserializeObject<boundary>(contentResponse);
                //await DisplayAlert("Alert", contentResponse, "Ok");
                Polygon polygon = new Polygon
                {
                    StrokeWidth = 8,
                    StrokeColor = Color.FromHex("#1BA1E2"),
                    FillColor = Color.FromHex("#881BA1E2"),
                };
                foreach (var p in Boundary.features[0].geometry.coordinates[0])
                {
                    polygon.Geopath.Add(new Position(p[1], p[0]));
                }
                // add the polygon to the map's MapElements collection
                Mymap.MapElements.Add(polygon);
                polygons = new List<Polygon>() { polygon };
                SetMapPosition(polygons);
                var NamingPage = new PopUpNamingFieldPage(Boundary);
                NamingPage.UpdateMapEvent += () =>
                {
                    LoadMap();
                };
                await PopupNavigation.Instance.PushAsync(NamingPage);
            }
             ((Xamarin.Forms.Maps.Map)sender).IsEnabled = true;
        }

        public async Task<string> GetTappedPositionPolygonAsync(TappedPosition item, bool isNewItem = false)
        {
            //boundary Boundary = new boundary();
            var uri = new Uri(Url);
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.Add("apikey", "your_api_key");
            HttpResponseMessage response = null;
            if (isNewItem)
            {
                if (Iscoonected() == true)
                {
                    try
                    {
                        response = await client.PostAsync(uri, content);
                    }
                    catch (Exception e)
                    {
                        await DisplayAlert("Alert", e.Message, "Ok");
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return "No Content";
                        }
                        else
                        {
                            var contentResponse = await response.Content.ReadAsStringAsync();
                            return contentResponse;
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Alert", "Please Check Your Internet Connection And Try Again", "Ok");
                }
            }

            return "Something went wrong";
        }

        public void SetMapPosition(List<Polygon> polygons)
        {
            var minLat = polygons.SelectMany(p => p.Geopath).Min(p => p.Latitude);
            var maxLat = polygons.SelectMany(p => p.Geopath).Max(p => p.Latitude);
            var minLon = polygons.SelectMany(p => p.Geopath).Min(p => p.Longitude);
            var maxLon = polygons.SelectMany(p => p.Geopath).Max(p => p.Longitude);
            var centerLat = (minLat + maxLat) / 2;
            var centerLon = (minLon + maxLon) / 2;

            Mymap.MoveToRegion(
                new MapSpan(
                    new Position(centerLat, centerLon),
                    Math.Abs(maxLat - minLat) * 1.2,
                    Math.Abs(maxLon - minLon) * 1.2));
        }

        public void CreatePolygon(Field f)
        {
            Polygon polygon = new Polygon
            {
                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#1BA1E2"),
                FillColor = Color.FromHex("#881BA1E2"),
            };
            f.coordinates = App.Database.GetCoordinate(f.Id);
            foreach (Coordinate c in f.coordinates.ToList())
            {
                polygon.Geopath.Add(new Position(c.lat, c.lng));
            }
            // add the polygon to the map's MapElements collection
            Mymap.MapElements.Add(polygon);
            polygons.Add(polygon);

            Position p = new Position(f.coordinates[0].lat, f.coordinates[0].lng);
            Pin pin = new Pin
            {
                Label = "",
                Address = "",
                Type = PinType.SearchResult,
                Position = p
            };
            Mymap.Pins.Add(pin);
        }

        public bool Iscoonected()
        {
            //return CrossConnectivity.Current.IsConnected;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }
    }
}
