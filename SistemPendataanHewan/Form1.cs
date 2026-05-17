using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            AturDataGridView();

            // Kunci tombol Hapus untuk Petugas
            if (SesiPengguna.RoleUser == "Petugas")
            {
                btnDelete.Visible = false;
            }
        }

        private void AturDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}