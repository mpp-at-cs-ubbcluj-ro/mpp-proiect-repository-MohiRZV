using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2MPP.domain
{
    class Customer:Entity<long>
    {
        public Customer(long id, string name, string address):base(id)
        {
            this.name = name;
            this.address = address;
        }

        private string name { get; set; }
        private string address { get; set; }
    }
}
