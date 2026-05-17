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
    }
}