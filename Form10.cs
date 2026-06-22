using System;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            this.FormClosed += (s, args) =>
            {
                if (SesiPengguna.IDPengguna > 0)
                {
                    Form8 frm = new Form8();
                    frm.Show();
                }
            };

            btnLaporanPemilik.Visible = false;
            btnLaporanVaksinasi.Visible = false;

            btnLaporanHewan.Text = "LAPORAN LENGKAP";
            btnLaporanHewan.BackColor = System.Drawing.Color.MediumAquamarine;
            btnLaporanHewan.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            btnLaporanHewan.Size = new System.Drawing.Size(300, 60);
            btnLaporanHewan.Location = new System.Drawing.Point(250, 100);
            btnLaporanHewan.ForeColor = System.Drawing.Color.White;

            dataGridView1.Visible = false;

            btnTutup.Location = new System.Drawing.Point(350, 200);
            btnTutup.Size = new System.Drawing.Size(100, 40);
            btnTutup.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            btnTutup.BackColor = System.Drawing.Color.LightGray;
        }

        private void btnLaporanHewan_Click(object sender, EventArgs e)
        {
            FormFilterReport frm = new FormFilterReport();
            frm.Show();
            this.Hide();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLaporanPemilik_Click(object sender, EventArgs e) { }
        private void btnLaporanVaksinasi_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) { }
    }
}