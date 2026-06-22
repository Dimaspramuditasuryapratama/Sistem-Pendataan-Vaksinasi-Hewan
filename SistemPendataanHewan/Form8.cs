using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form8 : Form
    {
        // Pastikan Data Source sesuai dengan nama server SQL Server kamu
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // 1. Batasan Hak Akses: Petugas tidak boleh melihat Menu Laporan
            if (SesiPengguna.RoleUser == "Petugas")
            {
                btnLaporan.Visible = false;
            }
            else if (SesiPengguna.RoleUser == "Admin")
            {
                btnLaporan.Visible = true;
            }

            // 2. Jalankan Fungsi ExecuteScalar untuk Menghitung Total Data
            HitungTotalHewan();
        }

        // IMPLEMENTASI EXECUTESCALAR (Syarat Komponen Penilaian 10%)
        // IMPLEMENTASI EXECUTESCALAR
        private void HitungTotalHewan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // PERBAIKAN DI SINI: Ubah 'Hewan' menjadi 'HewanPeliharaan'
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
                MessageBox.Show("Penyebab gagal memuat total hewan: " + ex.Message, "Informasi Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnLaporan_Click(object sender, EventArgs e)
        {
            Form10 frm = new Form10();
            frm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult resultConfirm = MessageBox.Show(
                "Yakin ingin keluar dari sistem?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultConfirm == DialogResult.Yes)
            {
                // Membersihkan Sesi
                SesiPengguna.IDPengguna = 0;
                SesiPengguna.NamaPengguna = "";
                SesiPengguna.RoleUser = "";

                MessageBox.Show("Anda telah berhasil keluar dari sistem.", "Logout Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Kembali ke Form Login (Form4)
                Form4 login = new Form4();
                login.Show();
                this.Close();
            }
        }
    }
}