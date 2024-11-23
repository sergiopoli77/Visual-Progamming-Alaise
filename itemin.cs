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
    public partial class itemin : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;

        private void itemin_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection koneksi = new MySqlConnection(alamat))
                {
                    koneksi.Open();
                    query = "SELECT * FROM tbl_itemin";
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    ds.Clear();
                    adapter.Fill(ds);
                }

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "Code";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Item Name";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Incoming amount";
                dataGridView1.Columns[3].Width = 140;
                dataGridView1.Columns[3].HeaderText = "Date of entry";
                dataGridView1.Columns[4].Width = 140;
                dataGridView1.Columns[4].HeaderText = "Category";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnsearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Call the search function (simulating the click of lblsearch)
                lblsearch_Click_1(sender, e);
                // Prevent the "beep" sound on pressing Enter
                e.SuppressKeyPress = true;
            }
        }

        private void lbldashboard_Click(object sender, EventArgs e)
        {
            dashboard Dashboard = new dashboard();
            Dashboard.Show();
            this.Close();
        }

        private void lbladd_Click(object sender, EventArgs e)
        {
            iteminadd iteminAdd = new iteminadd();
            iteminAdd.Show();
            this.Close();
        }

        private void lblservice_Click(object sender, EventArgs e)
        {
            service Service= new service();
            Service.Show();
            this.Close();
        }

        private void lblkitchen_Click(object sender, EventArgs e)
        {
            kitchen Kitchen= new kitchen();
            Kitchen.Show();
            this.Close();
        }

        private void lblbar_Click(object sender, EventArgs e)
        {
            bar Bar= new bar();
            Bar.Show();
            this.Close();
        }

        private void lblout_Click(object sender, EventArgs e)
        {
            Form1 form= new Form1();
            form.Show();
            this.Close();
        }

        private void lblsearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection koneksi = new MySqlConnection(alamat))
                {
                    koneksi.Open();
                    string searchValue = btnsearch.Text;

                    query = @"SELECT * FROM tbl_itemin
                      WHERE id_in LIKE @searchValue 
                      OR nama_item LIKE @searchValue 
                      OR jumlah_masuk LIKE @searchValue 
                      OR tanggal_masuk LIKE @searchValue
                      OR Kategori LIKE @searchValue";

                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                    adapter = new MySqlDataAdapter(perintah);
                    ds.Clear();
                    adapter.Fill(ds);
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Data not found");
                }
                else
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[0].HeaderText = "Code";
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[1].HeaderText = "Item Name";
                    dataGridView1.Columns[2].Width = 120;
                    dataGridView1.Columns[2].HeaderText = "Incoming amount";
                    dataGridView1.Columns[3].Width = 140;
                    dataGridView1.Columns[3].HeaderText = "Date of entry";
                    dataGridView1.Columns[4].Width = 140;
                    dataGridView1.Columns[4].HeaderText = "Category";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection koneksi = new MySqlConnection(alamat))
                {
                    koneksi.Open();
                    query = "SELECT * FROM tbl_itemin";
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    ds.Clear();
                    adapter.Fill(ds);
                }

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "Code";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Item Name";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Incoming amount";
                dataGridView1.Columns[3].Width = 140;
                dataGridView1.Columns[3].HeaderText = "Date of entry";
                dataGridView1.Columns[4].Width = 140;
                dataGridView1.Columns[4].HeaderText = "Category";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblclear_Click(object sender, EventArgs e)
        {
            btnsearch.Text = string.Empty;
            LoadData();
        }

        private void lblitemout_Click(object sender, EventArgs e)
        {
            itemout itemOut = new itemout();
            itemOut.Show();
            this.Close();
        }

        private void lblsetting_Click(object sender, EventArgs e)
        {
            usersettings userSettings = new usersettings();
            userSettings.Show();
            this.Close();
        }

        private void lblclear_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection koneksi = new MySqlConnection(alamat))
                {
                    koneksi.Open();
                    string searchValue = btnsearch.Text;

                    query = @"SELECT * FROM tbl_itemin
                      WHERE id_in LIKE @searchValue 
                      OR nama_item LIKE @searchValue 
                      OR jumlah_masuk LIKE @searchValue 
                      OR tanggal_masuk LIKE @searchValue
                      OR Kategori LIKE @searchValue";

                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                    adapter = new MySqlDataAdapter(perintah);
                    ds.Clear();
                    adapter.Fill(ds);
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Data not found");
                }
                else
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[0].HeaderText = "Code";
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[1].HeaderText = "Item Name";
                    dataGridView1.Columns[2].Width = 120;
                    dataGridView1.Columns[2].HeaderText = "Incoming amount";
                    dataGridView1.Columns[3].Width = 140;
                    dataGridView1.Columns[3].HeaderText = "Date of entry";
                    dataGridView1.Columns[4].Width = 140;
                    dataGridView1.Columns[4].HeaderText = "Category";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblsearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection koneksi = new MySqlConnection(alamat))
                {
                    koneksi.Open();
                    string searchValue = btnsearch.Text;

                    query = @"SELECT * FROM tbl_itemin
                      WHERE id_in LIKE @searchValue 
                      OR nama_item LIKE @searchValue 
                      OR jumlah_masuk LIKE @searchValue 
                      OR tanggal_masuk LIKE @searchValue
                      OR Kategori LIKE @searchValue";

                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                    adapter = new MySqlDataAdapter(perintah);
                    ds.Clear();
                    adapter.Fill(ds);
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Data not found");
                }
                else
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[0].HeaderText = "Code";
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[1].HeaderText = "Item Name";
                    dataGridView1.Columns[2].Width = 120;
                    dataGridView1.Columns[2].HeaderText = "Incoming amount";
                    dataGridView1.Columns[3].Width = 140;
                    dataGridView1.Columns[3].HeaderText = "Date of entry";
                    dataGridView1.Columns[4].Width = 140;
                    dataGridView1.Columns[4].HeaderText = "Category";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public itemin()
        {
            alamat = "server=localhost; database=db_alaise; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
            // Add KeyDown event handler for btnsearch (TextBox)
            btnsearch.KeyDown += new KeyEventHandler(btnsearch_KeyDown);

        }
    }
}
