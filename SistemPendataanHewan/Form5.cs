using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form5 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        private DataTable dtHewan = new DataTable();
        private BindingSource hewanBindingSource = new BindingSource();

        public Form5()
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
            txtIDHewan.Clear();
            txtIDPemilik.Clear();
            txtNamaHewan.Clear();
            txtJenisHewan.Clear();
            txtRas.Clear();
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
            txtJenisHewan.DataBindings.Clear();
            txtRas.DataBindings.Clear();
            cmbJenisKelamin.DataBindings.Clear();
            txtUmur.DataBindings.Clear();
            txtWarna.DataBindings.Clear();

            txtIDHewan.DataBindings.Add("Text", hewanBindingSource, "IDHewan");
            txtIDPemilik.DataBindings.Add("Text", hewanBindingSource, "IDPemilik");
            txtNamaHewan.DataBindings.Add("Text", hewanBindingSource, "NamaHewan");
            txtJenisHewan.DataBindings.Add("Text", hewanBindingSource, "JenisHewan");
            txtRas.DataBindings.Add("Text", hewanBindingSource, "Ras");
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
                    using (SqlCommand cmd = new SqlCommand("sp_GetHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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

        private void Form5_Load(object sender, EventArgs e)
        {
            cmbJenisKelamin.Items.Clear();
            cmbJenisKelamin.Items.Add("L");
            cmbJenisKelamin.Items.Add("P");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadData();
        }
    }
}