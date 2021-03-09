package ui;

import domain.Artist;
import org.junit.jupiter.api.Test;
import repository.ArtistRepo;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class Main {
    public static void main(String[] args) {
        Properties props=new Properties();
        try{
            props.load(new FileReader("bd.properties"));
        }catch (IOException ex){
            System.out.println("Cannot find bd.config"+ex);
        }
        ArtistRepo repo=new ArtistRepo(props);
        repo.add(new Artist(0l,"Avicii","Dance"));

        repo.update(new Artist(0l,"David Guetta","Dance"));
        System.out.println("Toti artistii din db:");
        for(Artist artist:repo.findArtistByGenre("Dance")){
            System.out.println(artist);
        }

    }

}
