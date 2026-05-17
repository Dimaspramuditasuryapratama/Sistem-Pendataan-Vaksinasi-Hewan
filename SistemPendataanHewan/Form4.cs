using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form4 : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form4()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Password akan tampil sebagai *****
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}