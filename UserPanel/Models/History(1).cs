using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.Models
{
    class History
    {

        public Driver Driver { get; set; }

        public string TravelDate { get; private set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string TravelDuration { get; set; }

        public float Price { get; set; }

        public float Distance { get; set; } //with km

        public History(Driver driver, string startPoint, string endPoint, string travelDuration, float price, float distance)
        {
            TravelDate = DateTime.Now.ToLongDateString();
            Driver = driver;
            StartPoint = startPoint;
            EndPoint = endPoint;
            TravelDuration = travelDuration;
            Price = price;
            Distance = distance;
        }
    }
}
