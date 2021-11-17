using AdminPanel.Commads;
using AdminPanel.Stores;
using AdminPanel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdminPanel.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private readonly NavigationStore _navigation;
        public BaseViewModel SelectedViewModel { get; set; }

        public NavigationBarViewModel NavigationBarViewModel { get; set; }

        public MainViewModel (NavigationStore navigation)
        {
            _navigation = navigation;
            _navigation.SelectedViewModelChanged += OnSelectedViewChanged;
            SelectedViewModel = _navigation.SelectedViewModel;
            NavigationBarViewModel = new NavigationBarViewModel(navigation);
            NavigationBarViewModel.PropertyChanged += NavigationBar_PropertyChanged;
        }

        private void NavigationBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SelectedViewModel = NavigationBarViewModel.SelectedViewModel;
        }

        protected void OnSelectedViewChanged()
        {
            OnPropertChanged(nameof(SelectedViewModel));
        }
    }
}
