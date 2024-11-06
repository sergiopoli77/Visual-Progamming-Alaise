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
    public partial class updateservice : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public updateservice()
        {
            alamat = "server=localhost; database=db_alaise; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();

            // Tambahkan event handler KeyDown ke txtid
            txtid.KeyDown += new KeyEventHandler(txtid_KeyDown);
        }

        private void lblclose_Click(object sender, EventArgs e)
        {
            service Service = new service();
            Service.Show();
            this.Close();
        }

        private void lblcari_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();

                // Mendapatkan nilai id dari txtid (textbox yang digunakan untuk pencarian)
                string id = txtid.Text;

                // Query untuk mencari data berdasarkan id_item_service
                query = "SELECT * FROM tbl_item_service WHERE id_item_service = @id_item_service";
                perintah = new MySqlCommand(query, koneksi);
                perintah.Parameters.AddWithValue("@id_item_service", id);

                MySqlDataReader reader = perintah.ExecuteReader();

                if (reader.Read())
                {
                    // Jika data ditemukan, tampilkan di textbox
                    txtid.Text = reader["id_item_service"].ToString();
                    txtnama.Text = reader["nama_item"].ToString();
                    lblkategori.Text = reader["kategori"].ToString();
                    txtstok.Text = reader["stok"].ToString();
                }
                else
                {
                    // Jika data tidak ditemukan, tampilkan pesan
                    MessageBox.Show("Data not found");
                    // Kosongkan semua textbox untuk memastikan tidak ada sisa data sebelumnya
                    txtid.Clear();
                    txtnama.Clear();
                    lblkategori.Clear();
                    txtstok.Clear();
                }

                reader.Close();
                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblhapus_Click(object sender, EventArgs e)
        {

        }

        private void lblupdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Mendapatkan nilai id dan data baru dari textbox
                string id = txtid.Text;
                string namaItem = txtnama.Text;
                string kategori = lblkategori.Text; // Kategori ditampilkan sebagai label
                string stok = txtstok.Text;

                // Validasi input
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(namaItem) || string.IsNullOrEmpty(kategori) || string.IsNullOrEmpty(stok))
                {
                    MessageBox.Show("Please fill all fields before updating.");
                    return;
                }

                // Mengonfirmasi apakah pengguna benar-benar ingin mengupdate data
                DialogResult result = MessageBox.Show("Are you sure you want to update this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    koneksi.Open();

                    // Query untuk mengupdate data berdasarkan id_item_service
                    query = "UPDATE tbl_item_service SET nama_item = @nama_item, kategori = @kategori, stok = @stok WHERE id_item_service = @id_item_service";
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@nama_item", namaItem);
                    perintah.Parameters.AddWithValue("@kategori", kategori);
                    perintah.Parameters.AddWithValue("@stok", stok);
                    perintah.Parameters.AddWithValue("@id_item_service", id);

                    int rowsAffected = perintah.ExecuteNonQuery();
                    koneksi.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data has been successfully updated.");

                        // Memperbarui data grid untuk menampilkan data yang sudah diperbarui
                        updateservice_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update data. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lbldelet_Click(object sender, EventArgs e)
        {

        }

        private void txtid_KeyDown(object sender, KeyEventArgs e)
        {
            // Cek apakah tombol yang ditekan adalah Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Panggil langsung fungsi pencarian
                lblcari_Click(sender, e);
                // Mencegah bunyi beep (opsional)
                e.SuppressKeyPress = true;
            }
        }

        private void lbldelet_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Mendapatkan nilai id dari txtid (textbox yang berisi id_item_service untuk dihapus)
                string id = txtid.Text;

                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Please search for an item first before deleting.");
                    return;
                }

                // Konfirmasi sebelum menghapus data
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    koneksi.Open();

                    // Query untuk menghapus data berdasarkan id_item_service
                    query = "DELETE FROM tbl_item_service WHERE id_item_service = @id_item_service";
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@id_item_service", id);

                    int rowsAffected = perintah.ExecuteNonQuery();
                    koneksi.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data has been successfully deleted.");

                        // Kosongkan TextBox setelah data dihapus
                        txtid.Clear();
                        txtnama.Clear();
                        lblkategori.Text = string.Empty;
                        txtstok.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete data. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void updateservice_Load(object sender, EventArgs e)
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
    }
}
