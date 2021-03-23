using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.domain
{
    class Ticket:Entity<long>
    {
        public Ticket(long id,Festival festival, double price, string client, int seats):base(id)
        {
            this.festival = festival;
            this.price = price;
            this.client = client;
            this.seats = seats;
        }

        public Festival festival { get; set; }
        public double price { get; set; }
        public string client { get; set; }
        public int seats { get; set; }

        public override string ToString()
        {
            return String.Format("Ticket for {0}; seats: {1}", festival.name, seats);
        }
    }
}
