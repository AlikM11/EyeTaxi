using AdminPanel.Stores;
using AdminPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminPanel.Commads
{
    class UpdateViewCommand<TVViewModel> : ICommand where TVViewModel : BaseViewModel
    {

        private NavigationStore _navigationStore;

        private readonly Func<TVViewModel> _createViewModel;

        public UpdateViewCommand(NavigationStore navigationStore, Func<TVViewModel> createViewModel)
        {
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
            _navigationStore.SelectedViewModel = _createViewModel();
        }
    }
}
