using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Stores
{
     public  class DriverStore
    {
        public event Action<Driver> DriverAdded;

        public event Action<Driver> DriverEdited;

        public void Add(Driver driver)
        {
            DriverAdded?.Invoke(driver);
        }

        public void Edit(Driver driver)
        {
            DriverEdited?.Invoke(driver);
        }
    }
}
