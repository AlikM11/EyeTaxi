using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserPanel.Commands;
using UserPanel.Models;
using UserPanel.Services;
using UserPanel.Stores;
using UserPanel.Views;

namespace UserPanel.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class LoginViewModel : BaseViewModel
    {

        public List<User> Users { get; set; } = new List<User>(0);

        public ICommand NavigateRegisterCommand { get; set; }

        public ICommand NavigateMapCommand { get; set; }

        public ICommand LoginCommand { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginViewModel(NavigationStore NV)
        {
            NavigateRegisterCommand = new UpdateViewCommand<RegisterViewModel>(NV, () => new RegisterViewModel(NV));
            NavigateMapCommand = new UpdateViewCommand<MapVIewModel>(NV, () => new MapVIewModel());
            LoginCommand = new RelayCommand(Login);
            Users = JsonSaveService<List<User>>.Load("Users");
            if (Users == null)
            Users = new List<User>(0);
        }


        public void Login(Object obj)
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                var usr = Users.Find(u => u.Username == Username);
                if (usr != null)
                {
                    if (usr.Password == Password)
                    {
                        MessageBox.Show("You have entered successfully");
                        NavigationStore navigation = new NavigationStore();
                        NavigateMapCommand = new UpdateViewCommand<MapVIewModel>(navigation, () => new MapVIewModel());
                    }
                    else
                        MessageBox.Show("Username/password is incorrect :(");
                }
                else
                {
                    MessageBox.Show("Username/password is incorrect :(");
                }
            }
            else
            {
                MessageBox.Show("Enter username & password");
            }
        }
    }
}
