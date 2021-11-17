using AdminPanel.Models;
using Microsoft.Maps.MapControl.WPF;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using UserPanel.Commands;
using UserPanel.Models;
using UserPanel.Services;

namespace UserPanel.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class MapVIewModel : BaseViewModel
    {

        public MapVIewModel()
        {
            Centerr = new Location();
            PushPinLocations = new List<Location>(3);
            Locations = new LocationCollection();

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;



            GoCommand = new RelayCommand(a =>
            {
                Locations.Clear();
                if (IsVisiblePin1 == Visibility.Visible)
                {
                    Centerr = GetRouteService.GetRoute(FromLocation, ToLocation, Locations);
                    if (Locations.Count != 0)
                    {
                        From = Locations[0].ToString();
                        To = Locations[Locations.Count - 1].ToString();
                    }
                }
                else
                {
                    Centerr = GetRouteService.GetRoute(From, To, Locations);
                }
                if (Locations.Count > 0)
                {
                    GeoCoordinate ge = new GeoCoordinate(Locations[0].Latitude, Locations[0].Longitude);
                    Distance = (ge.GetDistanceTo(new GeoCoordinate(Locations[Locations.Count - 1].Latitude, Locations[Locations.Count - 1].Longitude)) / 1000).ToString();
                }
            },

            b => !string.IsNullOrWhiteSpace(FromLocation) && !string.IsNullOrWhiteSpace(ToLocation));
            ApplyCommand = new RelayCommand(a => Apply());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (1 < Locations.Count)
            {
                Locations.Remove(Locations.Last());
                TaxiLocation = Locations.Last().Latitude + ", " + Locations.Last().Longitude;
            }
            else
            {
                Timer.Stop();
                System.Windows.MessageBox.Show("You reach your destination");
            }
        }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }



        public RelayCommand GoCommand { get; set; }

        public RelayCommand ApplyCommand { get; set; }


        public string From { get; set; }

        public string To { get; set; }


        public string Distance { get; set; }



        private LocationCollection _locations;

        public LocationCollection Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged("Locations");
            }
        }


        private List<Location> _pushPinLocations;

        public List<Location> PushPinLocations
        {
            get => _pushPinLocations;
            set
            {
                _pushPinLocations = value;
                OnPropertyChanged("PushPinLocations");
            }
        }



        private Location _centerr;

        public Location Centerr
        {
            get => _centerr;
            set
            {
                _centerr = value;
                OnPropertyChanged("Centerr");
            }
        }

        public Visibility IsVisiblePin1 { get; set; } = Visibility.Visible;

        public Visibility IsVisiblePin2 { get; set; } = Visibility.Visible;

        public string TaxiLocation { get; set; }

        public Visibility TaxiVisible { get; set; } = Visibility.Hidden;


        DispatcherTimer Timer;

        public static User Usr { get; set; }

        public static List<User> Users { get; set; }

        public void Apply()
        {


            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            directory = directory.Replace("UserPanel", "AdminPanel");
            List<Driver> Drivers = JsonSaveService<List<Driver>>.Load($@"{directory}\driver");
            List<Location> TaxiLocations = Drivers.Select(d => d.LastLocation).ToList();



            TaxiLocation = FindTaxiService.TaxiLocation(new Location(double.Parse(From.Split(',')[0]), double.Parse(From.Split(',')[1])), TaxiLocations).ToString();
            if(TaxiLocation != null)
            {
                Locations.Clear();
                GetRouteService.GetRoute(From, TaxiLocation, Locations);
                TaxiVisible = Visibility.Visible;
                IsVisiblePin2 = Visibility.Hidden;
                
                Timer.Start();
            }
        }


    }


}
