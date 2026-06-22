
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form10 : Form
    {
        // Pastikan nama server SQL Server kamu sesuai
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            // Mengatur tampilan tabel agar rapi dan tidak bisa diedit secara manual
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- FUNGSI UTAMA UNTUK MEMANGGIL VIEW ---
        // Fungsi ini dibuat agar kita tidak perlu menulis ulang kodingan koneksi berkali-kali
        private void TampilkanLaporan(string queryView, string namaLaporan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryView, conn))
                    {
                        cmd.CommandType = CommandType.Text; // Menggunakan Text karena langsung memanggil View
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtLaporan = new DataTable();
                            da.Fill(dtLaporan);

                            // Menampilkan data ke tabel
                            dataGridView1.DataSource = dtLaporan;

                            // Opsional: Mengubah judul form agar menampilkan jumlah data
                            this.Text = $"Form Laporan - {namaLaporan} (Total Data: {dtLaporan.Rows.Count})";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENT KLIK TOMBOL ---

        private void btnLaporanPemilik_Click(object sender, EventArgs e)
        {
            // Memanggil View Pemilik
            TampilkanLaporan("SELECT * FROM vw_DataPemilik", "Data Pemilik");
        }

        private void btnLaporanHewan_Click(object sender, EventArgs e)
        {
            // Memanggil View Hewan
            TampilkanLaporan("SELECT * FROM vw_DataHewan", "Data Hewan");
        }

        private void btnLaporanVaksinasi_Click(object sender, EventArgs e)
        {
            // Memanggil View Vaksinasi
            TampilkanLaporan("SELECT * FROM vw_DataVaksinasi", "Data Vaksinasi");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            // Menutup form laporan
            this.Close();
        }

        // Event ini terdaftar di desainermen, kita biarkan kosong karena ini hanya form untuk melihat data
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tidak ada aksi yang diperlukan saat baris diklik di menu laporan
        }
    }
}