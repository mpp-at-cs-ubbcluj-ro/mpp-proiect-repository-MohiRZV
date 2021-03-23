using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain
{
    class TableRowDTO
    {

        string name;
        DateTime data;
        string location;
        int availableSeats;
        int soldSeats;

        public TableRowDTO(string name, DateTime data, string location, int availableSeats, int soldSeats)
        {
            this.name = name;
            this.data = data;
            this.location = location;
            this.availableSeats = availableSeats;
            this.soldSeats = soldSeats;
        }

        public string Name { get => name; set => name = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Location { get => location; set => location = value; }
        public int AvailableSeats { get => availableSeats; set => availableSeats = value; }
        public int SoldSeats { get => soldSeats; set => soldSeats = value; }
    }
}
