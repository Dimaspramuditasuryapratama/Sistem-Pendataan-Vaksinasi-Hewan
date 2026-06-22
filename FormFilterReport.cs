using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class FormFilterReport : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-MBD0B33T\\SHENDY;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtHewan = new DataTable();
        private string selectedJenisHewan = "Semua";
        private bool isDataLoaded = false;

        public FormFilterReport()
        {
            InitializeComponent();
        }

        private void FormFilterReport_Load(object sender, EventArgs e)
        {
            cmbJenisHewan.Items.Clear();
            cmbJenisHewan.Items.Add("Semua");
            cmbJenisHewan.Items.Add("Kucing");
            cmbJenisHewan.Items.Add("Anjing");
            cmbJenisHewan.SelectedIndex = 0;

            dgvPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreview.MultiSelect = false;
            dgvPreview.ReadOnly = true;
            dgvPreview.AllowUserToAddRows = false;
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnCetak.Enabled = false;
            btnCetak.BackColor = System.Drawing.Color.Gray;
            btnCetak.ForeColor = System.Drawing.Color.DarkGray;
            isDataLoaded = false;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                selectedJenisHewan = cmbJenisHewan.SelectedItem.ToString();

                string query = "SELECT * FROM vw_LaporanLengkap";
                if (selectedJenisHewan != "Semua")
                {
                    query += $" WHERE JenisHewan = '{selectedJenisHewan}'";
                }
                query += " ORDER BY IDHewan";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtHewan = new DataTable();
                            da.Fill(dtHewan);
                            dgvPreview.DataSource = dtHewan;

                            if (dtHewan.Rows.Count > 0)
                            {
                                btnCetak.Enabled = true;
                                btnCetak.BackColor = System.Drawing.Color.LightSkyBlue;
                                btnCetak.ForeColor = System.Drawing.Color.Black;
                                isDataLoaded = true;
                                lblTotal.Text = "Total Data: " + dtHewan.Rows.Count;
                            }
                            else
                            {
                                btnCetak.Enabled = false;
                                btnCetak.BackColor = System.Drawing.Color.Gray;
                                btnCetak.ForeColor = System.Drawing.Color.DarkGray;
                                isDataLoaded = false;
                                lblTotal.Text = "Total Data: 0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (!isDataLoaded)
            {
                MessageBox.Show("Silakan klik 'Load' terlebih dahulu untuk memuat data!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtHewan == null || dtHewan.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak!",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string jenisHewan = selectedJenisHewan;
            if (jenisHewan == "Semua")
                jenisHewan = null;

            FormReport frm = new FormReport(jenisHewan);
            frm.Show();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbJenisHewan_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCetak.Enabled = false;
            btnCetak.BackColor = System.Drawing.Color.Gray;
            btnCetak.ForeColor = System.Drawing.Color.DarkGray;
            isDataLoaded = false;
            lblTotal.Text = "Total Data: - (Klik Load)";
        }
    }
}