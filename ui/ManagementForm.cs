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
    partial class ManagementForm : Form
    {
        private MainPageService mainPageService;
        public ManagementForm()
        {
            InitializeComponent();
        }
        public void setServices(MainPageService mainPageService)
        {
            this.mainPageService = mainPageService;
        }

        private void buttonAddArtist_Click(object sender, EventArgs e)
        {
            String name = this.textBoxArtistName.Text;
            String genre = this.textBoxArtistGenre.Text;
            try
            {
                mainPageService.addArtist(name, genre);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddFestival_Click(object sender, EventArgs e)
        {
            DateTime date = this.dateTimePickerFestival.Value;
            String location = this.textBoxFestivalLocation.Text;
            String name = this.textBoxFestivalName.Text;
            String genre = this.textBoxFestivalGenre.Text;
            int seats = int.Parse(textBoxFestivalSeats.Text);
            long aid = long.Parse(textBoxFestivalAID.Text);

            try
            {
                mainPageService.addFestival(date, location, name, genre, seats, aid);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
