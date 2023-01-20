using Caliburn.Micro;
using Komunikator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikator
{
    class DataAccess
    {

        private static DataAccess _instance;

        

        

        public BindableCollection<UserModel> Users { get; set; }

       

       

        private ApplicationDbContext _dbContext;

        private DataAccess() 
        {
            _dbContext = new ApplicationDbContext();
            loadUserConversations();
            
            
        }

        ~DataAccess() { 
            _dbContext.Dispose();
        }

        public static DataAccess getInstance()
        {
            if (_instance == null)
                _instance = new DataAccess();
            return _instance;
        }
        
        public void loadUserConversations()
        {
            if (_dbContext.Users.ToList().Count > 0)
                Users = new BindableCollection<UserModel>(_dbContext.Users.Include(u => u.Messages).ToList());
            else
                Users = new BindableCollection<UserModel>();
       
        }

  
        
      

       public BindableCollection<UserModel> GetUserConversations()
       {
            return Users;
       }


        public List<string> GetAllIps()
        {
            List<string> ipList=new List<string>();
            Users.ToList().ForEach(u => { ipList.Add(u.Ip); });
            return ipList;

        }

        public void SetUserOnline(string ip)
        {
            if(checkIfUserExist(ip)){
                UserModel toChange = Users.SingleOrDefault(u => u.Ip == ip);
                Users.Remove(toChange);
                toChange.Online = true;
                Users.Add(toChange);
            }
           
        }

        public void SetUserOffline(string ip)
        {
            if (checkIfUserExist(ip))
            {
                UserModel toChange = Users.SingleOrDefault(u => u.Ip == ip);
                Users.Remove(toChange);
                toChange.Online = false;
                Users.Add(toChange);
            }

        }


        public List<MessageModel> GetUserMessages(UserModel user)
        {
            return Users.SingleOrDefault(user).Messages;
        } 

       

        public bool AddNewMessage(string ip,string message,string owner)
        {
            if (checkIfUserExist(ip))
            {
                UserModel dbUser = _dbContext.Users.Include(u => u.Messages).FirstOrDefault(u => u.Ip == ip);
                MessageModel messageModel = new MessageModel(message);
                messageModel.Owner=owner;
                Users.Remove(dbUser);
                dbUser.Messages.Add(messageModel);
                Users.Add(dbUser);

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
               
            
        }

        public string getUserLogin(string ip)
        {
            if (checkIfUserExist(ip))
            {
                return Users.FirstOrDefault(u => u.Ip == ip).Login;
            }
            return string.Empty;
        }

        public bool AddNewUser(string ip,string login = "Nieznany")
        {
            if(!checkIfUserExist(ip.Trim()))
            {
                UserModel user = new UserModel(ip.Trim(), login.Trim());
                Users.Add(user);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            else         
                return false;
            
            
        }

       

      

        public bool DeleteUserConversation(string ip)
        {
            if (checkIfUserExist(ip))
            {
                UserModel toRemove = _dbContext.Users.Include(u => u.Messages).ToList().Find(u => u.Ip == ip);
                _dbContext.Users.Remove(toRemove);
                Users.Remove(toRemove);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        private bool checkIfUserExist(string ip)
        {
            try
            {
                if (Users.ToList().Find(u => u.Ip == ip) == null)
                    return false;
                else
                    return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
