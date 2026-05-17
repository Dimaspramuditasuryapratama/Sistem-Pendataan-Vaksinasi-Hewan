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

    }
}