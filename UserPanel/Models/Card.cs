using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.Models
{
    public class Card
    {
        public string Pin { get; set; }

        public string Pan { get; set; }

        public DateTime LastTime { get; set; }

        public string Cvc { get; set; }
   
        public Card(string pin, string pan, DateTime lastTime, string cvc)
        {
            Pin = pin;
            Pan = pan;
            LastTime = lastTime;
            Cvc = cvc;
        }

    }
}
