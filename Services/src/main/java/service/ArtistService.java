package service;

import domain.Artist;
import domain.validators.ArtistValidator;
import domain.validators.ValidationException;
import domain.validators.Validator;
import repository.ArtistRepo;
import repository.ArtistRepoHibernate;
import repository.ArtistRepoInterface;

public class ArtistService {
    private ArtistRepoInterface artistRepo;
    private Validator<Artist> validator=new ArtistValidator();
    public ArtistService(ArtistRepoInterface artistRepo) {
        this.artistRepo = artistRepo;
    }

    public Artist addArtist(String name,String genre) throws ValidationException {
        Artist artist=new Artist(0l,name,genre);
        validator.validate(artist);
        return artistRepo.add(artist);
    }
}
