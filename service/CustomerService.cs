using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    class CustomerService
    {
        private CustomerRepo customerRepo;
        private Validator<Customer> validator = new CustomerValidator();

        public CustomerService(CustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        public Customer getCustomer(long idUser)
        {
            return customerRepo.getOne(idUser);
        }
    }
}
