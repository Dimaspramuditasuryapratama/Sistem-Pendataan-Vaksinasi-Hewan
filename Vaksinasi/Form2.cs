using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class FormVaksinasi : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public FormVaksinasi()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void ConnectDatabase()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                MessageBox.Show("Koneksi berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

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
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["IDVaksinasi"].ToString(),
                        reader["IDHewan"].ToString(),
                        reader["JenisVaksin"].ToString(),
                        Convert.ToDateTime(reader["TanggalVaksin"]).ToShortDateString(),
                        Convert.ToDateTime(reader["TanggalBerikutnya"]).ToShortDateString(),
                        reader["StatusVaksin"].ToString(),
                        reader["Keterangan"].ToString()
                    );
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
                if (txtIDHewan.Text == "")
                {
                    MessageBox.Show("ID Hewan harus diisi!");
                    txtIDHewan.Focus();
                    return;
                }

                if (txtJenisVaksin.Text == "")
                {
                    MessageBox.Show("Jenis Vaksin harus diisi!");
                    txtJenisVaksin.Focus();
                    return;
                }

                if (cmbStatusVaksin.Text == "")
                {
                    MessageBox.Show("Status Vaksin harus dipilih!");
                    cmbStatusVaksin.Focus();
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"INSERT INTO Vaksinasi
                                (IDHewan, JenisVaksin, TanggalVaksin, TanggalBerikutnya, StatusVaksin, Keterangan)
                                VALUES
                                (@IDHewan, @JenisVaksin, @TanggalVaksin, @TanggalBerikutnya, @StatusVaksin, @Keterangan)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDHewan", txtIDHewan.Text);
                cmd.Parameters.AddWithValue("@JenisVaksin", txtJenisVaksin.Text);
                cmd.Parameters.AddWithValue("@TanggalVaksin", dtpTanggalVaksin.Value.Date);
                cmd.Parameters.AddWithValue("@TanggalBerikutnya", dtpTanggalBerikutnya.Value.Date);
                cmd.Parameters.AddWithValue("@StatusVaksin", cmbStatusVaksin.Text);
                cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text);

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
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDVaksinasi.Text == "")
                {
                    MessageBox.Show("Pilih data pada tabel terlebih dahulu!");
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE Vaksinasi
                                 SET IDHewan = @IDHewan,
                                     JenisVaksin = @JenisVaksin,
                                     TanggalVaksin = @TanggalVaksin,
                                     TanggalBerikutnya = @TanggalBerikutnya,
                                     StatusVaksin = @StatusVaksin,
                                     Keterangan = @Keterangan
                                 WHERE IDVaksinasi = @IDVaksinasi";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDVaksinasi", txtIDVaksinasi.Text);
                cmd.Parameters.AddWithValue("@IDHewan", txtIDHewan.Text);
                cmd.Parameters.AddWithValue("@JenisVaksin", txtJenisVaksin.Text);
                cmd.Parameters.AddWithValue("@TanggalVaksin", dtpTanggalVaksin.Value.Date);
                cmd.Parameters.AddWithValue("@TanggalBerikutnya", dtpTanggalBerikutnya.Value.Date);
                cmd.Parameters.AddWithValue("@StatusVaksin", cmbStatusVaksin.Text);
                cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data berhasil diubah.");
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDVaksinasi.Text == "")
                {
                    MessageBox.Show("Pilih data pada tabel terlebih dahulu!");
                    return;
                }

                DialogResult konfirmasi = MessageBox.Show(
                    "Yakin ingin menghapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "DELETE FROM Vaksinasi WHERE IDVaksinasi = @IDVaksinasi";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IDVaksinasi", txtIDVaksinasi.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus.");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtIDVaksinasi.Text = row.Cells["IDVaksinasi"].Value?.ToString();
                txtIDHewan.Text = row.Cells["IDHewan"].Value?.ToString();
                txtJenisVaksin.Text = row.Cells["JenisVaksin"].Value?.ToString();
                dtpTanggalVaksin.Value = Convert.ToDateTime(row.Cells["TanggalVaksin"].Value);
                dtpTanggalBerikutnya.Value = Convert.ToDateTime(row.Cells["TanggalBerikutnya"].Value);
                cmbStatusVaksin.Text = row.Cells["StatusVaksin"].Value?.ToString();
                txtKeterangan.Text = row.Cells["Keterangan"].Value?.ToString();
            }
        }

        private void FormVaksinasi_Load(object sender, EventArgs e)
        {
            cmbStatusVaksin.Items.Clear();
            cmbStatusVaksin.Items.Add("Sudah");
            cmbStatusVaksin.Items.Add("Belum");
            cmbStatusVaksin.Items.Add("Ulang");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.CellClick += dataGridView1_CellClick;
        }
    }
}
