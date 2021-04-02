using LabMPP.domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.repository
{
    interface TicketRepoInterface:RepositoryInterface<long,Ticket>
    {
        long getSoldSeats(long festival_id);
    }
}
