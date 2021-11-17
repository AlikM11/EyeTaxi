using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Gmail { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public Card UserCard { get; set; }

        public User(string username, string password,string name,string surname,string gmail,string phone,string adress)

        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Phone = phone;
            Adress = adress;
            
        }




    }
}
