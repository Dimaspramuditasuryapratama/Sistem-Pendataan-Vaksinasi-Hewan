namespace SistemPendataanHewan
{
    partial class Form8
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnPemilik = new System.Windows.Forms.Button();
            this.btnHewan = new System.Windows.Forms.Button();
            this.btnVaksinasi = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLaporan = new System.Windows.Forms.Button();
            this.lblTotalHewan = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(140, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "MENU UTAMA SIPETRA";
            // 
            // btnPemilik
            // 
            this.btnPemilik.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPemilik.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPemilik.Location = new System.Drawing.Point(175, 110);
            this.btnPemilik.Name = "btnPemilik";
            this.btnPemilik.Size = new System.Drawing.Size(250, 50);
            this.btnPemilik.TabIndex = 1;
            this.btnPemilik.Text = "Data Pemilik Hewan";
            this.btnPemilik.UseVisualStyleBackColor = false;
            this.btnPemilik.Click += new System.EventHandler(this.btnPemilik_Click);
            // 
            // btnHewan
            // 
            this.btnHewan.BackColor = System.Drawing.Color.LightGreen;
            this.btnHewan.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHewan.Location = new System.Drawing.Point(175, 175);
            this.btnHewan.Name = "btnHewan";
            this.btnHewan.Size = new System.Drawing.Size(250, 50);
            this.btnHewan.TabIndex = 2;
            this.btnHewan.Text = "Data Hewan Peliharaan";
            this.btnHewan.UseVisualStyleBackColor = false;
            this.btnHewan.Click += new System.EventHandler(this.btnHewan_Click);
            // 
            // btnVaksinasi
            // 
            this.btnVaksinasi.BackColor = System.Drawing.Color.PeachPuff;
            this.btnVaksinasi.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVaksinasi.Location = new System.Drawing.Point(175, 240);
            this.btnVaksinasi.Name = "btnVaksinasi";
            this.btnVaksinasi.Size = new System.Drawing.Size(250, 50);
            this.btnVaksinasi.TabIndex = 3;
            this.btnVaksinasi.Text = "Data Vaksinasi";
            this.btnVaksinasi.UseVisualStyleBackColor = false;
            this.btnVaksinasi.Click += new System.EventHandler(this.btnVaksinasi_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.LightCoral;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(175, 370);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(250, 50);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLaporan
            // 
            this.btnLaporan.BackColor = System.Drawing.Color.Thistle;
            this.btnLaporan.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaporan.Location = new System.Drawing.Point(175, 305);
            this.btnLaporan.Name = "btnLaporan";
            this.btnLaporan.Size = new System.Drawing.Size(250, 50);
            this.btnLaporan.TabIndex = 5;
            this.btnLaporan.Text = "Laporan";
            this.btnLaporan.UseVisualStyleBackColor = false;
            this.btnLaporan.Click += new System.EventHandler(this.btnLaporan_Click);
            // 
            // lblTotalHewan
            // 
            this.lblTotalHewan.AutoSize = true;
            this.lblTotalHewan.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHewan.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTotalHewan.Location = new System.Drawing.Point(175, 450);
            this.lblTotalHewan.Name = "lblTotalHewan";
            this.lblTotalHewan.Size = new System.Drawing.Size(218, 25);
            this.lblTotalHewan.TabIndex = 6;
            this.lblTotalHewan.Text = "Total Hewan Terdaftar: -";
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(600, 510);
            this.Controls.Add(this.lblTotalHewan);
            this.Controls.Add(this.btnLaporan);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnVaksinasi);
            this.Controls.Add(this.btnHewan);
            this.Controls.Add(this.btnPemilik);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Utama SIPETRA";
            this.Load += new System.EventHandler(this.Form8_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPemilik;
        private System.Windows.Forms.Button btnHewan;
        private System.Windows.Forms.Button btnVaksinasi;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLaporan;
        private System.Windows.Forms.Label lblTotalHewan;
    }
}