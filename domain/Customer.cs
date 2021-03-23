using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.domain
{
    class Customer:Entity<long>
    {
        public Customer(long id, string name, string address):base(id)
        {
            this.name = name;
            this.address = address;
        }

        public string name { get; set; }
        public string address { get; set; }

        public override string ToString()
        {
            return String.Format("Customer: {0} living at: {1}", name, address);
        }
    }
}
