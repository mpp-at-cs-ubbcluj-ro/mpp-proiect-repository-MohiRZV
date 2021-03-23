using LabMPP.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    class MainPageService
    {
        private CustomerService customerService;
        private ArtistService artistService;
        private FestivalService festivalService;
        private TicketService ticketService;

        public MainPageService(CustomerService customerService, ArtistService artistService, FestivalService festivalService, TicketService ticketService)
        {
            this.customerService = customerService;
            this.artistService = artistService;
            this.festivalService = festivalService;
            this.ticketService = ticketService;
        }

        public Customer getCustomer(Account account)
        {
            return customerService.getCustomer(account.idUser);
        }
        public IEnumerable<Festival> getFestivals()
        {
            return festivalService.getAll();
        }

        public IEnumerable<Festival> getFestivalsByDate(DateTime date)
        {
            return festivalService.getByDate(date);
        }

        public long getSoldSeats(long festival_id)
        {
            return ticketService.getSoldSeats(festival_id);
        }

        public Ticket sellTicket(Festival festival, long seats, String client)
        {
            Random random=new Random();
            double x = random.NextDouble() % 25;
            return ticketService.addTicket(x* seats, client, festival.id, (int)seats);
        }

        public Festival addFestival(DateTime date, String location, String name, String genre, int seats, long aid)
        {
        return festivalService.addFestival(date, location, name, genre, seats, aid);
    }

    public Artist addArtist(String name, String genre)
    {
        return artistService.addArtist(name,genre);
    }
}
}
