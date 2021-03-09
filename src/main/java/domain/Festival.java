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
    //TODO one artist/show
    public LocalDateTime getDate() {
        return date;
    }

    public void setDate(LocalDateTime date) {
        this.date = date;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getGenre() {
        return genre;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public Long getSeats() {
        return seats;
    }

    public void setSeats(Long seats) {
        this.seats = seats;
    }

    public List<Artist> getArtists() {
        return artists;
    }

    public void setArtists(List<Artist> artists) {
        this.artists = artists;
    }

    public Festival(Long id, LocalDateTime date, String location, String name, String genre, Long seats, List<Artist> artists) {
        super(id);
        this.date = date;
        this.location = location;
        this.name = name;
        this.genre = genre;
        this.seats=seats;
        this.artists = artists;
    }
}
