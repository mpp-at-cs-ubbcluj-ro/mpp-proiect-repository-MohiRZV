package service;

import domain.Ticket;
import domain.validators.TicketValidator;
import domain.validators.ValidationException;
import domain.validators.Validator;
import repository.CustomerRepo;
import repository.FestivalRepo;
import repository.TicketRepo;

public class TicketService {
    private TicketRepo ticketRepo;
    private FestivalRepo festivalRepo;
    private CustomerRepo customerRepo;
    private Validator<Ticket> validator=new TicketValidator();
    public TicketService(TicketRepo ticketRepo, FestivalRepo festivalRepo, CustomerRepo customerRepo) {
        this.ticketRepo = ticketRepo;
        this.festivalRepo = festivalRepo;
        this.customerRepo = customerRepo;
    }

    public Ticket addTicket(Double price,String customer,Long festival_id,Integer seats) throws ValidationException {
        Ticket ticket=new Ticket(0l, festivalRepo.getOne(festival_id),price, customer,seats);
        validator.validate(ticket);
        return ticketRepo.add(ticket);
    }

    public Long getSoldSeats(Long festival_id) {
        return ticketRepo.getSoldSeats(festival_id);
    }
}
