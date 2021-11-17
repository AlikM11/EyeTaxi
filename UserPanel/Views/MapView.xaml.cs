using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using UserPanel.Services;
using static UserPanel.Services.GetAdressService;

namespace UserPanel.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public bool Help { get; set; } = false;

        public ArrayList Pins { get; set; } = new ArrayList();

        public AutoSuggestService Auto { get; set; } = new AutoSuggestService();


        public MapView()
        {
            InitializeComponent();
            Map.CredentialsProvider = new ApplicationIdCredentialsProvider(ConfigurationManager.AppSettings["MapKey"]);

            FromLocation.FilterMode = AutoCompleteFilterMode.Contains;
            FromLocation.ItemsSource = new string[] { };
            ToLocation.FilterMode = AutoCompleteFilterMode.Contains;
            ToLocation.ItemsSource = new string[] { };

        }


        private void ChckBox_Click(object sender, RoutedEventArgs e)
        {
            if (ChckBox.IsChecked == true)
            {
                FromLocation.IsEnabled = false;
                ToLocation.IsEnabled = false;
                Pin1.Visibility = Visibility.Visible;
                Pin2.Visibility = Visibility.Hidden;
            }
            if (ChckBox.IsChecked == false)
            {
                FromLocation.IsEnabled = true;
                ToLocation.IsEnabled = true;
                Pin2.Visibility = Visibility.Visible;
                Pin1.Visibility = Visibility.Visible;
                Pin1.Location = default;
                Pin2.Location = default;

                foreach (var item in Pins)
                {
                    Map.Children.Remove(item as Pushpin);
                }
                Pins.Clear();
            }

            Route.Locations.Clear();
            FromLocation.Text = "";
            ToLocation.Text = "";
            Help = false;
        }


        private void Map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ChckBox.IsChecked == true)
            {
                e.Handled = true;

                Point mousePosition = e.GetPosition(this);
                Location pinLocation = Map.ViewportPointToLocation(mousePosition);

                Pushpin pin = new Pushpin();
                pin.Location = pinLocation;

                if (Help == false)
                {
                    Route.Locations.Clear();
                    foreach (var item in Pins)
                    {
                        Map.Children.Remove(item as Pushpin);
                    }
                    Pins.Clear();
                    FromLocation.Text = "";
                    ToLocation.Text = "";

                    //FromLocation.Text = pinLocation.ToString();
                    RootObject rootObject = getAddress(pinLocation.Latitude, pinLocation.Longitude);
                    FromLocation.Text = rootObject.display_name;
                    Pin1.Location = pinLocation;
                    Help = true;
                }
                else
                {
                    RootObject rootObject = getAddress(pinLocation.Latitude, pinLocation.Longitude);
                    ToLocation.Text = rootObject.display_name;
                    Pin2.Location = pinLocation;
                    Help = false;
                }
                Map.Children.Add(pin);
                Pins.Add(pin);
            }
        }


        private void FromLocation_TextChanged(object sender, RoutedEventArgs e)
        {
            if (FromLocation.Text.Length > 2 && FromLocation.Text.Length < 10)
                FromLocation.ItemsSource = Auto.GetResponse(FromLocation.Text, "40.409264,49.867092", "30000");
            else if (FromLocation.Text.Length == 0)
            {
                FromLocation.ItemsSource = new string[] { };
            }
        }

        private void ToLocation_TextChanged(object sender, RoutedEventArgs e)
        {
            if (ToLocation.Text.Length > 2 && ToLocation.Text.Length < 10)
                ToLocation.ItemsSource = Auto.GetResponse(ToLocation.Text, "40.409264,49.867092", "30000");
            else if (ToLocation.Text.Length == 0)
            {
                ToLocation.ItemsSource = new string[] { };
            }
        }
    }




}