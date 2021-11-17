using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserPanel.Commands;
using UserPanel.Stores;

namespace UserPanel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public BaseViewModel SelectedViewModel => _navigationStore.SelectedViewModel;

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.SelectedViewModelChanged += OnSelectedViewModelChanged;
        }

        protected void OnSelectedViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}
