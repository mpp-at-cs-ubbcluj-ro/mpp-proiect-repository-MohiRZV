package ui;

import domain.Artist;
import domain.Customer;
import domain.Festival;
import domain.Ticket;
import org.junit.jupiter.api.Test;
import repository.*;

import java.io.FileReader;
import java.io.IOException;
import java.sql.Date;
import java.util.Properties;

public class Main {
    public static void testArtist(){
        Properties props=new Properties();
        try{
            props.load(new FileReader("bd.properties"));
        }catch (IOException ex){
            System.out.println("Cannot find bd.config"+ex);
        }
        ArtistRepo repo=new ArtistRepo(props);
        repo.getAll().forEach(x->repo.delete(x.getId()));
        repo.add(new Artist(0l,"Avicii","Dance"));
        System.out.println("Toti artistii din db:");
        for(Artist artist:repo.findArtistByGenre("Dance")){
            System.out.println(artist);
        }
    }
    public static void testCustomer(){
        Properties props=new Properties();
        try{
            props.load(new FileReader("bd.properties"));
        }catch (IOException ex){
            System.out.println("Cannot find bd.config"+ex);
        }
        CustomerRepo repo=new CustomerRepo(props);
        repo.getAll().forEach(x->repo.delete(x.getId()));
        repo.add(new Customer(0l,"Jon","Stada X"));
        System.out.println("Toti clientii din db:");
        repo.getAll().forEach(System.out::println);
    }
    public static void testFestival(){
        Properties props=new Properties();
        try{
            props.load(new FileReader("bd.properties"));
        }catch (IOException ex){
            System.out.println("Cannot find bd.config"+ex);
        }
        FestivalRepo repo=new FestivalRepo(props);
        repo.getAll().forEach(x->repo.delete(x.getId()));

        Artist artist=new ArtistRepo(props).getOne(1l);
        Festival festival=new Festival(0l,Date.valueOf("2021-03-13"),"Baia Mare","FestivalUL","rock",7l,artist);
        repo.add(festival);


//        repo.update(new Festival(0l,Date.valueOf("2021-03-13"),"Baia Mare","FestivalULUL","folk",3l,artist));
        System.out.println("Toate festivalurile din db:");
        repo.getAll().forEach(System.out::println);
    }
    public static void testTicket(){
        Properties props=new Properties();
        try{
            props.load(new FileReader("bd.properties"));
        }catch (IOException ex){
            System.out.println("Cannot find bd.config"+ex);
        }
        TicketRepo repo=new TicketRepo(props);
        repo.getAll().forEach(x->repo.delete(x.getId()));


        Artist artist=new ArtistRepo(props).getOne(1l);
        Customer customer=new CustomerRepo(props).getOne(1l);
        Festival festival=new Festival(0l,Date.valueOf("2021-03-13"),"Baia Mare","FestivalUL","rock",7l,artist);
        Ticket ticket=new Ticket(0l,festival,15.4,customer,11);
        repo.add(ticket);


//        repo.update(new Ticket(1l,festival,12.4,customer,11));
        System.out.println("Toate ticketele din db:");
        repo.getAll().forEach(System.out::println);
    }
    public static void main(String[] args) {
        testArtist();
        testCustomer();
        testFestival();
        testTicket();
    }

}
