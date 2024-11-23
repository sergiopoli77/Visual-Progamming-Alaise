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
    public partial class service : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public service()
        {
            alamat = "server=localhost; database=db_alaise; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
            // Add KeyDown event handler for btnsearch (TextBox)
            btnsearch.KeyDown += new KeyEventHandler(btnsearch_KeyDown);
        }



        private void btnsearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Call the search function (simulating the click of lblsearch)
                lblsearch_Click(sender, e);
                // Prevent the "beep" sound on pressing Enter
                e.SuppressKeyPress = true;
            }
        }

        private void lblout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void lbladd_Click(object sender, EventArgs e)
        {
            serviceadd Serviceadd = new serviceadd();
            Serviceadd.Show();
            this.Close();
        }

        private void service_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from tbl_item_service");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "Code";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Item Name";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Category";
                dataGridView1.Columns[3].Width = 140;
                dataGridView1.Columns[3].HeaderText = "Stock";

                


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
                koneksi.Open();
                query = "SELECT * FROM tbl_item_service";
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "Code";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Item Name";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Category";
                dataGridView1.Columns[3].Width = 140;
                dataGridView1.Columns[3].HeaderText = "Stock";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnsearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void lblsearch_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();

                // Mengambil nilai dari btnsearch (TextBox pencarian)
                string searchValue = btnsearch.Text;

                // Mendefinisikan query dengan WHERE dan LIKE untuk pencarian pada beberapa kolom
                query = @"SELECT * FROM tbl_item_service 
                  WHERE id_item_service LIKE @searchValue 
                  OR nama_item LIKE @searchValue 
                  OR kategori LIKE @searchValue 
                  OR stok LIKE @searchValue";

                perintah = new MySqlCommand(query, koneksi);

                // Menambahkan '%' di kedua sisi searchValue untuk pencarian parsial
                perintah.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                adapter = new MySqlDataAdapter(perintah);
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();

                // Mengecek apakah ada data yang ditemukan
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
                    dataGridView1.Columns[2].HeaderText = "Category";
                    dataGridView1.Columns[3].Width = 140;
                    dataGridView1.Columns[3].HeaderText = "Stock";
                }
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

        private void lbldetails_Click(object sender, EventArgs e)
        {
            updateservice updateservicee = new updateservice();
            updateservicee.Show();
            this.Close();
        }

        private void lblkitchen_Click(object sender, EventArgs e)
        {
            kitchen Kitchen = new kitchen();
            Kitchen.Show();
            this.Close();
        }

        private void lblservice_Click(object sender, EventArgs e)
        {
            
        }

        private void lblbar_Click(object sender, EventArgs e)
        {
            bar Bar = new bar();
            Bar.Show();
            this.Close();
        }

        private void lblitemin_Click(object sender, EventArgs e)
        {
            itemin itemIn = new itemin();
            itemIn.Show();
            this.Close();
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

        private void lbldashboard_Click(object sender, EventArgs e)
        {
            dashboard Dashboard = new dashboard();
            Dashboard.Show();
            this.Close();
        }
    }
}
