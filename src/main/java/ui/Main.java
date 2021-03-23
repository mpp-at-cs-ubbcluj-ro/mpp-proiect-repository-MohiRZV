package ui;

import controller.Controller;
import domain.Artist;
import domain.Customer;
import domain.Festival;
import domain.Ticket;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import repository.*;

import java.io.FileReader;
import java.io.IOException;
import java.sql.Date;
import java.util.Properties;

import javafx.application.Application;
import service.*;

public class Main extends Application{
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
        String customer="Clientul";
        Festival festival=new Festival(0l,Date.valueOf("2021-03-13"),"Baia Mare","FestivalUL","rock",7l,artist);
        Ticket ticket=new Ticket(0l,festival,15.4,customer,11);
        repo.add(ticket);


//        repo.update(new Ticket(1l,festival,12.4,customer,11));
        System.out.println("Toate ticketele din db:");
        repo.getAll().forEach(System.out::println);
    }

    public static void main(String[] args) {
//        testArtist();
//        testCustomer();
//        testFestival();
//        testTicket();

        launch(args);
    }

    Properties props=new Properties();
    private AccountRepo accountRepo=new AccountRepo(props);
    private CustomerRepo customerRepo=new CustomerRepo(props);
    private ArtistRepo artistRepo=new ArtistRepo(props);
    private FestivalRepo festivalRepo=new FestivalRepo(props);
    private TicketRepo ticketRepo=new TicketRepo(props);
    private EmployeeRepo employeeRepo=new EmployeeRepo(props);

    private LoginService loginService=new LoginService(accountRepo);
    private ArtistService artistService=new ArtistService(artistRepo);
    private CustomerService customerService=new CustomerService(customerRepo);
    private FestivalService festivalService=new FestivalService(festivalRepo, artistRepo);
    private TicketService ticketService=new TicketService(ticketRepo, festivalRepo, customerRepo);
    private EmployeeService employeeService=new EmployeeService(employeeRepo);
    @Override
    public void start(Stage primaryStage) throws Exception {
        try{
            props.load(new FileReader("bd.properties"));
        }catch (IOException ex){
            System.out.println("Cannot find bd.config"+ex);
        }
        initView(primaryStage);
        primaryStage.setWidth(800);
        primaryStage.show();

    }


    private void initView(Stage primaryStage) throws IOException {
        FXMLLoader loginViewLoader = new FXMLLoader();
        loginViewLoader.setLocation(getClass().getResource("/views/loginView.fxml"));
        AnchorPane anchorPane = loginViewLoader.load();
        primaryStage.setScene(new Scene(anchorPane));

        Controller controller= loginViewLoader.getController();
        controller.setService(loginService,artistService,customerService,festivalService,ticketService,employeeService);
    }
}
