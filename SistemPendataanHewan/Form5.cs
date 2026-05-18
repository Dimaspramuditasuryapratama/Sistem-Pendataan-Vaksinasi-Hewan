using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form5 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtHewan = new DataTable();
        private BindingSource hewanBindingSource = new BindingSource();

        public Form5()
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

        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CountHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter outputParam = new SqlParameter("@Total", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        this.Text = "Form Data Hewan - Total Data: " + outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghitung total: " + ex.Message);
            }
        }

        private void BindControls()
        {
            txtIDHewan.DataBindings.Clear();
            txtIDPemilik.DataBindings.Clear();
            txtNamaHewan.DataBindings.Clear();
            txtJenisHewan.DataBindings.Clear();
            txtRas.DataBindings.Clear();
            cmbJenisKelamin.DataBindings.Clear();
            txtUmur.DataBindings.Clear();
            txtWarna.DataBindings.Clear();

            txtIDHewan.DataBindings.Add("Text", hewanBindingSource, "IDHewan");
            txtIDPemilik.DataBindings.Add("Text", hewanBindingSource, "IDPemilik");
            txtNamaHewan.DataBindings.Add("Text", hewanBindingSource, "NamaHewan");
            txtJenisHewan.DataBindings.Add("Text", hewanBindingSource, "JenisHewan");
            txtRas.DataBindings.Add("Text", hewanBindingSource, "Ras");
            cmbJenisKelamin.DataBindings.Add("Text", hewanBindingSource, "JenisKelamin");
            txtUmur.DataBindings.Add("Text", hewanBindingSource, "Umur");
            txtWarna.DataBindings.Add("Text", hewanBindingSource, "Warna");
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtHewan = new DataTable();
                            da.Fill(dtHewan);

                            hewanBindingSource.DataSource = dtHewan;
                            dataGridView1.DataSource = hewanBindingSource;

                            bindingNavigator1.BindingSource = hewanBindingSource;

                            BindControls();
                        }
                    }
                }
                HitungTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
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

            LoadData();
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
                if (txtNamaHewan.Text == "" || txtIDPemilik.Text == "")
                {
                    MessageBox.Show("ID Pemilik dan Nama Hewan harus diisi!");
                    txtIDPemilik.Focus();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text);
                        cmd.Parameters.AddWithValue("@JenisHewan", txtJenisHewan.Text);
                        cmd.Parameters.AddWithValue("@Ras", txtRas.Text);
                        cmd.Parameters.AddWithValue("@JenisKelamin", cmbJenisKelamin.Text);
                        cmd.Parameters.AddWithValue("@Umur", decimal.Parse(txtUmur.Text));
                        cmd.Parameters.AddWithValue("@Warna", txtWarna.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data hewan berhasil ditambahkan");
                        ClearForm();
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat insert: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDHewan.Text == "")
                {
                    MessageBox.Show("Pilih data hewan yang ingin diubah terlebih dahulu");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));
                        cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text);
                        cmd.Parameters.AddWithValue("@JenisHewan", txtJenisHewan.Text);
                        cmd.Parameters.AddWithValue("@Ras", txtRas.Text);
                        cmd.Parameters.AddWithValue("@JenisKelamin", cmbJenisKelamin.Text);
                        cmd.Parameters.AddWithValue("@Umur", decimal.Parse(txtUmur.Text));
                        cmd.Parameters.AddWithValue("@Warna", txtWarna.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data berhasil diupdate");
                        ClearForm();
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat update: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDHewan.Text == "")
                {
                    MessageBox.Show("Pilih data yang ingin dihapus terlebih dahulu");
                    return;
                }

                DialogResult resultConfirm = MessageBox.Show(
                    "Yakin ingin menghapus data hewan ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultConfirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteHewan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data berhasil dihapus");
                                ClearForm();
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data tidak ditemukan");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data. Kemungkinan data ini sedang digunakan di tabel Vaksinasi.\n\nDetail: " + ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtIDHewan.Text = row.Cells["IDHewan"].Value.ToString();
                txtIDPemilik.Text = row.Cells["IDPemilik"].Value.ToString();
                txtNamaHewan.Text = row.Cells["NamaHewan"].Value.ToString();
                txtJenisHewan.Text = row.Cells["JenisHewan"].Value.ToString();
                txtRas.Text = row.Cells["Ras"].Value.ToString();
                cmbJenisKelamin.Text = row.Cells["JenisKelamin"].Value.ToString();
                txtUmur.Text = row.Cells["Umur"].Value.ToString();
                txtWarna.Text = row.Cells["Warna"].Value.ToString();
            }
        }
    }
}