package domain.validators;

import domain.Ticket;

import java.io.Serializable;

public class TicketValidator implements Validator<Ticket>, Serializable {
    @Override
    public void validate(Ticket entity) throws ValidationException {
        if(entity.getClient().isEmpty())
            throw new ValidationException("Numele clientului nu poate fi vid!");
        if(entity.getFestival()==null)
            throw new ValidationException("Festivalul pentru care se vinde biletul nu poate fi null!");
    }
}
