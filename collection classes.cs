using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatio
{
    class User
    {
        public int id;
        public string FullName;
        public string username;
        public User(int id, string FullName, string username)
        {
            this.id = id;
            this.FullName = FullName;
            this.username = username;
        }
        public User() {
            this.id = 1;
        }
        public int getid() {
            return id;
        }
    }
    class message{
        public string created_at;
        public string from;
        public string text;
        public message(string a,string b,string c) {
            this.text = c;
            this.from = b;
            this.created_at = a;
        }

}

    
    class Group
    {
        public int id;
        public string groupname;
        public bool isadmin;

        public Group(int id, string groupname,bool isadmin){
            this.id = id;
            this.groupname = groupname;
            this.isadmin = isadmin;
            
        }

    }
}
