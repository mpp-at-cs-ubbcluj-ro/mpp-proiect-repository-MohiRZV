using LabMPP.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    class TableDisplayDTO
    {
        private MainPageService mainPageService;
        private List<TableRowDTO> list = new List<TableRowDTO>();
        public TableDisplayDTO(MainPageService mainPageService)
        {
            this.mainPageService = mainPageService;
            init();
        }
        private void init()
        {
            foreach(Festival festival in mainPageService.getFestivals())
            {
                list.Add(new TableRowDTO(festival.artist.name, festival.date, festival.location, festival.seats, (int)mainPageService.getSoldSeats(festival.id)));
            }
        }
        public List<TableRowDTO> getData()
        {
            return list;
        }
    }
}
