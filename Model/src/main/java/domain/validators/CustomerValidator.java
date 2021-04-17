package domain.validators;

import domain.Customer;

import java.io.Serializable;

public class CustomerValidator implements Validator<Customer>, Serializable {
    @Override
    public void validate(Customer entity) throws ValidationException {

    }
}
