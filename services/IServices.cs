using System;
using System.Collections.Generic;
using LabMPP.domain;

namespace services
{
    public interface IServices

    {
        Account login(Account user, IObserver client);
        IEnumerable<FestivalDTO> searchByDate(DateTime date);
        IEnumerable<FestivalDTO> getAll();
        void sellTicket(int festivalID, long seats, String client);
        void logout(Account user, IObserver client);

    }
}
