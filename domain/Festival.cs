using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2MPP.domain
{
    class Festival:Entity<long>
    {
        public Festival(long id,DateTime date, string location, string name, string genre, List<Artist> artists):base(id)
        {
            this.date = date;
            this.location = location;
            this.name = name;
            this.genre = genre;
            this.artists = artists;
        }

        private DateTime date { get; set; }
        private string location { get; set; }
        private string name { get; set; }
        private string genre { get; set; }
        private List<Artist> artists { get; }
    }
}
