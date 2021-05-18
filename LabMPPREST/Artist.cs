using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPPREST
{
    class Artist : Entity<long>
    {
        public Artist(long id, string name, string genre) : base(id)
        {

            this.name = name;
            this.genre = genre;
        }
        public Artist(): base(0L)
        {

        }
        public string name { get; set; }
        public string genre { get; set; }

        public override string ToString()
        {
            return String.Format("Artist: {0} genre: {1}", name, genre);
        }
    }
}
