package domain;

import javax.persistence.Column;
import javax.persistence.Table;

//@javax.persistence.Entity
//@Table(name="artist")
public class Artist extends Entity<Long>{

    private String name;
    private String genre;
    private Long id;

    public Artist(){
        super(null);
    }

    public Artist(Long id, String name, String genre) {
        super(id);
        this.id=id;
        this.name = name;
        this.genre = genre;
    }

//    @Column(name="name")
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
//    @Column(name="genre")
    public String getGenre() {
        return genre;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        super.setId(id);
        this.id = id;
    }
    @Override
    public String toString() {
        return "Artist{" +
                "name='" + name + '\'' +
                ", genre='" + genre + '\'' +
                '}';
    }
}
