using AdminPanel.Commads;
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels
{
    public class DriverEditViewModel: BaseViewModel
    {
        public RelayCommand SaveCommand { get; set; }


        public Driver Driver { get; set; }

        public DriverEditViewModel()
        {
            SaveCommand = new RelayCommand(save =>
            {
            });
        }
    }
}
