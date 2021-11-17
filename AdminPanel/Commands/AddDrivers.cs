using AdminPanel.Models;
using AdminPanel.Stores;
using AdminPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminPanel.Commands
{
    public class AddDrivers<TVViewModel> : ICommand where TVViewModel : BaseViewModel
    {

        private readonly DriverListViewModel _driverListViewModel;
        private readonly NavigationStore _navigationStore;
        private Driver _driver;

        public AddDrivers(DriverListViewModel driverListViewModel, NavigationStore navigationStore, Driver driver)
        {
            _driverListViewModel = driverListViewModel;
            _navigationStore = navigationStore;
            _driver = driver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _driverListViewModel.driverStore.Add(_driver);
            _navigationStore.SelectedViewModel = _driverListViewModel;
        }
    }
   
}
