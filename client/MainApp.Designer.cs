
namespace LabMPP
{
    partial class MainApp
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
            this.dataGridArtist = new System.Windows.Forms.DataGridView();
            this.dataGridSArtist = new System.Windows.Forms.DataGridView();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridArtist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridSArtist)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridArtist
            // 
            this.dataGridArtist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridArtist.Location = new System.Drawing.Point(68, 271);
            this.dataGridArtist.Name = "dataGridArtist";
            this.dataGridArtist.RowHeadersWidth = 51;
            this.dataGridArtist.RowTemplate.Height = 24;
            this.dataGridArtist.Size = new System.Drawing.Size(566, 238);
            this.dataGridArtist.TabIndex = 0;
            this.dataGridArtist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridArtist_CellContentClick);
            this.dataGridArtist.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridArtist_DataBindingComplete);
            // 
            // dataGridSArtist
            // 
            this.dataGridSArtist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSArtist.Location = new System.Drawing.Point(686, 271);
            this.dataGridSArtist.Name = "dataGridSArtist";
            this.dataGridSArtist.RowHeadersWidth = 51;
            this.dataGridSArtist.RowTemplate.Height = 24;
            this.dataGridSArtist.Size = new System.Drawing.Size(502, 238);
            this.dataGridSArtist.TabIndex = 1;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(906, 169);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(84, 26);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(850, 95);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Artists";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(294, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Sell ticket";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSellTicket_Click);
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 558);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dataGridSArtist);
            this.Controls.Add(this.dataGridArtist);
            this.Name = "MainApp";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainApp_FormClosing);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridArtist)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridSArtist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridArtist;
        private System.Windows.Forms.DataGridView dataGridSArtist;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

