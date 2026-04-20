using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form1()
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
            txtIDPemilik.Clear();
            txtNamaPemilik.Clear();
            txtAlamat.Clear();
            txtNoHP.Clear();
            txtRTRW.Clear();
            txtNamaPemilik.Focus();
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

                dataGridView1.Columns.Add("IDPemilik", "ID Pemilik");
                dataGridView1.Columns.Add("NamaPemilik", "Nama Pemilik");
                dataGridView1.Columns.Add("Alamat", "Alamat");
                dataGridView1.Columns.Add("NoHP", "No. HP");
                dataGridView1.Columns.Add("RTRW", "RT/RW");

                string query = "SELECT * FROM Pemilik";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

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
                if (txtNamaPemilik.Text == "")
                {
                    MessageBox.Show("Nama Pemilik harus diisi!");
                    txtNamaPemilik.Focus();
                    return;
                }

                if (txtAlamat.Text == "")
                {
                    MessageBox.Show("Alamat harus diisi!");
                    txtAlamat.Focus();
                    return;
                }

                if (txtNoHP.Text == "")
                {
                    MessageBox.Show("No. HP harus diisi!");
                    txtNoHP.Focus();
                    return;
                }

                if (txtRTRW.Text == "")
                {
                    MessageBox.Show("RT/RW harus diisi!");
                    txtRTRW.Focus();
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"INSERT INTO Pemilik (NamaPemilik, Alamat, NoHP, RTRW)
                                 VALUES (@NamaPemilik, @Alamat, @NoHP, @RTRW)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NamaPemilik", txtNamaPemilik.Text);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text);
                cmd.Parameters.AddWithValue("@RTRW", txtRTRW.Text);

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
                if (txtIDPemilik.Text == "")
                {
                    MessageBox.Show("Pilih data pada tabel terlebih dahulu!");
                    return;
                }

                if (txtNamaPemilik.Text == "")
                {
                    MessageBox.Show("Nama Pemilik harus diisi!");
                    txtNamaPemilik.Focus();
                    return;
                }

                if (txtAlamat.Text == "")
                {
                    MessageBox.Show("Alamat harus diisi!");
                    txtAlamat.Focus();
                    return;
                }

                if (txtNoHP.Text == "")
                {
                    MessageBox.Show("No. HP harus diisi!");
                    txtNoHP.Focus();
                    return;
                }

                if (txtRTRW.Text == "")
                {
                    MessageBox.Show("RT/RW harus diisi!");
                    txtRTRW.Focus();
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE Pemilik
                                 SET NamaPemilik = @NamaPemilik,
                                     Alamat = @Alamat,
                                     NoHP = @NoHP,
                                     RTRW = @RTRW
                                 WHERE IDPemilik = @IDPemilik";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPemilik", txtIDPemilik.Text);
                cmd.Parameters.AddWithValue("@NamaPemilik", txtNamaPemilik.Text);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text);
                cmd.Parameters.AddWithValue("@RTRW", txtRTRW.Text);

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
                if (txtIDPemilik.Text == "")
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

                    string query = "DELETE FROM Pemilik WHERE IDPemilik = @IDPemilik";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IDPemilik", txtIDPemilik.Text);

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

                txtIDPemilik.Text = row.Cells["IDPemilik"].Value?.ToString();
                txtNamaPemilik.Text = row.Cells["NamaPemilik"].Value?.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value?.ToString();
                txtNoHP.Text = row.Cells["NoHP"].Value?.ToString();
                txtRTRW.Text = row.Cells["RTRW"].Value?.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.CellClick += dataGridView1_CellClick;
        }
    }
}