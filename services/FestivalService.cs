using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    public class FestivalService
    {
        private FestivalRepo festivalRepo;
        private ArtistRepo artistRepo;
        private Validator<Festival> validator = new FestivalValidator();

        public FestivalService(FestivalRepo festivalRepo, ArtistRepo artistRepo)
        {
            this.festivalRepo = festivalRepo;
            this.artistRepo = artistRepo;
        }

        public IEnumerable<Festival> getAll()
        {
            return festivalRepo.getAll();
        }
        public IEnumerable<Festival> getByDate(DateTime date)
        {
            return festivalRepo.FindByDate(date);
        }
        public Festival getOne(int id)
        {
            return festivalRepo.getOne(id);
        }
        public Festival addFestival(DateTime date,TimeSpan time, String location, String name, String genre, int seats, long artist_id)
        {
            Artist artist = artistRepo.getOne(artist_id);
            if (artist == null)
            {
                throw new ValidationException("Artistul cu id-ul introdus nu exista!");
            }
            Festival festival = new Festival(0L, date, time, location, name, genre, seats, artist);
            validator.validate(festival);
            return festivalRepo.add(festival);
        }
    }
}

