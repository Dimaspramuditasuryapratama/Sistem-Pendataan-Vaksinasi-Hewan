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
                MessageBox.Show("Penyebab gagal memuat: " + ex.Message, "Informasi Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}