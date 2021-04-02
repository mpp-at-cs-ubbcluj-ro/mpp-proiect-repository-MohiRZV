using LabMPP.domain;
using LabMPP.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    public class LoginService
    {
        private AccountRepo accountRepo;

        public LoginService(AccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }
        private bool checkPassword(Account account,string password)
        {
            if (!account.password.Equals(password))
                return false;
            return true;
        }

        public Account getAccount(String username, String password)
        {
            Account account = accountRepo.getOne(username);
            if(account==null || checkPassword(account, password) == false)
            {
                throw new BadCredentialsException("Nume sau parola gresite!");

            }
            return account;
        }
    }
}
