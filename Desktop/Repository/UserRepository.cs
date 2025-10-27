using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Repository
{
    internal class UserRepository
    {
        private static List<User> registeredUsers = new List<User>();

      
        public bool RegisterUser(string username, string password)
        {
           
            if (registeredUsers.Any(u => u.Username == username))
            {
                return false; 
            }

          
            User newUser = new User(username, password); 
            registeredUsers.Add(newUser);
            return true; 
        }

        
        public bool AuthorizeUser(string username, string password)
        {
           
            User user = registeredUsers.FirstOrDefault(u => u.Username == username);

           
            if (user != null && user.Password == password) 
            {
                return true; 
            }

            return false; 
        }

        
        public List<User> GetAllUsers()
        {
            return registeredUsers;
        }
    }
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

