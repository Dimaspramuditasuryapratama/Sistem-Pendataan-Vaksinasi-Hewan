using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemPendataanHewan
{
    public partial class Form4 : Form
    {
        private readonly SqlConnection conn;
        // Pastikan Data Source sudah sesuai dengan nama Server SQL kamu
        private readonly string connectionString =
            "Data Source=LAPTOP-2QET043V\\DIMAS;Initial Catalog=DBHewanPeliharaanADO;Integrated Security=True";

        public Form4()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Password akan tampil sebagai bintang-bintang (*****)
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validasi Input Kosong
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

                // =====================================================================
                // 2. SOLUSI ANTI SQL INJECTION: Menggunakan Parameterized Query
                // =====================================================================
                string query = @"SELECT IDPengguna, NamaPengguna, RoleUser 
                                 FROM Pengguna 
                                 WHERE Username = @Username AND PasswordHash = @PasswordHash";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Nilai dikirim via parameter, sehingga karakter injeksi tidak akan tereksekusi
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Jika username & password ditemukan di database
                        {
                            // Simpan data login ke Class SesiPengguna (Pastikan class ini sudah kamu buat)
                            SesiPengguna.IDPengguna = Convert.ToInt32(reader["IDPengguna"]);
                            SesiPengguna.NamaPengguna = reader["NamaPengguna"].ToString();
                            SesiPengguna.RoleUser = reader["RoleUser"].ToString();

                            MessageBox.Show("Login berhasil!\nSelamat datang, " + SesiPengguna.NamaPengguna + " (" + SesiPengguna.RoleUser + ")",
                                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Buka Menu Utama (Form8)
                            Form8 frm = new Form8();
                            frm.Show();

                            // Sembunyikan form login
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
                MessageBox.Show("Terjadi kesalahan saat login: " + ex.Message, "Error Database",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Pastikan koneksi selalu tertutup apa pun yang terjadi
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}