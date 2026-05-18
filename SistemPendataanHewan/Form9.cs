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

    }
}