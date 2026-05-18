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

        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CountVaksinasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter outputParam = new SqlParameter("@Total", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        this.Text = "Form Data Vaksinasi - Total Data: " + outputParam.Value.ToString();
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
            txtIDVaksinasi.DataBindings.Clear();
            txtIDHewan.DataBindings.Clear();
            txtJenisVaksin.DataBindings.Clear();
            dtpTanggalVaksin.DataBindings.Clear();
            dtpTanggalBerikutnya.DataBindings.Clear();
            cmbStatusVaksin.DataBindings.Clear();
            txtKeterangan.DataBindings.Clear();

            txtIDVaksinasi.DataBindings.Add("Text", vaksinasiBindingSource, "IDVaksinasi");
            txtIDHewan.DataBindings.Add("Text", vaksinasiBindingSource, "IDHewan");
            txtJenisVaksin.DataBindings.Add("Text", vaksinasiBindingSource, "JenisVaksin");

            dtpTanggalVaksin.DataBindings.Add("Value", vaksinasiBindingSource, "TanggalVaksin", true, DataSourceUpdateMode.OnValidation, DateTime.Now);
            dtpTanggalBerikutnya.DataBindings.Add("Value", vaksinasiBindingSource, "TanggalBerikutnya", true, DataSourceUpdateMode.OnValidation, DateTime.Now);

            cmbStatusVaksin.DataBindings.Add("Text", vaksinasiBindingSource, "StatusVaksin");
            txtKeterangan.DataBindings.Add("Text", vaksinasiBindingSource, "Keterangan");
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetVaksinasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtVaksinasi = new DataTable();
                            da.Fill(dtVaksinasi);

                            vaksinasiBindingSource.DataSource = dtVaksinasi;
                            dataGridView1.DataSource = vaksinasiBindingSource;

                            bindingNavigator1.BindingSource = vaksinasiBindingSource;

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

        private void Form9_Load(object sender, EventArgs e)
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
                if (txtIDHewan.Text == "" || txtJenisVaksin.Text == "")
                {
                    MessageBox.Show("ID Hewan dan Jenis Vaksin harus diisi!");
                    txtIDHewan.Focus();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertVaksinasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));
                        cmd.Parameters.AddWithValue("@JenisVaksin", txtJenisVaksin.Text);
                        cmd.Parameters.AddWithValue("@TanggalVaksin", dtpTanggalVaksin.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalBerikutnya", dtpTanggalBerikutnya.Value.Date);
                        cmd.Parameters.AddWithValue("@StatusVaksin", cmbStatusVaksin.Text);
                        cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data vaksinasi berhasil ditambahkan");
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
                if (txtIDVaksinasi.Text == "")
                {
                    MessageBox.Show("Pilih data vaksinasi yang ingin diubah terlebih dahulu");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVaksinasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDVaksinasi", int.Parse(txtIDVaksinasi.Text));
                        cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));
                        cmd.Parameters.AddWithValue("@JenisVaksin", txtJenisVaksin.Text);
                        cmd.Parameters.AddWithValue("@TanggalVaksin", dtpTanggalVaksin.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalBerikutnya", dtpTanggalBerikutnya.Value.Date);
                        cmd.Parameters.AddWithValue("@StatusVaksin", cmbStatusVaksin.Text);
                        cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text);

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
                if (txtIDVaksinasi.Text == "")
                {
                    MessageBox.Show("Pilih data yang ingin dihapus terlebih dahulu");
                    return;
                }

                DialogResult resultConfirm = MessageBox.Show(
                    "Yakin ingin menghapus riwayat vaksinasi ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultConfirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteVaksinasi", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDVaksinasi", int.Parse(txtIDVaksinasi.Text));

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
                MessageBox.Show("Gagal menghapus data. Detail: " + ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}