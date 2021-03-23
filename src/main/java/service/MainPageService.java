package service;

import domain.*;
import domain.validators.ValidationException;

import java.sql.Date;
import java.util.Random;

public class MainPageService {
    private CustomerService customerService;
    private ArtistService artistService;
    private FestivalService festivalService;
    private TicketService ticketService;
    public EmployeeService employeeService;

    public MainPageService(CustomerService customerService, ArtistService artistService, FestivalService festivalService, TicketService ticketService, EmployeeService employeeService) {
        this.customerService = customerService;
        this.artistService = artistService;
        this.festivalService = festivalService;
        this.ticketService = ticketService;
        this.employeeService=employeeService;
    }

    public Employee getAgent(Account account){
        return employeeService.getAgent(account.getIdUser());
    }
    public Iterable<Festival> getFestivals(){
        return festivalService.getAll();
    }

    public Iterable<Festival> getFestivalsByDate(Date date){
        return festivalService.getByDate(date);
    }

    public Long getSoldSeats(Long festival_id){
        return ticketService.getSoldSeats(festival_id);
    }

    public Ticket sellTicket(Festival festival,Long seats,String client) throws ValidationException {
        Random random=new Random();
        double x=random.nextDouble()%25;
        return ticketService.addTicket(x*seats,client, festival.getId(), seats.intValue());
    }

    public Festival addFestival(Date date,String location,String name,String genre,Long seats,Long aid) throws ValidationException {
        return festivalService.addFestival(date,location,name,genre,seats,aid);
    }

    public Artist addArtist(String name,String genre) throws ValidationException {
        return artistService.addArtist(name,genre);
    }
}
