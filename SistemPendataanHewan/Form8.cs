using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form8 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // 1. Batasan Hak Akses Petugas
            if (SesiPengguna.RoleUser == "Petugas")
            {
                btnLaporan.Visible = false;
            }

            // 2. Jalankan Fungsi ExecuteScalar untuk Menghitung Total Data
            HitungTotalHewan();
        }

        // IMPLEMENTASI EXECUTESCALAR (Syarat Komponen Penilaian 10%)
        private void HitungTotalHewan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Pastikan kata 'Hewan' di bawah ini sama dengan nama tabel di database Anda!
                    string query = "SELECT COUNT(*) FROM HewanPeliharaan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int totalHewan = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalHewan.Text = "Total Hewan Terdaftar: " + totalHewan.ToString() + " Ekor";
                    }
                }
            }
            catch (Exception ex)
            {
                lblTotalHewan.Text = "Total Hewan: Gagal Memuat";

                // TAMBAHKAN BARIS INI UNTUK MELIHAT ERROR ASLINYA
                MessageBox.Show("Penyebab gagal memuat: " + ex.Message, "Informasi Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPemilik_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void btnHewan_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Show();
        }

        private void btnVaksinasi_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9();
            frm.Show();
        }

    }
}