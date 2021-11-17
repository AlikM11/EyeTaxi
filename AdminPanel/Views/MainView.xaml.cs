using AdminPanel.Stores;
using AdminPanel.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminPanel.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    /// 
    [AddINotifyPropertyChangedInterface]
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            NavigationStore navigation = new NavigationStore();
            navigation.SelectedViewModel = new PricingViewModel();
            DataContext = new MainViewModel(navigation);
        }
    }
}
