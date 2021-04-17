package service;

import domain.Employee;
import repository.EmployeeRepoInterface;

import java.io.Serializable;

public class EmployeeService implements Serializable {
    private EmployeeRepoInterface employeeRepo;

    public EmployeeService(EmployeeRepoInterface employeeRepo) {
        this.employeeRepo = employeeRepo;
    }

    public Employee getAgent(Long id){
        return employeeRepo.getOne(id);
    }
}
