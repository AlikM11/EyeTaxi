using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Car
    {
        public string Model { get; set; }

        public string Vendor { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public Car(string model, string vendor, string color, int year)
        {
            Model = model;
            Vendor = vendor;
            Color = color;
            Year = year;
        }
    }
}
