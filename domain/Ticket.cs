using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2MPP.domain
{
    class Ticket:Entity<long>
    {
        public Ticket(long id,Festival festival, double price, Customer client, int seats):base(id)
        {
            this.festival = festival;
            this.price = price;
            this.client = client;
            this.seats = seats;
        }

        public Festival festival { get; set; }
        public double price { get; set; }
        public Customer client { get; set; }
        public int seats { get; set; }
    }
}
