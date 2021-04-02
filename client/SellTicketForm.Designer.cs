
namespace LabMPP.ui
{
    partial class SellTicketForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSellTicket = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNumeCumparator = new System.Windows.Forms.TextBox();
            this.textBoxFestival = new System.Windows.Forms.TextBox();
            this.comboBoxNumarBilete = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSellTicket
            // 
            this.btnSellTicket.Location = new System.Drawing.Point(290, 310);
            this.btnSellTicket.Name = "btnSellTicket";
            this.btnSellTicket.Size = new System.Drawing.Size(100, 23);
            this.btnSellTicket.TabIndex = 0;
            this.btnSellTicket.Text = "Sell Ticket";
            this.btnSellTicket.UseVisualStyleBackColor = true;
            this.btnSellTicket.Click += new System.EventHandler(this.btnSellTicket_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nume client";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numar bilete";
            // 
            // textBoxNumeCumparator
            // 
            this.textBoxNumeCumparator.Location = new System.Drawing.Point(337, 130);
            this.textBoxNumeCumparator.Name = "textBoxNumeCumparator";
            this.textBoxNumeCumparator.Size = new System.Drawing.Size(100, 22);
            this.textBoxNumeCumparator.TabIndex = 3;
            // 
            // textBoxFestival
            // 
            this.textBoxFestival.Location = new System.Drawing.Point(82, 36);
            this.textBoxFestival.Multiline = true;
            this.textBoxFestival.Name = "textBoxFestival";
            this.textBoxFestival.Size = new System.Drawing.Size(573, 56);
            this.textBoxFestival.TabIndex = 5;
            // 
            // comboBoxNumarBilete
            // 
            this.comboBoxNumarBilete.FormattingEnabled = true;
            this.comboBoxNumarBilete.Location = new System.Drawing.Point(337, 195);
            this.comboBoxNumarBilete.Name = "comboBoxNumarBilete";
            this.comboBoxNumarBilete.Size = new System.Drawing.Size(40, 24);
            this.comboBoxNumarBilete.TabIndex = 6;
            // 
            // SellTicketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxNumarBilete);
            this.Controls.Add(this.textBoxFestival);
            this.Controls.Add(this.textBoxNumeCumparator);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSellTicket);
            this.Name = "SellTicketForm";
            this.Text = "SellTicketForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSellTicket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNumeCumparator;
        private System.Windows.Forms.TextBox textBoxFestival;
        private System.Windows.Forms.ComboBox comboBoxNumarBilete;
    }
}