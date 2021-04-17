using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.domain
{
    public class Festival:Entity<long>
    {
        public Festival(long id,DateTime date, TimeSpan time, string location, string name, string genre,int seats,Artist artist):base(id)
        {
            this.time = time;
            this.date = date;
            this.location = location;
            this.name = name;
            this.genre = genre;
            this.seats = seats;
            this.artist = artist;
        }

        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string genre { get; set; }
        public Artist artist{ get; set; }

        public int seats { get; set; }

        public override string ToString()
        {
            return String.Format("Festival: {0} genre: {1} date: {2}", name, genre, date);
        }
    }
}
