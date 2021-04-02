using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain
{
    [Serializable]
    public class Account:Entity<String>
    {
        public string name { get; set; }
        public string password { get; set; }

        public Account(string username, string password, string name):base(username)
        {
            this.name = name;
            this.password = password;
        }

        public string getUsername()
        {
            return base.id;
        }

        public override string ToString()
        {
            return getUsername();
        }
    }
}
