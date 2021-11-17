using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Price
    {

        public float PricePerKm { get; set; }

        public Price()
        {
            PricePerKm = 0.7f;
        }
    }
}
