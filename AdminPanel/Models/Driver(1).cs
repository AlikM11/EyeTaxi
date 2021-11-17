using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Driver
    {
        public float Balance { get; set; }

        public float Rating { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public Location LastLocation { get; set; }

        public Car Car { get; set; }

        public Driver(float balance, float rating, string fullName, string phoneNumber, Location lastLocation, Car car)
        {
            Balance = balance;
            Rating = rating;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            LastLocation = lastLocation;
            Car = car;
        }

        public Driver()
        {
            LastLocation = new Location();
            Car = new Car("a", "b", "c", 3);
        }
    }
}
