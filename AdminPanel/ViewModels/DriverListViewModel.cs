using AdminPanel.Commads;
using AdminPanel.Commands;
using AdminPanel.Models;
using AdminPanel.Stores;
using AdminPanel.Views;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserPanel.Services;

namespace AdminPanel.ViewModels
{
    public class DriverListViewModel : BaseViewModel
    {
        public ObservableCollection<Driver> Drivers { get; set; }

        public Driver SelectedDriver { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand ShowCommand { get; set; }

        private DriverStore DriverStore;

        private readonly NavigationStore _navigation;

        public BaseViewModel SelectedViewModel { get; set; }

        public DriverStore driverStore = new DriverStore();

        public DriverListViewModel(NavigationStore navigation)
        {
            Drivers = JsonSaveService<ObservableCollection<Driver>>.Load("driver");
            if (Drivers == null)
                Drivers = new ObservableCollection<Driver>();
            //if json file is empty make fake drivers
            if (Drivers.Count == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Driver driver = FakeDriverService.GenerateDriver();
                    Drivers.Add(driver);
                }
                JsonSaveService<ObservableCollection<Driver>>.Save(Drivers, "driver");
            }
            driverStore.DriverEdited += EditDriver;
            driverStore.DriverAdded += AddDriver;
            DriverStore = new DriverStore();

            EditCommand = new UpdateViewCommand<DriverEditViewModel>(navigation, () => new DriverEditViewModel(navigation, driverStore) { Driver = SelectedDriver });
            ShowCommand = new UpdateViewCommand<DriverMapViewModel>(navigation, () => new DriverMapViewModel(navigation) { Driver = SelectedDriver, LastLocation = SelectedDriver.LastLocation });
            DeleteCommand = new RelayCommand(delete =>
            {
                Driver temp = SelectedDriver;
                if (MessageBox.Show("Do you want to delete this driver?", "Deleting Driver", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Drivers.Remove(temp);
                    JsonSaveService<ObservableCollection<Driver>>.Save(Drivers, "driver");
                }

            });
            _navigation = navigation;
            _navigation.SelectedViewModelChanged += OnSelectedViewChanged;
        }


        protected void OnSelectedViewChanged()
        {
            OnPropertChanged(nameof(SelectedViewModel));
        }

        private void EditDriver(Driver driver)
        {
            foreach (var item in Drivers)
            {
                if (item.Id == driver.Id)
                {
                    Drivers.Remove(item);
                    Drivers.Add(driver);
                    break;
                }
            }
            JsonSaveService<ObservableCollection<Driver>>.Save(Drivers, "driver");
        }

        private void AddDriver(Driver driver)
        {
            if (Drivers.Count > 0)
                driver.Id = Drivers.Last().Id + 1;
            else
                driver.Id = 0;
            Drivers.Add(driver);
            JsonSaveService<ObservableCollection<Driver>>.Save(Drivers, "driver");
        }
    }
}
