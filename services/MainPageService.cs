using LabMPP.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    public class MainPageService
    {
        private ArtistService artistService;
        private FestivalService festivalService;
        private TicketService ticketService;

        public MainPageService(ArtistService artistService, FestivalService festivalService, TicketService ticketService)
        {
            this.artistService = artistService;
            this.festivalService = festivalService;
            this.ticketService = ticketService;
        }

        public IEnumerable<Festival> getFestivals()
        {
            return festivalService.getAll();
        }

        public IEnumerable<Festival> getFestivalsByDate(DateTime date)
        {
            if (date == null)
                return getFestivals();
            return festivalService.getByDate(date);
        }

        public IEnumerable<FestivalDTO> GetFestivalDTOs(DateTime date)
        {
            List<FestivalDTO> list = new List<FestivalDTO>();
            foreach (Festival festival in getFestivalsByDate(date))
            {
                list.Add(new FestivalDTO(festival.artist.name, festival.date, festival.time, festival.location, festival.seats, (int)getSoldSeats(festival.id), (int)festival.id));
            }
            return list;
        }
        public IEnumerable<FestivalDTO> GetFestivalDTOs()
        {
            List<FestivalDTO> list = new List<FestivalDTO>();
            foreach (Festival festival in getFestivals())
            {
                list.Add(new FestivalDTO(festival.artist.name, festival.date, festival.time, festival.location, festival.seats, (int)getSoldSeats(festival.id), (int)festival.id));
            }
            return list;
        }
        public long getSoldSeats(long festival_id)
        {
            return ticketService.getSoldSeats(festival_id);
        }

        public Ticket sellTicket(int festivalID, long seats, String client)
        {
            Random random=new Random();
            double x = random.NextDouble() % 25;
            return ticketService.addTicket(x* seats, client, festivalID, (int)seats);
        }

        public Festival addFestival(DateTime date, TimeSpan time, String location, String name, String genre, int seats, long aid)
        {
        return festivalService.addFestival(date, time, location, name, genre, seats, aid);
        }

        public Artist addArtist(String name, String genre)
        {
            return artistService.addArtist(name,genre);
        }

        public Festival getFestival(int id) 
        {
            return festivalService.getOne(id);
        }
     

}
}
