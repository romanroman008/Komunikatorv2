using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikator.Model
{
    class UserModel
    {
        public int UserModelId { get; set; }
        public string Ip { get; set; }
        public string Login { get; set; }

        public bool Online { get; set; }

        public List<MessageModel> Messages { get; set; }

        public UserModel(string ip, string login) {
            Ip = ip;
            Login = login;
          

        }

       public void AddNewMessage(MessageModel message)
        {
            if (Messages == null)
            {
                Messages = new List<MessageModel>();
               
            }
            Messages.Add(message);
        }

      
    }
}
