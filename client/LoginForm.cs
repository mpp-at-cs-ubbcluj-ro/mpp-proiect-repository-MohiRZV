using LabMPP.domain;
using LabMPP.service;
using System;
using System.Windows.Forms;
using WinFormsApp1;

namespace LabMPP.ui
{
    partial class LoginForm : Form
    {
        private ClientController _controller;
        public LoginForm(ClientController controller)
        {
            InitializeComponent();
            this._controller = controller;
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
                account = _controller.login(username, password);
            }
            catch (BadCredentialsException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            var form = new MainApp(_controller);
            form.Show();
            this.Hide();
        }

        
    }
}
