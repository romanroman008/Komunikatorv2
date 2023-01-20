using Caliburn.Micro;
using Komunikator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Komunikator.ViewModels
{
    internal class AddNewRecipientViewModel : Screen
    {
        private string _info=string.Empty;

        private string _ip;
        private string _login;


        public string Info
        {
            get { return _info; }
            set { _info = value;
                NotifyOfPropertyChange(() => Info);
            }
        }

        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value;}
        }

        public bool CanAddNewUserConversation(string ip,string login)
        {
            if (ip == string.Empty || login == string.Empty)
                return false;
         
            return true;
        }

        public void AddNewUserConversation(string  ip,string login)
        {
           
            if (!DataAccess.getInstance().AddNewUser(ip,login))
            {
                MessageBox.Show("Użytkownik o podanym IP już istnieje!");
            }
            clear();
            
            
        }

        private void clear()
        {
            Ip= string.Empty;
            Login= string.Empty;
            NotifyOfPropertyChange(() => Ip);
            NotifyOfPropertyChange(() => Login);    
        }

        public void AddNewIpAdress()
        {
            Info = "Hehe";
           // Connection.SendUdpPackage(User);
        }

        public async void GetInfo()
        {
            //Info = "Hahah";
           Connection.StartListening();
           
        }

      

       
    }
}
