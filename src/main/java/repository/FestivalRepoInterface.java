package repository;

import domain.Artist;
import domain.Festival;

import java.util.Date;

public interface FestivalRepoInterface extends RepositoryInterface<Long, Festival> {
    Iterable<Artist> findByDate(Date date);
}
