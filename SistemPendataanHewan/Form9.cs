using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form9 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtVaksinasi = new DataTable();
        private BindingSource vaksinasiBindingSource = new BindingSource();

        public Form9()
        {
            InitializeComponent();
        }

        private void ConnectDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Koneksi ke DBHewanPeliharaanADO berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtIDVaksinasi.Clear();
            txtIDHewan.Clear();
            txtJenisVaksin.Clear();

            dtpTanggalVaksin.Value = DateTime.Now;
            dtpTanggalBerikutnya.Value = DateTime.Now.AddMonths(1);

            cmbStatusVaksin.SelectedIndex = -1;
            txtKeterangan.Clear();

            txtIDHewan.Focus();
        }

    }
}