using LabMPP.domain;
using LabMPP.service;
using LabMPP.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using client;
using WinFormsApp1;

namespace LabMPP
{
    partial class MainApp : Form
    {

        private ClientController _controller;
        
        private Dictionary<int, FestivalDTO> allData = new Dictionary<int, FestivalDTO>();
        public MainApp(ClientController controller)
        {
            InitializeComponent();
            this._controller = controller;
            initData();
            controller.updateEvent += userUpdate;
        }
        private void initData()
        {
            foreach (var festivalDto in _controller.getAll())
            {
                allData.Add(festivalDto.Id,festivalDto);
            }
            this.dataGridArtist.DataSource = allData.Values.ToArray();
            
        }
        
        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime date = this.dateTimePicker.Value;
            this.dataGridSArtist.DataSource = _controller.searchByDate(date);
            dataGridSArtist.Columns["availableSeats"].Visible = false;
            dataGridSArtist.Columns["id"].Visible = false;
            dataGridSArtist.Columns["soldSeats"].Visible = false;
            if(!dataGridSArtist.Columns.Contains("remainingSeats"))
                dataGridSArtist.Columns.Add("remainingSeats","Remaining seats");
            foreach (DataGridViewRow row in dataGridSArtist.Rows)
            {
                row.Cells["remainingSeats"].Value=(int)row.Cells["availableSeats"].Value - (int)row.Cells["soldSeats"].Value;
            }
            this.dataGridSArtist.Update();
        }

        private void dataGridArtist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSellTicket_Click(object sender, EventArgs e)
        {
            
            FestivalDTO festival = null;
            try
            {
                festival = (FestivalDTO)this.dataGridArtist.SelectedRows[0].DataBoundItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Trebuie sa selectati tot randul!");
                return;
            }
            
            if (festival == null)
            {
                MessageBox.Show("Trebuie sa selectati un festival");
                return;
            }

            var sellTicketForm = new SellTicketForm(_controller, festival);
            sellTicketForm.Show();
        }
        
        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("ChatWindow closing "+e.CloseReason);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                _controller.logout();
                _controller.updateEvent -= userUpdate;
                Application.Exit();
            }
        }
        
        public void userUpdate(object sender, UserEventArgs e)
        {
            if (e.UserEventType==UserEvent.TicketSold)
            {
                TicketDTO ticket = (TicketDTO)e.Data;
                Console.WriteLine("[Main Window] ticketSold "+ ticket);
                dataGridArtist.BeginInvoke(new UpdateTableCallback(this.updateTable), new Object[]{dataGridArtist,ticket});
                //   friendList.BeginInvoke((Action) delegate { friendList.DataSource = friendsData; });

            }
        }
        
        //for updating the GUI

        //1. define a method for updating the DataGridView
        private void updateTable(DataGridView dataGridView, TicketDTO ticketDto)
        {
            //allData[ticketDto.festivalID].SoldSeats += (int)ticketDto.seats;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if ((int) row.Cells["id"].Value == ticketDto.festivalID)
                {
                    row.Cells["soldSeats"].Value = (int)row.Cells["soldSeats"].Value + (int)ticketDto.seats;
                    if ((int) row.Cells["soldSeats"].Value == (int) row.Cells["availableSeats"].Value)
                        row.DefaultCellStyle.BackColor = Color.Red;
                    return;
                }
            }
            /*
            dataGridView.DataSource = null;
            dataGridView.DataSource = allData.Values.ToArray();
            */
        }
        
        //2. define a delegate to be called back by the GUI Thread
        public delegate void UpdateTableCallback(DataGridView dataGridView, TicketDTO ticketDto);

        

        //3. in the other thread call like this:
        /*
         * list.Invoke(new UpdateListBoxCallback(this.updateListBox), new Object[]{list, data});
         * 
         * */

        private void dataGridArtist_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridArtist.Columns["id"].Visible = false;
            dataGridArtist.Columns["Time"].Visible = false;
            foreach (DataGridViewRow row in this.dataGridArtist.Rows)
            {
                if ((int) row.Cells["soldSeats"].Value == (int) row.Cells["availableSeats"].Value)
                    row.DefaultCellStyle.BackColor = Color.Red;
                  
            }
        }
    }
}
