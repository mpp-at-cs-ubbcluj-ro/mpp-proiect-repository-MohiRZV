using System;
using System.Windows.Forms;
using LabMPP.ui;
using services;
using WinFormsApp1;


namespace client
{
    static class StartClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
                   
            IServices server = new ServerProxy("127.0.0.1", 55555);
            ClientController ctrl=new ClientController(server);
            
            LoginForm form = new LoginForm(ctrl);
            
            Application.Run(form);
        }
    }
}
