using LabMPP.domain;
using LabMPP.domain.Validators;
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
    partial class SellTicketForm : Form
    {
        public SellTicketForm()
        {
            InitializeComponent();
        }
        private MainPageService mainPageService;
        private Festival festival;
        public void setServices(MainPageService mainPageService,Festival festival)
        {
            this.mainPageService = mainPageService;
            this.festival = festival;
            init();
        }

        private void init()
        {
            this.textBoxFestival.Text = festival.ToString();
            long leftTickets = festival.seats - mainPageService.getSoldSeats(festival.id);
            List<long> arrayList = new List<long>();
            for (long i = 1; i <= leftTickets; i++)
                arrayList.Add(i);
            this.comboBoxNumarBilete.DataSource = arrayList;
        }
        private void btnSellTicket_Click(object sender, EventArgs e)
        {
            long seats = long.Parse(this.comboBoxNumarBilete.SelectedItem.ToString());
            String client = this.textBoxNumeCumparator.Text;
            if (client=="")
            {
                MessageBox.Show("Numele nu poate fi vid");
                return;
            }
            try
            {
                mainPageService.sellTicket(festival, seats, client);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
