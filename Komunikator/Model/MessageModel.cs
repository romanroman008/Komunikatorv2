using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikator.Model
{
    class MessageModel
    {
        public int MessageModelId { get; set; }
        public string Content { get; set; }

        public DateTime DateSend {get;}

        public DateTime DateRecived { set; get; }

        public string Owner { get; set; } 

        public MessageModel(string content)
        {
            this.Content = content;
            this.DateSend = DateTime.Now;
        }

        

    }
}
