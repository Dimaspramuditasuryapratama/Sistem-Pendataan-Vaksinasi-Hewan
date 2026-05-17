using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form9 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            cmbStatusVaksin.Items.Clear();
            cmbStatusVaksin.Items.Add("Sudah");
            cmbStatusVaksin.Items.Add("Belum");
            cmbStatusVaksin.Items.Add("Ulang");

            txtIDVaksinasi.ReadOnly = true;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // -------- PERBAIKAN DI SINI --------
            // Hapus atau comment baris di bawah ini agar data tidak otomatis muncul
            // LoadData(); 
            // -----------------------------------

            // Kunci tombol Hapus untuk Petugas
            if (SesiPengguna.RoleUser == "Petugas")
            {
                btnDelete.Visible = false;
            }
        }

        private void ConnectDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Koneksi berhasil!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtIDVaksinasi.Clear();
            txtIDHewan.Clear();
            txtJenisVaksin.Clear();
            dtpTanggalVaksin.Value = DateTime.Now;
            dtpTanggalBerikutnya.Value = DateTime.Now;
            cmbStatusVaksin.SelectedIndex = -1;
            txtKeterangan.Clear();
            txtIDHewan.Focus();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    dataGridView1.Columns.Add("IDVaksinasi", "ID Vaksinasi");
                    dataGridView1.Columns.Add("IDHewan", "ID Hewan");
                    dataGridView1.Columns.Add("JenisVaksin", "Jenis Vaksin");
                    dataGridView1.Columns.Add("TanggalVaksin", "Tanggal Vaksin");
                    dataGridView1.Columns.Add("TanggalBerikutnya", "Tanggal Berikutnya");
                    dataGridView1.Columns.Add("StatusVaksin", "Status Vaksin");
                    dataGridView1.Columns.Add("Keterangan", "Keterangan");

                    string query = "SELECT * FROM Vaksinasi";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["IDVaksinasi"].ToString(),
                                reader["IDHewan"].ToString(),
                                reader["JenisVaksin"].ToString(),
                                Convert.ToDateTime(reader["TanggalVaksin"]).ToString("yyyy-MM-dd"),
                                Convert.ToDateTime(reader["TanggalBerikutnya"]).ToString("yyyy-MM-dd"),
                                reader["StatusVaksin"].ToString(),
                                reader["Keterangan"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message);
            }
        }

    }
}