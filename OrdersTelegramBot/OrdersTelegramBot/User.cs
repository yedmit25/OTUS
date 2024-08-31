using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTelegramBot
{
    internal class User
    {
        public int id = 0;
        public string name = string.Empty;
        public User()
        {
            // Constructor Statements
        }
        public void GetUserDetails(int uid, string uname)
        {
            id = uid;
            uname = name;
            Console.WriteLine("Id: {0}, Name: {1}", id, name);
        }
        public int Designation { get; set; }
        public string Location { get; set; }
    }

}
