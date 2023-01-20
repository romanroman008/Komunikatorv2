using Caliburn.Micro;
using Komunikator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Komunikator.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
       
        
        private List<UserModel> _items;
        public BindableCollection<UserModel> Users { get; set; }

        public UserModel SelectedUser { get; set; }


        public ShellViewModel()
        {
            Users =DataAccess.getInstance().GetUserConversations();
            ActivateItemAsync(new AddNewRecipientViewModel());
            Connection.StartListening();
            
        }

    
    
       // public bool CanOpenNewMessageWindow()
      //  {
       //     if(SelectedUser==null)
       //         return false;
      //      else
       //         return true;
       // }

        public void OpenNewMessageWindow()
        {
            if(SelectedUser!= null)
            {
                ActivateItemAsync(new SelectedConversationViewModel(SelectedUser));
            }
            
        }
       
        public void OpenNewRecipientWindow()
        {
            ActivateItemAsync(new AddNewRecipientViewModel());
        }
       
        
            
          

        public void DeleteUserConversation()
        {
            if (SelectedUser != null)
            {
                if (DataAccess.getInstance().DeleteUserConversation(SelectedUser.Ip))
                {
                    MessageBox.Show("Udało się usunąć konwersacje");
                }
                else
                    MessageBox.Show("Nie udało się usunąć konwersacji");
            }
            
        }

      
       


     
    }
}
