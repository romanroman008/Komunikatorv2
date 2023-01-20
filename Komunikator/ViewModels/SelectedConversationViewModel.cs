using Caliburn.Micro;
using Komunikator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Komunikator.ViewModels
{
    class SelectedConversationViewModel : Screen
    {
        private UserModel _user;
        public BindableCollection<MessageModel> Messages { get; set; }

        public string MessageContent { get; set; }

        public SelectedConversationViewModel(UserModel user)
        {
            _user = user;
            if(user.Messages!=null)
            Messages=new BindableCollection<MessageModel>(user.Messages);
            else
            Messages=new BindableCollection<MessageModel>();
            //Messages = new BindableCollection<MessageModel>(DataAccess.getInstance().GetUserMessages(user));
        }

        public bool CanSendNewMessage(string messageContent)
        {
            if (messageContent.Trim()=="")
                return false;
            return true;
        }

        public void SendNewMessage(string messageContent)
        {
            
            if(Connection.SendUdpPackage(_user, MessageContent))
            {
                DataAccess.getInstance().AddNewMessage(_user.Ip, MessageContent, "ja");
                Messages.Add(new MessageModel(MessageContent));
            }
            else
            {
                MessageBox.Show("Nieprawidłowy adres IP");
            }
            
            clear();

        }

        private void clear()
        {
            MessageContent = "";
            NotifyOfPropertyChange(() => MessageContent);
        }


    }
}
