using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.domain
{
    class Artist:Entity<long>
    {
        public Artist(long id, string name, string genre) : base(id)
        {
            
            this.name = name;
            this.genre = genre;
        }

        public string name { get; set; }
        public string genre { get; set; }

        public override string ToString()
        {
            return String.Format("Artist: {0} genre: {1}", name, genre);
        }
    }
}
