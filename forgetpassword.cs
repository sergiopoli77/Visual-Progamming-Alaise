using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Vispro_Final_Project___ALAISE
{
    public partial class forgetpassword : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;

        private ToolTip toolTip;

        public forgetpassword()
        {
            alamat = "server=localhost; database=db_alaise; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();

            // Inisialisasi ToolTip
            toolTip = new ToolTip();

            // Atur teks ToolTip
            toolTip.SetToolTip(txtusername, "Enter your username here");
            toolTip.SetToolTip(txtpassword, "Enter your new password here");
        }

        private void forgetpassword_Load(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string newPassword = txtpassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please fill in both fields.");
                return;
            }

            try
            {
                koneksi.Open();
                string query = "SELECT COUNT(*) FROM tbl_admin WHERE username = @username";
                perintah = new MySqlCommand(query, koneksi);
                perintah.Parameters.AddWithValue("@username", username);

                int userExists = Convert.ToInt32(perintah.ExecuteScalar());

                if (userExists > 0)
                {
                    query = "UPDATE tbl_admin SET password = @newPassword WHERE username = @username";
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@newPassword", newPassword);
                    perintah.Parameters.AddWithValue("@username", username);

                    perintah.ExecuteNonQuery();
                    MessageBox.Show("Password changed successfully");

                    this.Close(); 
                    Form1 loginForm = new Form1(); 
                    loginForm.Show();
                }
                else
                {
                    MessageBox.Show("Username not found");
                    txtusername.Clear();
                    txtpassword.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }
    }
}
