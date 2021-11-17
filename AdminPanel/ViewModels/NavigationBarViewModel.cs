using AdminPanel.Commads;
using AdminPanel.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminPanel.ViewModels
{
    class NavigationBarViewModel: BaseViewModel
    {

        public ICommand NavigateDriverListCommand { get; set; }

        public ICommand NavigateAddDriverCommand { get; set; }

        public ICommand NavigateDriverPricingCommand { get; set; }

        private readonly NavigationStore _navigation;

        public BaseViewModel SelectedViewModel => _navigation.SelectedViewModel;

        public NavigationBarViewModel(NavigationStore navigation)
        {
            _navigation = navigation;
            _navigation.SelectedViewModelChanged += OnSelectedViewChanged;
            NavigateAddDriverCommand = new UpdateViewCommand<DriverEditViewModel>(navigation, () => new DriverEditViewModel());
            NavigateDriverListCommand = new UpdateViewCommand<DriverListViewModel>(navigation, () => new DriverListViewModel());
            NavigateDriverPricingCommand = new UpdateViewCommand<PricingViewModel>(navigation, () => new PricingViewModel());
        }

        protected void OnSelectedViewChanged()
        {
            OnPropertChanged(nameof(SelectedViewModel));
        }
    }
}
