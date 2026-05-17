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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasi Input
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // MENGAMBIL DATA ROLE DARI DATABASE
                string query = @"SELECT IDPengguna, NamaPengguna, RoleUser 
                 FROM Pengguna 
                 WHERE Username = @Username AND PasswordHash = @PasswordHash";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Jika username & password cocok
                        {
                            // Simpan data login ke Class SesiPengguna
                            SesiPengguna.IDPengguna = Convert.ToInt32(reader["IDPengguna"]);
                            SesiPengguna.NamaPengguna = reader["NamaPengguna"].ToString();
                            SesiPengguna.RoleUser = reader["RoleUser"].ToString();

                            MessageBox.Show("Login berhasil!\nSelamat datang, " + SesiPengguna.NamaPengguna + " (" + SesiPengguna.RoleUser + ")",
                                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Buka Menu Utama
                            Form8 frm = new Form8();
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username atau password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Tutup koneksi
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            // Konfirmasi keluar aplikasi
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

 
private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Kosongkan password jika username berubah (opsional)
            // txtPassword.Clear();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Event ini bisa dibiarkan kosong
        }
    }
}