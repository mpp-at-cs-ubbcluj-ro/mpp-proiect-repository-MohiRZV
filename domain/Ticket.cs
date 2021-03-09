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

        private Festival festival { get; set; }
        private double price { get; set; }
        private Customer client { get; set; }
        private int seats { get; set; }
    }
}
