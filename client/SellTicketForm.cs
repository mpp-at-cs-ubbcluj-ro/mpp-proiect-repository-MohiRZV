using LabMPP.domain;
using LabMPP.domain.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsApp1;

namespace LabMPP.ui
{
    partial class SellTicketForm : Form
    {
        private ClientController _controller;
        private FestivalDTO _festival;
        public SellTicketForm(ClientController controller, FestivalDTO festival)
        {
            InitializeComponent();
            _controller = controller;
            _festival = festival;
            init();
            
        }
        private void init()
        {
            this.textBoxFestival.Text = _festival.ToString();
            long leftTickets = _festival.AvailableSeats - _festival.SoldSeats;
            List<long> arrayList = new List<long>();
            for (long i = 1; i <= leftTickets && i <= 15; i++)
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
                _controller.sellTicket((int)_festival.Id, seats, client);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
