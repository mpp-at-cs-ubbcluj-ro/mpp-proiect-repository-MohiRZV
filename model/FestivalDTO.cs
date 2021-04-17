using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain
{
    [Serializable]
    public class FestivalDTO
    {

        string name;
        DateTime data;
        TimeSpan time;
        string location;
        int availableSeats;
        int soldSeats;
        int id;

        public FestivalDTO(string name, DateTime data, TimeSpan time, string location, int availableSeats, int soldSeats,int id)
        {
            this.name = name;
            this.time = time;
            this.data = data;
            this.location = location;
            this.availableSeats = availableSeats;
            this.soldSeats = soldSeats;
            this.id = id;
        }

        public string Name { get => name; set => name = value; }
        public DateTime Data { get => data; set => data = value; }
        public TimeSpan Time { get => time; set => time = value; }
        public string Location { get => location; set => location = value; }
        public int AvailableSeats { get => availableSeats; set => availableSeats = value; }
        public int SoldSeats { get => soldSeats; set => soldSeats = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return "Artist: " + name + " Location: " + location + " total seats: " + availableSeats;
        }
    }
}
