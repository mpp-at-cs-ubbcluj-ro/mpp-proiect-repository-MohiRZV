package domain;

public class Artist extends Entity<Long>{

    private String name;
    private String genre;
    public Artist(Long id, String name, String genre) {
        super(id);
        this.name = name;
        this.genre = genre;
    }
}
