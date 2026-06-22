using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;

namespace SistemPendataanHewan
{
    public partial class Form5 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtHewan = new DataTable();
        private BindingSource hewanBindingSource = new BindingSource();
        private DataTable dtExcelData = null;
        private byte[] fotoBytes = null;

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
            picFoto.Image = null;
            picFoto.BackColor = System.Drawing.Color.WhiteSmoke;
            fotoBytes = null;
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

        private void TampilkanFotoDiDataGridView()
        {
            if (dataGridView1.Columns.Contains("Foto"))
            {
                DataGridViewImageColumn fotoColumn = (DataGridViewImageColumn)dataGridView1.Columns["Foto"];
                fotoColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                fotoColumn.Width = 80;
                fotoColumn.HeaderText = "Foto";
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM vw_DataHewan ORDER BY IDHewan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
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

                TampilkanFotoDiDataGridView();
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
                if (bindingNavigatorDeleteItem != null)
                    bindingNavigatorDeleteItem.Enabled = false;
            }
            else if (SesiPengguna.RoleUser == "Admin")
            {
                btnDelete.Visible = true;
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
                // Log gagal, abaikan agar tidak mengganggu operasi utama
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormClosed += (s, args) =>
            {
                if (SesiPengguna.IDPengguna > 0)
                {
                    Form8 frm = new Form8();
                    frm.Show();
                }
            };

            dataGridView1.DataError += (s, ev) => { ev.ThrowException = false; };

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

            btnImportDatabase.Enabled = false;

            LoadData();
            TerapkanHakAkses();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Koneksi ke database berhasil!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnUploadFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Pilih Foto Hewan";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(openFileDialog.FileName);
                        picFoto.Image = img;
                        picFoto.SizeMode = PictureBoxSizeMode.StretchImage;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            fotoBytes = ms.ToArray();
                        }

                        MessageBox.Show("Foto berhasil diupload!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal upload foto: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHapusFoto_Click(object sender, EventArgs e)
        {
            picFoto.Image = null;
            picFoto.BackColor = System.Drawing.Color.WhiteSmoke;
            fotoBytes = null;
            MessageBox.Show("Foto berhasil dihapus!", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void picFoto_Click(object sender, EventArgs e)
        {
            btnUploadFoto.PerformClick();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNamaHewan.Text == "" || txtIDPemilik.Text == "")
                {
                    MessageBox.Show("ID Pemilik dan Nama Hewan harus diisi!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        if (fotoBytes != null)
                            cmd.Parameters.AddWithValue("@Foto", fotoBytes);
                        else
                            cmd.Parameters.AddWithValue("@Foto", DBNull.Value);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                SimpanLog("Insert Hewan", "Menambah hewan: " + txtNamaHewan.Text + " (Jenis: " + cmbJenisHewan.Text + ")");

                MessageBox.Show("Data hewan berhasil ditambahkan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat insert: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDHewan.Text == "")
                {
                    MessageBox.Show("Pilih data hewan yang ingin diupdate!", "Peringatan");
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
                        cmd.Parameters.AddWithValue("@JenisHewan", cmbJenisHewan.Text);
                        cmd.Parameters.AddWithValue("@Ras", cmbRas.Text);
                        cmd.Parameters.AddWithValue("@JenisKelamin", cmbJenisKelamin.Text);
                        cmd.Parameters.AddWithValue("@Umur", decimal.Parse(txtUmur.Text));
                        cmd.Parameters.AddWithValue("@Warna", txtWarna.Text);

                        if (fotoBytes != null)
                            cmd.Parameters.AddWithValue("@Foto", fotoBytes);
                        else
                            cmd.Parameters.AddWithValue("@Foto", DBNull.Value);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                SimpanLog("Update Hewan", "Update hewan ID: " + txtIDHewan.Text + " - " + txtNamaHewan.Text);

                MessageBox.Show("Data berhasil diupdate!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat update: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDHewan.Text == "") return;

                if (MessageBox.Show("Yakin ingin menghapus data hewan ini?", "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string namaHewan = txtNamaHewan.Text;
                    string idHewan = txtIDHewan.Text;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteHewan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDHewan", int.Parse(txtIDHewan.Text));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    SimpanLog("Delete Hewan", "Menghapus hewan ID: " + idHewan + " - " + namaHewan);

                    MessageBox.Show("Data berhasil dihapus!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data. " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (row.Cells["Foto"].Value != DBNull.Value && row.Cells["Foto"].Value != null)
                {
                    try
                    {
                        byte[] foto = (byte[])row.Cells["Foto"].Value;
                        using (MemoryStream ms = new MemoryStream(foto))
                        {
                            Image img = Image.FromStream(ms);
                            picFoto.Image = img;
                            picFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                            fotoBytes = foto;
                        }
                    }
                    catch
                    {
                        picFoto.Image = null;
                        picFoto.BackColor = System.Drawing.Color.WhiteSmoke;
                        fotoBytes = null;
                    }
                }
                else
                {
                    picFoto.Image = null;
                    picFoto.BackColor = System.Drawing.Color.WhiteSmoke;
                    fotoBytes = null;
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void HanyaAngka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Pilih File Excel"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;

                        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var config = new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                };

                                var result = reader.AsDataSet(config);

                                if (result.Tables.Count == 0)
                                {
                                    MessageBox.Show("File Excel tidak memiliki sheet data!", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                DataTable dt = result.Tables[0];

                                foreach (DataColumn col in dt.Columns)
                                {
                                    col.ColumnName = col.ColumnName.Trim().Replace(" ", "");
                                }

                                int dataCount = 0;
                                foreach (DataRow row in dt.Rows)
                                {
                                    bool isEmpty = true;
                                    foreach (var item in row.ItemArray)
                                    {
                                        if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                                        {
                                            isEmpty = false;
                                            break;
                                        }
                                    }
                                    if (!isEmpty) dataCount++;
                                }

                                if (dataCount == 0)
                                {
                                    MessageBox.Show("File Excel kosong! Tidak ada data.", "Peringatan",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (!dt.Columns.Contains("IDPemilik") ||
                                    !dt.Columns.Contains("NamaHewan") ||
                                    !dt.Columns.Contains("JenisHewan"))
                                {
                                    MessageBox.Show("Format Excel salah!\n\n" +
                                        "Format yang benar:\n" +
                                        "IDPemilik | NamaHewan | JenisHewan | Ras | JenisKelamin | Umur | Warna",
                                        "Error Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                dtExcelData = dt;
                                dataGridView1.DataSource = dt;

                                btnImportDatabase.Enabled = true;
                                dataGridView1.Enabled = false;
                                btnInsert.Enabled = false;
                                btnUpdate.Enabled = false;
                                btnDelete.Enabled = false;
                                btnLoad.Enabled = false;

                                MessageBox.Show($"Data berhasil di-load dari Excel!\nTotal: {dt.Rows.Count} data\n\n" +
                                    "Klik 'Import to DB' untuk menyimpan ke database.", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang digunakan! Tutup file Excel terlebih dahulu.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImportDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data untuk diimport. Silakan import Excel terlebih dahulu.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirm = MessageBox.Show($"Yakin ingin mengimport {dt.Rows.Count} data ke database?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;

                int sukses = 0;
                int gagal = 0;
                string errorMsg = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            bool isEmptyRow = true;
                            foreach (var item in row.ItemArray)
                            {
                                if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                                {
                                    isEmptyRow = false;
                                    break;
                                }
                            }
                            if (isEmptyRow) continue;

                            int idPemilik = Convert.ToInt32(row["IDPemilik"]);
                            string namaHewan = row["NamaHewan"].ToString().Trim();
                            string jenisHewan = row["JenisHewan"].ToString().Trim();
                            string ras = row["Ras"].ToString().Trim();
                            string jenisKelamin = row["JenisKelamin"].ToString().Trim();
                            decimal umur = Convert.ToDecimal(row["Umur"]);
                            string warna = row["Warna"].ToString().Trim();

                            if (string.IsNullOrEmpty(namaHewan) || string.IsNullOrEmpty(jenisHewan))
                            {
                                gagal++;
                                errorMsg += $"Data tidak lengkap: {namaHewan}\n";
                                continue;
                            }

                            string cekQuery = "SELECT COUNT(*) FROM HewanPeliharaan WHERE NamaHewan = @NamaHewan AND IDPemilik = @IDPemilik";
                            using (SqlCommand cekCmd = new SqlCommand(cekQuery, conn))
                            {
                                cekCmd.Parameters.AddWithValue("@NamaHewan", namaHewan);
                                cekCmd.Parameters.AddWithValue("@IDPemilik", idPemilik);
                                int exists = Convert.ToInt32(cekCmd.ExecuteScalar());
                                if (exists > 0)
                                {
                                    gagal++;
                                    errorMsg += $"Hewan '{namaHewan}' sudah ada untuk Pemilik ID {idPemilik}\n";
                                    continue;
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand("sp_InsertHewan", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@IDPemilik", idPemilik);
                                cmd.Parameters.AddWithValue("@NamaHewan", namaHewan);
                                cmd.Parameters.AddWithValue("@JenisHewan", jenisHewan);
                                cmd.Parameters.AddWithValue("@Ras", ras);
                                cmd.Parameters.AddWithValue("@JenisKelamin", jenisKelamin);
                                cmd.Parameters.AddWithValue("@Umur", umur);
                                cmd.Parameters.AddWithValue("@Warna", warna);
                                cmd.Parameters.AddWithValue("@Foto", DBNull.Value);

                                cmd.ExecuteNonQuery();
                                sukses++;
                            }
                        }
                        catch (Exception ex)
                        {
                            gagal++;
                            errorMsg += $"Error: {ex.Message}\n";
                        }
                    }
                }

                if (sukses > 0)
                {
                    SimpanLog("Import Excel", "Import " + sukses + " data hewan dari Excel");
                }

                MessageBox.Show($"Import selesai!\n\nSukses: {sukses} data\nGagal: {gagal} data" +
                    (string.IsNullOrEmpty(errorMsg) ? "" : $"\n\nDetail Error:\n{errorMsg}"),
                    "Hasil Import", MessageBoxButtons.OK,
                    gagal > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                btnImportDatabase.Enabled = false;
                ClearForm();
                LoadData();
                dataGridView1.Enabled = true;
                btnInsert.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnLoad.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}