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
                    MessageBox.Show("Koneksi ke DBHewanPeliharaanADO berhasil!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "SELECT * FROM vw_DataPemilik ORDER BY IDPemilik";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
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
                if (bindingNavigatorDeleteItem != null)
                    bindingNavigatorDeleteItem.Enabled = false;
            }
            else if (SesiPengguna.RoleUser == "Admin")
            {
                btnDelete.Visible = true;
                btnResetData.Visible = true;
                btnTestInjection.Visible = true;
                if (bindingNavigatorDeleteItem != null)
                    bindingNavigatorDeleteItem.Enabled = true;
            }
        }

        private void SimpanLog(string aktivitas, string detail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertLaporan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPengguna", SesiPengguna.IDPengguna);
                        cmd.Parameters.AddWithValue("@JenisLaporan", aktivitas);
                        cmd.Parameters.AddWithValue("@IsiLaporan", detail);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Log gagal, abaikan
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            this.FormClosed += (s, args) =>
            {
                if (SesiPengguna.IDPengguna > 0)
                {
                    Form8 frm = new Form8();
                    frm.Show();
                }
            };

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtIDPemilik.ReadOnly = true;
            LoadData();
            TerapkanHakAkses();
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
                if (string.IsNullOrWhiteSpace(txtNamaPemilik.Text))
                {
                    MessageBox.Show("Nama Pemilik wajib diisi!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    }
                }

                SimpanLog("Insert Pemilik", "Menambah pemilik: " + txtNamaPemilik.Text + " - No HP: " + txtNoHP.Text);

                MessageBox.Show("Data pemilik berhasil ditambahkan", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat insert: " + ex.Message, "Error Insert",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    }
                }

                SimpanLog("Update Pemilik", "Update pemilik ID: " + txtIDPemilik.Text);

                MessageBox.Show("Data pemilik berhasil diperbarui", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
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

                if (MessageBox.Show("Yakin ingin menghapus data pemilik ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string namaPemilik = txtNamaPemilik.Text;
                    string idPemilik = txtIDPemilik.Text;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeletePemilik", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    SimpanLog("Delete Pemilik", "Menghapus pemilik ID: " + idPemilik + " - " + namaPemilik);

                    MessageBox.Show("Data berhasil dihapus");
                    ClearForm();
                    LoadData();
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
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtIDPemilik.Text = row.Cells["IDPemilik"].Value.ToString();
                txtNamaPemilik.Text = row.Cells["NamaPemilik"].Value.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                txtNoHP.Text = row.Cells["NoHP"].Value.ToString();
                txtRTRW.Text = row.Cells["RTRW"].Value.ToString();
            }
        }

        private void btnResetData_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirm = MessageBox.Show(
                    "Yakin ingin mereset data Pemilik ke backup awal?\n\n" +
                    "Data yang sudah diubah akan hilang!",
                    "Konfirmasi Reset",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM Pemilik", conn);
                        cmdDelete.ExecuteNonQuery();

                        SqlCommand cmdReset = new SqlCommand(
                            "DBCC CHECKIDENT ('Pemilik', RESEED, 0)", conn);
                        cmdReset.ExecuteNonQuery();

                        SqlCommand cmdRestore = new SqlCommand(
                            "INSERT INTO Pemilik SELECT * FROM Pemilik_Backup", conn);
                        cmdRestore.ExecuteNonQuery();
                    }

                    SimpanLog("Reset Data Pemilik", "Reset data pemilik ke backup awal");

                    MessageBox.Show("Data Pemilik berhasil direset!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal reset data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestInjection_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Test Injection: Aplikasi sudah aman dari SQL Injection\n" +
                "karena menggunakan Stored Procedure!", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}