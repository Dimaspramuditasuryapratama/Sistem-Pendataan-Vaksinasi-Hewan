namespace SistemPendataanHewan
{
    partial class Form10
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
            this.btnLaporanPemilik = new System.Windows.Forms.Button();
            this.btnLaporanHewan = new System.Windows.Forms.Button();
            this.btnLaporanVaksinasi = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "FORM LAPORAN";
            // 
            // btnLaporanPemilik
            // 
            this.btnLaporanPemilik.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLaporanPemilik.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaporanPemilik.Location = new System.Drawing.Point(31, 75);
            this.btnLaporanPemilik.Name = "btnLaporanPemilik";
            this.btnLaporanPemilik.Size = new System.Drawing.Size(160, 45);
            this.btnLaporanPemilik.TabIndex = 1;
            this.btnLaporanPemilik.Text = "Laporan Pemilik";
            this.btnLaporanPemilik.UseVisualStyleBackColor = false;
            this.btnLaporanPemilik.Click += new System.EventHandler(this.btnLaporanPemilik_Click);
            // 
            // btnLaporanHewan
            // 
            this.btnLaporanHewan.BackColor = System.Drawing.Color.LightGreen;
            this.btnLaporanHewan.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaporanHewan.Location = new System.Drawing.Point(207, 75);
            this.btnLaporanHewan.Name = "btnLaporanHewan";
            this.btnLaporanHewan.Size = new System.Drawing.Size(160, 45);
            this.btnLaporanHewan.TabIndex = 2;
            this.btnLaporanHewan.Text = "Laporan Hewan";
            this.btnLaporanHewan.UseVisualStyleBackColor = false;
            this.btnLaporanHewan.Click += new System.EventHandler(this.btnLaporanHewan_Click);
            // 
            // btnLaporanVaksinasi
            // 
            this.btnLaporanVaksinasi.BackColor = System.Drawing.Color.PeachPuff;
            this.btnLaporanVaksinasi.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaporanVaksinasi.Location = new System.Drawing.Point(383, 75);
            this.btnLaporanVaksinasi.Name = "btnLaporanVaksinasi";
            this.btnLaporanVaksinasi.Size = new System.Drawing.Size(160, 45);
            this.btnLaporanVaksinasi.TabIndex = 3;
            this.btnLaporanVaksinasi.Text = "Laporan Vaksinasi";
            this.btnLaporanVaksinasi.UseVisualStyleBackColor = false;
            this.btnLaporanVaksinasi.Click += new System.EventHandler(this.btnLaporanVaksinasi_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTutup.BackColor = System.Drawing.Color.LightGray;
            this.btnTutup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTutup.Location = new System.Drawing.Point(670, 400);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(100, 35);
            this.btnTutup.TabIndex = 4;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = false;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(739, 240);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Form10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.btnLaporanVaksinasi);
            this.Controls.Add(this.btnLaporanHewan);
            this.Controls.Add(this.btnLaporanPemilik);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form10";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Laporan";
            this.Load += new System.EventHandler(this.Form10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLaporanPemilik;
        private System.Windows.Forms.Button btnLaporanHewan;
        private System.Windows.Forms.Button btnLaporanVaksinasi;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}