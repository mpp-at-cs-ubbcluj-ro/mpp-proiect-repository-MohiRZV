using System;

namespace LabMPP.domain
{
    [Serializable]
    public class TicketDTO
    {
        public int festivalID { get; set; }
        public long seats { get; set; }
        
        public string client { get; set; }

        public TicketDTO(int festivalId, long seats, string client)
        {
            festivalID = festivalId;
            this.seats = seats;
            this.client = client;
        }

        
    }
}