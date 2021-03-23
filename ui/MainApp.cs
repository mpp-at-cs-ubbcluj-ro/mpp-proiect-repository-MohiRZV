using LabMPP.domain;
using LabMPP.repository;
using LabMPP.service;
using LabMPP.ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabMPP
{
    partial class MainApp : Form
    {

        private MainPageService mainPageService;
        Account account;
        public MainApp()
        {
            InitializeComponent();
            
        }
        public void setServices(Account account,MainPageService mainPageService) {
            this.account = account;
            this.mainPageService = mainPageService;
            initData();
        }
        private void initData()
        {
            this.dataGridArtist.DataSource = new TableDisplayDTO(mainPageService).getData();
            
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime date = this.dateTimePicker.Value;
            this.dataGridSArtist.DataSource = mainPageService.getFestivalsByDate(date);
            this.dataGridSArtist.Update();
        }

        private void dataGridArtist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSellTicket_Click(object sender, EventArgs e)
        {
            var sellTicketForm = new SellTicketForm();
            Festival festival = null;
            if(this.dataGridArtist.SelectedRows.Count>0)
                festival = this.dataGridArtist.SelectedRows[0].DataBoundItem as Festival;
            else
            {
                MessageBox.Show("Trebuie sa selectati un festival!");
                return;
            }
            if (festival == null)
            {
                MessageBox.Show("Trebuie sa selectati un festival");
                return;
            }
            sellTicketForm.setServices(mainPageService, festival);
            sellTicketForm.Show();
        }
    }
}
