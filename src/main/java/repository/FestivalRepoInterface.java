package repository;

import domain.Artist;
import domain.Festival;

import java.sql.Date;

public interface FestivalRepoInterface extends RepositoryInterface<Long, Festival> {
    Iterable<Festival> findByDate(Date date);
}
