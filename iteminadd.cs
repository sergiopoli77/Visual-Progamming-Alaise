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
    public partial class iteminadd : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public iteminadd()
        {
            alamat = "server=localhost; database=db_alaise; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
            // Menghubungkan event KeyDown ke kontrol txtid
            txtid.KeyDown += new KeyEventHandler(txtid_KeyDown);

            // Mengaktifkan KeyPreview untuk menangkap event KeyDown di form
            this.KeyPreview = true;
        }

        private void iteminadd_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from tbl_itemin");
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


        private void txtid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Menghilangkan suara beep

                try
                {
                    koneksi.Open();
                    string idItem = txtid.Text;
                    bool itemFound = false;

                    // Cek di tbl_item_service
                    query = "SELECT nama_item, kategori FROM tbl_item_service WHERE id_item_service = @id";
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@id", idItem);
                    MySqlDataReader reader = perintah.ExecuteReader();

                    if (reader.Read())
                    {
                        txtnama.Text = reader["nama_item"].ToString();
                        cbkategori.Text = reader["kategori"].ToString();
                        itemFound = true;
                    }
                    reader.Close();

                    // Cek di tbl_kitchen jika belum ditemukan
                    if (!itemFound)
                    {
                        query = "SELECT nama_item, kategori FROM tbl_item_kitchen WHERE id_item_kitchen = @id";
                        perintah = new MySqlCommand(query, koneksi);
                        perintah.Parameters.AddWithValue("@id", idItem);
                        reader = perintah.ExecuteReader();

                        if (reader.Read())
                        {
                            txtnama.Text = reader["nama_item"].ToString();
                            cbkategori.Text = reader["kategori"].ToString();
                            itemFound = true;
                        }
                        reader.Close();
                    }

                    // Cek di tbl_bar jika belum ditemukan
                    if (!itemFound)
                    {
                        query = "SELECT nama_item, kategori FROM tbl_item_bar WHERE id_item_bar = @id";
                        perintah = new MySqlCommand(query, koneksi);
                        perintah.Parameters.AddWithValue("@id", idItem);
                        reader = perintah.ExecuteReader();

                        if (reader.Read())
                        {
                            txtnama.Text = reader["nama_item"].ToString();
                            cbkategori.Text = reader["kategori"].ToString();
                            itemFound = true;
                        }
                        reader.Close();
                    }

                    if (!itemFound)
                    {
                        MessageBox.Show("ID item tidak ditemukan di tabel mana pun.");
                    }

                    koneksi.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
                finally
                {
                    if (koneksi.State == ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                }
            }
        }


        private void lblclose_Click(object sender, EventArgs e)
        {
            itemin itemIn = new itemin();
            itemIn.Show();
            this.Close();
        }

        private void lbladditemin_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();

                // Insert ke tbl_itemin
                string queryInsertItemIn = "INSERT INTO tbl_itemin (id_in, nama_item, jumlah_masuk, tanggal_masuk, kategori) " +
                                            "VALUES (@id_in, @nama_item, @jumlah_masuk, @tanggal_masuk, @kategori)";
                MySqlCommand cmdInsertItemIn = new MySqlCommand(queryInsertItemIn, koneksi);
                cmdInsertItemIn.Parameters.AddWithValue("@id_in", txtid.Text);
                cmdInsertItemIn.Parameters.AddWithValue("@nama_item", txtnama.Text);
                cmdInsertItemIn.Parameters.AddWithValue("@jumlah_masuk", int.Parse(txtjumlah.Text));
                cmdInsertItemIn.Parameters.AddWithValue("@tanggal_masuk", txttanggal.Text);
                cmdInsertItemIn.Parameters.AddWithValue("@kategori", cbkategori.Text);
                cmdInsertItemIn.ExecuteNonQuery();

                // Tentukan tabel kategori berdasarkan pilihan
                string selectedTable = "";
                string idColumn = "";
                switch (cbkategori.Text.ToLower())
                {
                    case "service":
                        selectedTable = "tbl_item_service";
                        idColumn = "id_item_service";
                        break;
                    case "kitchen":
                        selectedTable = "tbl_item_kitchen";
                        idColumn = "id_item_kitchen";
                        break;
                    case "bar":
                        selectedTable = "tbl_item_bar";
                        idColumn = "id_item_bar";
                        break;
                }

                if (!string.IsNullOrEmpty(selectedTable))
                {
                    // Cek apakah item sudah ada di tabel kategori
                    string queryCheck = $"SELECT COUNT(*) FROM {selectedTable} WHERE {idColumn} = @id_item";
                    MySqlCommand cmdCheck = new MySqlCommand(queryCheck, koneksi);
                    cmdCheck.Parameters.AddWithValue("@id_item", txtid.Text);
                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count > 0)
                    {
                        // Update stok jika item sudah ada
                        string queryUpdate = $"UPDATE {selectedTable} SET stok = stok + @jumlah WHERE {idColumn} = @id_item";
                        MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, koneksi);
                        cmdUpdate.Parameters.AddWithValue("@jumlah", int.Parse(txtjumlah.Text));
                        cmdUpdate.Parameters.AddWithValue("@id_item", txtid.Text);
                        cmdUpdate.ExecuteNonQuery();
                    }
                    else
                    {
                        // Insert item baru jika belum ada
                        string queryInsertCategory = $"INSERT INTO {selectedTable} ({idColumn}, nama_item, kategori, stok) " +
                                                     "VALUES (@id_item, @nama_item, @kategori, @stok)";
                        MySqlCommand cmdInsertCategory = new MySqlCommand(queryInsertCategory, koneksi);
                        cmdInsertCategory.Parameters.AddWithValue("@id_item", txtid.Text);
                        cmdInsertCategory.Parameters.AddWithValue("@nama_item", txtnama.Text);
                        cmdInsertCategory.Parameters.AddWithValue("@kategori", cbkategori.Text);
                        cmdInsertCategory.Parameters.AddWithValue("@stok", int.Parse(txtjumlah.Text));
                        cmdInsertCategory.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data berhasil ditambahkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void lbladd_Click(object sender, EventArgs e)
        {

        }

        private void lblitemout_Click(object sender, EventArgs e)
        {

        }

        private void txtkategori_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
