using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain
{
    class Account:Entity<String>
    {
        public long idUser { get; set; }
        public string password { get; set; }

        public Account(string username, long idUser, string password):base(username)
        {
            this.idUser = idUser;
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
