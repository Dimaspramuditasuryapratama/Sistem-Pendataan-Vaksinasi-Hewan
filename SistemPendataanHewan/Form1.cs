using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            AturDataGridView();

            // Kunci tombol Hapus untuk Petugas
            if (SesiPengguna.RoleUser == "Petugas")
            {
                btnDelete.Visible = false;
            }
        }

        private void AturDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void AturLebarKolom()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["IDPemilik"].FillWeight = 15;
                dataGridView1.Columns["NamaPemilik"].FillWeight = 25;
                dataGridView1.Columns["Alamat"].FillWeight = 35;
                dataGridView1.Columns["NoHP"].FillWeight = 20;
                dataGridView1.Columns["RTRW"].FillWeight = 15;
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

                    AturDataGridView();
                    AturLebarKolom();

                    string query = "SELECT IDPemilik, NamaPemilik, Alamat, NoHP, RTRW FROM Pemilik";

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

                    AturLebarKolom();
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
                if (txtNamaPemilik.Text.Trim() == "")
                {
                    MessageBox.Show("Nama Pemilik harus diisi!");
                    txtNamaPemilik.Focus();
                    return;
                }

                if (txtAlamat.Text.Trim() == "")
                {
                    MessageBox.Show("Alamat harus diisi!");
                    txtAlamat.Focus();
                    return;
                }

                if (txtNoHP.Text.Trim() == "")
                {
                    MessageBox.Show("No. HP harus diisi!");
                    txtNoHP.Focus();
                    return;
                }

                if (txtRTRW.Text.Trim() == "")
                {
                    MessageBox.Show("RT/RW harus diisi!");
                    txtRTRW.Focus();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Pemilik 
                                     (NamaPemilik, Alamat, NoHP, RTRW)
                                     VALUES 
                                     (@NamaPemilik, @Alamat, @NoHP, @RTRW)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NamaPemilik", txtNamaPemilik.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text.Trim());
                        cmd.Parameters.AddWithValue("@RTRW", txtRTRW.Text.Trim());

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