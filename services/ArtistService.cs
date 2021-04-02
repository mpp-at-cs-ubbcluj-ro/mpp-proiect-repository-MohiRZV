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
    public class ArtistService
    {
        private ArtistRepo artistRepo;
        private Validator<Artist> validator = new ArtistValidator();

        public ArtistService(ArtistRepo artistRepo)
        {
            this.artistRepo = artistRepo;
        }

        public Artist addArtist(String name,String genre)
        {
            Artist artist = new Artist(0L, name, genre);
            validator.validate(artist);
            return artistRepo.add(artist);
        }
    }
}
