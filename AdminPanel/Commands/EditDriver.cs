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
    class EditDriver<TVViewModel> : ICommand where TVViewModel : BaseViewModel
    {
        private readonly DriverEditViewModel _driverEditViewModel;
        private readonly DriverStore _driverStore;
        private readonly NavigationStore _navigationStore;

        private readonly Func<TVViewModel> _createViewModel;


        public EditDriver(DriverEditViewModel driverEditViewModel, DriverStore driverStore, NavigationStore navigationStore, Func<TVViewModel> createViewModel)
        {
            _driverEditViewModel = driverEditViewModel;
            _driverStore = driverStore;
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _driverStore.Edit(_driverEditViewModel.Driver);
            _navigationStore.SelectedViewModel = _createViewModel();
        }
    }
}
