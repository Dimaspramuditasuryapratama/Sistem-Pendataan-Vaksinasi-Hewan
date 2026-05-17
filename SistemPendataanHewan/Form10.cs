using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form10 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnLaporanPemilik_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    dataGridView1.Columns.Add("IDPemilik", "ID Pemilik");
                    dataGridView1.Columns.Add("NamaPemilik", "Nama Pemilik");
                    dataGridView1.Columns.Add("Alamat", "Alamat");
                    dataGridView1.Columns.Add("NoHP", "No. HP");
                    dataGridView1.Columns.Add("RTRW", "RT/RW");

                    string query = "SELECT * FROM Pemilik";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["IDPemilik"].ToString(),
                                reader["NamaPemilik"].ToString(),
                                reader["Alamat"].ToString(),
                                reader["NoHP"].ToString(),
                                reader["RTRW"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan laporan pemilik: " + ex.Message);
            }
        }

        private void btnLaporanHewan_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    dataGridView1.Columns.Add("IDHewan", "ID Hewan");
                    dataGridView1.Columns.Add("IDPemilik", "ID Pemilik");
                    dataGridView1.Columns.Add("NamaHewan", "Nama Hewan");
                    dataGridView1.Columns.Add("JenisHewan", "Jenis Hewan");
                    dataGridView1.Columns.Add("Ras", "Ras");
                    dataGridView1.Columns.Add("JenisKelamin", "Jenis Kelamin");
                    dataGridView1.Columns.Add("Umur", "Umur");
                    dataGridView1.Columns.Add("Warna", "Warna");

                    string query = "SELECT * FROM HewanPeliharaan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["IDHewan"].ToString(),
                                reader["IDPemilik"].ToString(),
                                reader["NamaHewan"].ToString(),
                                reader["JenisHewan"].ToString(),
                                reader["Ras"].ToString(),
                                reader["JenisKelamin"].ToString(),
                                reader["Umur"].ToString(),
                                reader["Warna"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan laporan hewan: " + ex.Message);
            }
        }
    }
}