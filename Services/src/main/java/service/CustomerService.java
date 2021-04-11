package service;

import domain.Customer;
import domain.validators.CustomerValidator;
import domain.validators.Validator;
import repository.CustomerRepo;

import java.io.Serializable;

public class CustomerService implements Serializable {
    private CustomerRepo customerRepo;
    private Validator<Customer> validator=new CustomerValidator();
    public CustomerService(CustomerRepo customerRepo) {
        this.customerRepo = customerRepo;
    }

    public Customer getCustomer(Long idUser) {
        return customerRepo.getOne(idUser);
    }
}
