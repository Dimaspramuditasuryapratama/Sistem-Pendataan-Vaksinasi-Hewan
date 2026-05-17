using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

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
            // Mengembalikan Binding yang sempat diputus saat Test Injection
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
                    using (SqlCommand cmd = new SqlCommand("sp_GetPemilik", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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

        // COCOK: Menggunakan nama Form1_Load_1 sesuai baris terakhir di Form1.Designer.cs kamu
        private void Form1_Load_1(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtIDPemilik.ReadOnly = true; // Default terkunci agar input normal aman
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

                        // Menggunakan parameter @NamaPemilik agar serasi dengan modifikasi database
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
                    MessageBox.Show("Pilih data pemilik dari tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        MessageBox.Show("Data pemilik berhasil diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat update: " + ex.Message, "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIDPemilik.Text))
                {
                    MessageBox.Show("Pilih data pemilik yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult resultConfirm = MessageBox.Show(
                    "Yakin ingin menghapus data pemilik ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultConfirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeletePemilik", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDPemilik", int.Parse(txtIDPemilik.Text));

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data berhasil dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearForm();
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data tidak ditemukan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data. Data ini kemungkinan masih terikat dengan data Hewan.\n\nDetail: " + ex.Message, "Error Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Jika sedang dalam mode simulasi, kembalikan kontrol agar sinkron kembali saat tabel diklik
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

        private void btnResetData_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultConfirm = MessageBox.Show(
                    "Yakin ingin memulihkan seluruh data Pemilik dari tabel backup?",
                    "Konfirmasi Reset",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultConfirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = @"
                            IF OBJECT_ID('dbo.Pemilik_Backup') IS NOT NULL
                            BEGIN
                                UPDATE P
                                SET P.NamaPemilik = B.NamaPemilik,
                                    P.Alamat = B.Alamat,
                                    P.NoHP = B.NoHP,
                                    P.RTRW = B.RTRW
                                FROM dbo.Pemilik P
                                INNER JOIN dbo.Pemilik_Backup B ON P.IDPemilik = B.IDPemilik;
                            END";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Data Pemilik berhasil dikembalikan ke kondisi semula!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    txtIDPemilik.ReadOnly = true;
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reset data gagal: " + ex.Message, "Error Reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestInjection_Click(object sender, EventArgs e)
        {
            try
            {
                // JALAN TIKUS DEMO: Jika TextBox ID terdeteksi berisi angka normal (bukan payload)
                // Kita putus binding data, bersihkan kotaknya, lalu minta user ketik payload.
                if (!txtIDPemilik.Text.Contains("'") && !txtIDPemilik.Text.ToLower().Contains("or"))
                {
                    txtIDPemilik.DataBindings.Clear();

                    txtIDPemilik.Clear();
                    txtIDPemilik.ReadOnly = false;
                    txtIDPemilik.Focus();

                    MessageBox.Show("TextBox ID Pemilik sekarang telah DIKOSONGKAN dan DIBUKA!\n\nSilakan ketikkan payload injection Anda sekarang:\n' OR 1=1 --\n\nSetelah diketik, klik tombol Test Injection ini sekali lagi.", "Mode Demo Aktif", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Mengatasi error Identity Insert pada tabel Backup
                    string fixBackupQuery = @"
                        IF OBJECT_ID('dbo.Pemilik_Backup') IS NOT NULL DROP TABLE dbo.Pemilik_Backup;
                        SELECT * INTO dbo.Pemilik_Backup FROM dbo.Pemilik;";

                    using (SqlCommand cmdBackup = new SqlCommand(fixBackupQuery, conn))
                    {
                        cmdBackup.ExecuteNonQuery();
                    }

                    // EKSEKUSI QUERY STR CONCATENATION (RENTAN)
                    string query =
                        "UPDATE Pemilik SET NamaPemilik='HACKED' WHERE IDPemilik='" + txtIDPemilik.Text + "'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected + " baris data pemilik berhasil di-HACK dan terupdate!", "Kerentanan Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                txtIDPemilik.ReadOnly = true;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test injection gagal: " + ex.Message, "Error Injection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}