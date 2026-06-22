
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form9 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtVaksinasi = new DataTable();
        private BindingSource vaksinasiBindingSource = new BindingSource();

        public Form9()
        {
            InitializeComponent();
        }

        private void ClearForm()
        {
            txtIDVaksinasi.Clear();
            txtIDHewan.Clear();
            cmbJenisVaksin.SelectedIndex = -1;
            cmbKeterangan.SelectedIndex = -1;
            dtpTanggalVaksin.Value = DateTime.Now;
            dtpTanggalBerikutnya.Value = DateTime.Now.AddMonths(1);
            cmbStatusVaksin.SelectedIndex = -1;
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
            cmbJenisVaksin.DataBindings.Clear();
            cmbKeterangan.DataBindings.Clear();
            dtpTanggalVaksin.DataBindings.Clear();
            dtpTanggalBerikutnya.DataBindings.Clear();
            cmbStatusVaksin.DataBindings.Clear();

            txtIDVaksinasi.DataBindings.Add("Text", vaksinasiBindingSource, "IDVaksinasi");
            txtIDHewan.DataBindings.Add("Text", vaksinasiBindingSource, "IDHewan");
            cmbJenisVaksin.DataBindings.Add("Text", vaksinasiBindingSource, "JenisVaksin");
            cmbKeterangan.DataBindings.Add("Text", vaksinasiBindingSource, "Keterangan");
            dtpTanggalVaksin.DataBindings.Add("Value", vaksinasiBindingSource, "TanggalVaksin", true, DataSourceUpdateMode.OnValidation, DateTime.Now);
            dtpTanggalBerikutnya.DataBindings.Add("Value", vaksinasiBindingSource, "TanggalBerikutnya", true, DataSourceUpdateMode.OnValidation, DateTime.Now);
            cmbStatusVaksin.DataBindings.Add("Text", vaksinasiBindingSource, "StatusVaksin");
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // PERBAIKAN: MEMANGGIL VIEW SECARA LANGSUNG
                    string queryView = "SELECT * FROM vw_DataVaksinasi";
                    using (SqlCommand cmd = new SqlCommand(queryView, conn))
                    {
                        cmd.CommandType = CommandType.Text;
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

        private void TerapkanHakAkses()
        {
            if (SesiPengguna.RoleUser == "Petugas")
            {
                btnDelete.Visible = false;
                if (bindingNavigatorDeleteItem != null) bindingNavigatorDeleteItem.Enabled = false;
            }
            else if (SesiPengguna.RoleUser == "Admin")
            {
                btnDelete.Visible = true;
                if (bindingNavigatorDeleteItem != null) bindingNavigatorDeleteItem.Enabled = true;
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            cmbJenisVaksin.Items.Clear();
            cmbJenisVaksin.Items.Add("Rabies");
            cmbJenisVaksin.Items.Add("Distemper");
            cmbJenisVaksin.Items.Add("Parvovirus");

            cmbKeterangan.Items.Clear();
            cmbKeterangan.Items.Add("Vaksin pertama");
            cmbKeterangan.Items.Add("Kondisi sehat");
            cmbKeterangan.Items.Add("Perlu kontrol ulang");
            cmbKeterangan.Items.Add("Sudah sembuh");
            cmbKeterangan.Items.Add("Masih observasi");

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
            TerapkanHakAkses();
        }

        private void btnConnect_Click(object sender, EventArgs e) { /* ... */ }
        private void btnLoad_Click(object sender, EventArgs e) { LoadData(); }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDHewan.Text == "" || cmbJenisVaksin.Text == "") return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertVaksinasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));
                        cmd.Parameters.AddWithValue("@JenisVaksin", cmbJenisVaksin.Text);
                        cmd.Parameters.AddWithValue("@TanggalVaksin", dtpTanggalVaksin.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalBerikutnya", dtpTanggalBerikutnya.Value.Date);
                        cmd.Parameters.AddWithValue("@StatusVaksin", cmbStatusVaksin.Text);
                        cmd.Parameters.AddWithValue("@Keterangan", cmbKeterangan.Text);

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
                if (txtIDVaksinasi.Text == "") return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVaksinasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDVaksinasi", int.Parse(txtIDVaksinasi.Text));
                        cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));
                        cmd.Parameters.AddWithValue("@JenisVaksin", cmbJenisVaksin.Text);
                        cmd.Parameters.AddWithValue("@TanggalVaksin", dtpTanggalVaksin.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalBerikutnya", dtpTanggalBerikutnya.Value.Date);
                        cmd.Parameters.AddWithValue("@StatusVaksin", cmbStatusVaksin.Text);
                        cmd.Parameters.AddWithValue("@Keterangan", cmbKeterangan.Text);

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
                if (txtIDVaksinasi.Text == "") return;

                if (MessageBox.Show("Yakin ingin menghapus riwayat vaksinasi ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteVaksinasi", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDVaksinasi", int.Parse(txtIDVaksinasi.Text));

                            conn.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data berhasil dihapus");
                            ClearForm();
                            LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data. Detail: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtIDVaksinasi.Text = row.Cells["IDVaksinasi"].Value.ToString();
                txtIDHewan.Text = row.Cells["IDHewan"].Value.ToString();
                cmbJenisVaksin.Text = row.Cells["JenisVaksin"].Value.ToString();
                cmbKeterangan.Text = row.Cells["Keterangan"].Value.ToString();

                if (row.Cells["TanggalVaksin"].Value != DBNull.Value)
                    dtpTanggalVaksin.Value = Convert.ToDateTime(row.Cells["TanggalVaksin"].Value);
                if (row.Cells["TanggalBerikutnya"].Value != DBNull.Value)
                    dtpTanggalBerikutnya.Value = Convert.ToDateTime(row.Cells["TanggalBerikutnya"].Value);

                cmbStatusVaksin.Text = row.Cells["StatusVaksin"].Value.ToString();
            }
        }

        // FUNGSI INI WAJIB DISAMBUNGKAN KE EVENTS "KeyPress" PADA txtIDHewan
        private void HanyaAngka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}