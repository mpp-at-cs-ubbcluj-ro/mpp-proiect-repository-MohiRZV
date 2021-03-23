using LabMPP.domain;
using LabMPP.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabMPP.ui
{
    partial class LoginForm : Form
    {
        LoginService loginService;
        ArtistService artistService;
        CustomerService customerService;
        FestivalService festivalService;
        TicketService ticketService;
        public LoginForm()
        {
            InitializeComponent();
        }

        public void setService(LoginService loginService, ArtistService artistService, CustomerService customerService, FestivalService festivalService, TicketService ticketService)
        {
            this.loginService = loginService;
            this.artistService = artistService;
            this.customerService = customerService;
            this.festivalService = festivalService;
            this.ticketService = ticketService;
        }
        private void Login_Click(object sender, EventArgs e)
        {
            string username = this.textBoxUsername.Text;
            string password = this.textBoxPassword.Text;
            if (username=="" || password=="")
            {
                MessageBox.Show("Username sau parola gresita!");
                return;
            }
            Account account = null;
            try
            {
                account = loginService.getAccount(username, password);
            }
            catch (BadCredentialsException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            var form = new MainApp();
            var mps = new MainPageService(customerService, artistService, festivalService, ticketService);
            form.setServices(account,mps);
            form.Show();
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            var form = new ManagementForm();
            var mps = new MainPageService(customerService, artistService, festivalService, ticketService);
            form.setServices(mps);
            form.Show();
        }
    }
}
