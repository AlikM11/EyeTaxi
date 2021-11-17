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
    public class BankCardViewModel:BaseViewModel
    {
        public ICommand Back { get; set; }

        public string Pan { get; set; }

        public string Pin { get; set; }

        public string Cvc { get; set; }

        public DateTime LastTime { get; set; }




        public BankCardViewModel(NavigationStore navigation)
        {
            Back = new UpdateViewCommand<RegisterViewModel>(navigation, () => new RegisterViewModel(navigation));
        }

    }
}
