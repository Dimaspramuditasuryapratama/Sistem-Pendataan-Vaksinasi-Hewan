using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form5 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtHewan = new DataTable();
        private BindingSource hewanBindingSource = new BindingSource();

        public Form5()
        {
            InitializeComponent();
        }

        private void ClearForm()
        {
            txtIDHewan.Clear();
            txtIDPemilik.Clear();
            txtNamaHewan.Clear();
            cmbJenisHewan.SelectedIndex = -1;
            cmbRas.SelectedIndex = -1;
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
            cmbJenisHewan.DataBindings.Clear();
            cmbRas.DataBindings.Clear();
            cmbJenisKelamin.DataBindings.Clear();
            txtUmur.DataBindings.Clear();
            txtWarna.DataBindings.Clear();

            txtIDHewan.DataBindings.Add("Text", hewanBindingSource, "IDHewan");
            txtIDPemilik.DataBindings.Add("Text", hewanBindingSource, "IDPemilik");
            txtNamaHewan.DataBindings.Add("Text", hewanBindingSource, "NamaHewan");
            cmbJenisHewan.DataBindings.Add("Text", hewanBindingSource, "JenisHewan");
            cmbRas.DataBindings.Add("Text", hewanBindingSource, "Ras");
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
                    // PERBAIKAN: MEMANGGIL VIEW SECARA LANGSUNG
                    string queryView = "SELECT * FROM vw_DataHewan";
                    using (SqlCommand cmd = new SqlCommand(queryView, conn))
                    {
                        cmd.CommandType = CommandType.Text;
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

        private void Form5_Load(object sender, EventArgs e)
        {
            cmbJenisHewan.Items.Clear();
            cmbJenisHewan.Items.Add("Kucing");
            cmbJenisHewan.Items.Add("Anjing");

            cmbRas.Items.Clear();
            cmbRas.Items.Add("Persia");
            cmbRas.Items.Add("Anggora");
            cmbRas.Items.Add("Maine Coon");
            cmbRas.Items.Add("Bulldog");
            cmbRas.Items.Add("Golden Retriever");
            cmbRas.Items.Add("Husky");

            cmbJenisKelamin.Items.Clear();
            cmbJenisKelamin.Items.Add("L");
            cmbJenisKelamin.Items.Add("P");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadData();
            TerapkanHakAkses();
        }

        private void btnConnect_Click(object sender, EventArgs e) { /*...sama*/ }
        private void btnLoad_Click(object sender, EventArgs e) { LoadData(); }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNamaHewan.Text == "" || txtIDPemilik.Text == "")
                {
                    MessageBox.Show("ID Pemilik dan Nama Hewan harus diisi!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text);
                        cmd.Parameters.AddWithValue("@JenisHewan", cmbJenisHewan.Text);
                        cmd.Parameters.AddWithValue("@Ras", cmbRas.Text);
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
                if (txtIDHewan.Text == "") return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));
                        cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text);
                        cmd.Parameters.AddWithValue("@JenisHewan", cmbJenisHewan.Text);
                        cmd.Parameters.AddWithValue("@Ras", cmbRas.Text);
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
                if (txtIDHewan.Text == "") return;

                if (MessageBox.Show("Yakin ingin menghapus data hewan ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteHewan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));

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
                MessageBox.Show("Gagal menghapus data. " + ex.Message);
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
                cmbJenisHewan.Text = row.Cells["JenisHewan"].Value.ToString();
                cmbRas.Text = row.Cells["Ras"].Value.ToString();
                cmbJenisKelamin.Text = row.Cells["JenisKelamin"].Value.ToString();
                txtUmur.Text = row.Cells["Umur"].Value.ToString();
                txtWarna.Text = row.Cells["Warna"].Value.ToString();
            }
        }

        // FUNGSI INI WAJIB DISAMBUNGKAN KE EVENTS "KeyPress" PADA txtIDPemilik
        private void HanyaAngka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}