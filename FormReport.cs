using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace SistemPendataanHewan
{
    public partial class FormReport : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private string jenisHewan { get; set; }
        private ReportDocument laporanSaya = new ReportDocument();

        public FormReport(string jenisHewanFilter = null)
        {
            InitializeComponent();
            jenisHewan = jenisHewanFilter;
            this.Text = "Laporan Lengkap Hewan & Vaksinasi";
            LoadReport();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            // Kosong
        }

        private void LoadReport()
        {
            try
            {
                string reportPath = Application.StartupPath + "\\CrystalReport.rpt";

                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show("File laporan tidak ditemukan!\n\n" +
                        "Pastikan file CrystalReport.rpt ada di folder:\n" +
                        Application.StartupPath,
                        "Error File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                laporanSaya.Load(reportPath);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_LaporanLengkap", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (string.IsNullOrEmpty(jenisHewan))
                        cmd.Parameters.AddWithValue("@JenisHewan", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@JenisHewan", jenisHewan);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Tidak ada data untuk dicetak!\n\n" +
                            "Filter yang dipilih: " + (string.IsNullOrEmpty(jenisHewan) ? "Semua" : jenisHewan),
                            "Info",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }

                    laporanSaya.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = laporanSaya;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Koneksi Database!\n\n" +
                    "Detail Error: " + ex.Message + "\n\n" +
                    "Pastikan SQL Server berjalan dan koneksi string benar.",
                    "Error Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan.\n\n" +
                    "Detail Error: " + ex.Message,
                    "Error Laporan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (laporanSaya == null)
                {
                    MessageBox.Show("Tidak ada laporan yang dapat diexport!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Simpan Laporan sebagai PDF";
                saveFileDialog.FileName = "Laporan_Hewan_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportOptions exportOpts = new ExportOptions();
                    PdfRtfWordFormatOptions pdfOpts = new PdfRtfWordFormatOptions();
                    exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
                    exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                    exportOpts.ExportDestinationOptions = new DiskFileDestinationOptions
                    {
                        DiskFileName = saveFileDialog.FileName
                    };
                    exportOpts.ExportFormatOptions = pdfOpts;

                    laporanSaya.Export(exportOpts);
                    MessageBox.Show("Laporan berhasil diexport ke PDF!\n\n" +
                        "Lokasi: " + saveFileDialog.FileName, "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error export PDF: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (laporanSaya == null)
                {
                    MessageBox.Show("Tidak ada laporan yang dapat diexport!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files|*.csv";
                saveFileDialog.Title = "Simpan Laporan sebagai CSV";
                saveFileDialog.FileName = "Laporan_Hewan_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportOptions exportOpts = new ExportOptions();
                    exportOpts.ExportFormatType = ExportFormatType.CharacterSeparatedValues;
                    exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                    exportOpts.ExportDestinationOptions = new DiskFileDestinationOptions
                    {
                        DiskFileName = saveFileDialog.FileName
                    };

                    laporanSaya.Export(exportOpts);
                    MessageBox.Show("Laporan berhasil diexport ke CSV!\n\n" +
                        "Lokasi: " + saveFileDialog.FileName, "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error export CSV: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}