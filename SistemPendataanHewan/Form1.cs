using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtPemilik = new DataTable();
        private BindingSource pemilikBindingSource = new BindingSource();

        public Form1()
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
            BindControls();
            txtIDPemilik.Clear();
            txtNamaPemilik.Clear();
            txtAlamat.Clear();
            txtNoHP.Clear();
            txtRTRW.Clear();
            txtNamaPemilik.Focus();
        }

        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CountPemilik", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter outputParam = new SqlParameter("@Total", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        this.Text = "Form Data Pemilik - Total Data: " + outputParam.Value.ToString();
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
            txtIDPemilik.DataBindings.Clear();
            txtNamaPemilik.DataBindings.Clear();
            txtAlamat.DataBindings.Clear();
            txtNoHP.DataBindings.Clear();
            txtRTRW.DataBindings.Clear();

            txtIDPemilik.DataBindings.Add("Text", pemilikBindingSource, "IDPemilik");
            txtNamaPemilik.DataBindings.Add("Text", pemilikBindingSource, "NamaPemilik");
            txtAlamat.DataBindings.Add("Text", pemilikBindingSource, "Alamat");
            txtNoHP.DataBindings.Add("Text", pemilikBindingSource, "NoHP");
            txtRTRW.DataBindings.Add("Text", pemilikBindingSource, "RTRW");
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // PERBAIKAN: MEMANGGIL VIEW SECARA LANGSUNG
                    string queryView = "SELECT * FROM vw_DataPemilik";
                    using (SqlCommand cmd = new SqlCommand(queryView, conn))
                    {
                        cmd.CommandType = CommandType.Text; // Diubah jadi Text karena query biasa
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtPemilik = new DataTable();
                            da.Fill(dtPemilik);
                            pemilikBindingSource.DataSource = dtPemilik;
                            dataGridView1.DataSource = pemilikBindingSource;
                            bindingNavigator1.BindingSource = pemilikBindingSource;
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
                btnResetData.Visible = false;
                btnTestInjection.Visible = false;
                if (bindingNavigatorDeleteItem != null) bindingNavigatorDeleteItem.Enabled = false;
            }
            else if (SesiPengguna.RoleUser == "Admin")
            {
                btnDelete.Visible = true;
                btnResetData.Visible = true;
                btnTestInjection.Visible = true;
                if (bindingNavigatorDeleteItem != null) bindingNavigatorDeleteItem.Enabled = true;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtIDPemilik.ReadOnly = true;
            LoadData();
            TerapkanHakAkses();
        }

        private void btnConnect_Click(object sender, EventArgs e) { ConnectDatabase(); }
        private void btnLoad_Click(object sender, EventArgs e) { LoadData(); }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNamaPemilik.Text))
                {
                    MessageBox.Show("Nama Pemilik wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNamaPemilik.Focus();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertPemilik", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NamaPemilik", txtNamaPemilik.Text);
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                        cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text);
                        cmd.Parameters.AddWithValue("@RTRW", txtRTRW.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data pemilik berhasil ditambahkan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat insert: " + ex.Message, "Error Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIDPemilik.Text))
                {
                    MessageBox.Show("Pilih data pemilik dari tabel terlebih dahulu!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePemilik", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));
                        cmd.Parameters.AddWithValue("@NamaPemilik", txtNamaPemilik.Text);
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                        cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text);
                        cmd.Parameters.AddWithValue("@RTRW", txtRTRW.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data pemilik berhasil diperbarui");
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
                if (string.IsNullOrWhiteSpace(txtIDPemilik.Text))
                {
                    MessageBox.Show("Pilih data pemilik yang ingin dihapus!");
                    return;
                }

                if (MessageBox.Show("Yakin ingin menghapus data pemilik ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeletePemilik", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));

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
                MessageBox.Show("Gagal menghapus data. Data terikat dengan Hewan.\n\nDetail: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!txtIDPemilik.ReadOnly)
                {
                    txtIDPemilik.ReadOnly = true;
                    BindControls();
                }

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtIDPemilik.Text = row.Cells["IDPemilik"].Value.ToString();
                txtNamaPemilik.Text = row.Cells["NamaPemilik"].Value.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                txtNoHP.Text = row.Cells["NoHP"].Value.ToString();
                txtRTRW.Text = row.Cells["RTRW"].Value.ToString();
            }
        }

        // Fitur Backup dan Test Injection (Tidak ada perubahan)
        private void btnResetData_Click(object sender, EventArgs e)
        {
            // ... (Isi kodingan Reset Data tetap sama seperti punyamu)
        }

        private void btnTestInjection_Click(object sender, EventArgs e)
        {
            // ... (Isi kodingan Test Injection tetap sama seperti punyamu)
        }
    }
}