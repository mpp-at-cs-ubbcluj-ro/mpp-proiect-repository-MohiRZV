package domain.validators;

import domain.Artist;

import java.io.Serializable;

public class ArtistValidator implements Validator<Artist> , Serializable {
    @Override
    public void validate(Artist entity) throws ValidationException {
        if(entity.getName().isEmpty() || entity.getGenre().isEmpty())
            throw new ValidationException("Numele si genreul nu pot fi vide!");
    }
}
