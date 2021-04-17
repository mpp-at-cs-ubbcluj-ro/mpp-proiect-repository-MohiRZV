using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabMPP.domain;

namespace services
{
    public interface IObserver
    {
        void ticketsSold(TicketDTO ticket);
    }
}
