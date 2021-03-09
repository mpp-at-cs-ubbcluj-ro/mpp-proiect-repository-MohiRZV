package repository;

import domain.Artist;

import java.util.Date;

public class FestivalRepo implements FestivalRepoInterface {

    @Override
    public Iterable<Artist> findByDate(Date date) {
        return null;
    }
}
