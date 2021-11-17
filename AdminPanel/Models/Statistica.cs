using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Statistica
    {
        public float MonthlyIncome { get; set; }

        public float DailyIncome { get; set; }

        public float IncomeAfterLWD { get; set; } //last withdraw

        public string Interest { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

    }
}
