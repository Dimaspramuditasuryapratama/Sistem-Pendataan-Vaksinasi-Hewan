using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form5 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            cmbJenisKelamin.Items.Clear();
            cmbJenisKelamin.Items.Add("L");
            cmbJenisKelamin.Items.Add("P");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
            txtIDHewan.Clear();
            txtIDPemilik.Clear();
            txtNamaHewan.Clear();
            txtJenisHewan.Clear();
            txtRas.Clear();
            cmbJenisKelamin.SelectedIndex = -1;
            txtUmur.Clear();
            txtWarna.Clear();
            txtIDPemilik.Focus();
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
                MessageBox.Show("Gagal menampilkan data: " + ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectDatabase();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDPemilik.Text == "")
                {
                    MessageBox.Show("ID Pemilik harus diisi!");
                    txtIDPemilik.Focus();
                    return;
                }

                if (txtNamaHewan.Text == "")
                {
                    MessageBox.Show("Nama Hewan harus diisi!");
                    txtNamaHewan.Focus();
                    return;
                }

                if (txtJenisHewan.Text == "")
                {
                    MessageBox.Show("Jenis Hewan harus diisi!");
                    txtJenisHewan.Focus();
                    return;
                }

                if (cmbJenisKelamin.Text == "")
                {
                    MessageBox.Show("Jenis Kelamin harus dipilih!");
                    cmbJenisKelamin.Focus();
                    return;
                }

                if (txtUmur.Text == "")
                {
                    MessageBox.Show("Umur harus diisi!");
                    txtUmur.Focus();
                    return;
                }

                if (txtWarna.Text == "")
                {
                    MessageBox.Show("Warna harus diisi!");
                    txtWarna.Focus();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO HewanPeliharaan
                                     (IDPemilik, NamaHewan, JenisHewan, Ras, JenisKelamin, Umur, Warna)
                                     VALUES
                                     (@IDPemilik, @NamaHewan, @JenisHewan, @Ras, @JenisKelamin, @Umur, @Warna)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPemilik", txtIDPemilik.Text);
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text);
                        cmd.Parameters.AddWithValue("@JenisHewan", txtJenisHewan.Text);
                        cmd.Parameters.AddWithValue("@Ras", txtRas.Text);
                        cmd.Parameters.AddWithValue("@JenisKelamin", cmbJenisKelamin.Text);
                        cmd.Parameters.Add("@Umur", System.Data.SqlDbType.Decimal).Value =
                        decimal.Parse(txtUmur.Text);
                        cmd.Parameters.AddWithValue("@Warna", txtWarna.Text);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan.");
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal ditambahkan.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
    }
}