using AdminPanel.Commads;
using AdminPanel.Commands;
using AdminPanel.Models;
using AdminPanel.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminPanel.ViewModels
{
    public class DriverEditViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        private Driver driver;
        public Driver Driver
        {
            get => driver;
            set
            {
                driver = value;
                OnPropertChanged("Driver");
            }
        }
        public DriverEditViewModel(NavigationStore navigation, DriverStore driverStore)
        {

            SaveCommand = new EditDriver<DriverListViewModel>(this, driverStore, navigation, () => new DriverListViewModel(navigation));
            CancelCommand = new UpdateViewCommand<DriverListViewModel>(navigation, () => new DriverListViewModel(navigation));
        }

        public DriverEditViewModel(NavigationStore navigation)
        {
            Driver = new Driver();
           // Driver.LastLocation = RandomLocationService.RandomLocation();
           // SaveCommand = new AddDriverCommand<DriverListViewModel>(new DriverListViewModel(navigation), Driver, navigation);
           // CancelCommand = new UpdateViewCommand<DriverListViewModel>(navigation, () => new DriverListViewModel(navigation));
        }


    }
}
