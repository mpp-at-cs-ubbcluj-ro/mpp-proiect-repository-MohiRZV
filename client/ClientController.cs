using System;
using System.Collections.Generic;
using client;
using LabMPP.domain;
using services;

namespace WinFormsApp1
{
    public class ClientController : IObserver
    {
        public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly IServices server;
        private Account currentUser;

        public ClientController(IServices server)
        {
            this.server = server;
            currentUser = null;
        }

        protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
        
        public Account login(String userId, String pass)
        {
            Account user=new Account(userId,pass,"");
            currentUser=server.login(user,this);
            Console.WriteLine("Login succeeded ....");
            Console.WriteLine("Current user {0}", user);
            return currentUser;
        }

        public IEnumerable<FestivalDTO> searchByDate(DateTime date)
        {
            return server.searchByDate(date);
        }

        public IEnumerable<FestivalDTO> getAll()
        {
            return server.getAll();
        }

        public void sellTicket(int festivalID, long seats, String client)
        {
            server.sellTicket(festivalID, seats, client);
        }
        
        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser, this);
            currentUser = null;
        }

        public void ticketsSold(TicketDTO ticket)
        {
            UserEventArgs userArgs=new UserEventArgs(UserEvent.TicketSold,ticket);
            Console.WriteLine("Ticket sold");
            OnUserEvent(userArgs);
        }
    }
}
