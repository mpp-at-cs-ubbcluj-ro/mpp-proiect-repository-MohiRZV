package domain;

import java.time.LocalDateTime;
import java.util.List;

public class Festival extends Entity<Long>{
    private LocalDateTime date;
    private String location;
    private String name;
    private String genre;
    private Long seats;
    private List<Artist> artists;

    public Festival(Long id, LocalDateTime date, String location, String name, String genre,Long seats, List<Artist> artists) {
        super(id);
        this.date = date;
        this.location = location;
        this.name = name;
        this.genre = genre;
        this.seats=seats;
        this.artists = artists;
    }
}
