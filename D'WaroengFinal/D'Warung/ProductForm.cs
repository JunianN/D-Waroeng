using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace D_Warung
{
    public partial class ProductForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Perkuliahan\Semester 2\Pemrograman Berorientasi Objek\D'Warung\D'Warung\dwarungdb.mdf;Integrated Security=True");

        public ProductForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fillcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Kategori from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Kategori", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "Kategori";
            CatCb.DataSource = dt;
            Con.Close();
        }

        private void fillSearchcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Kategori from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Kategori", typeof(string));
            dt.Load(rdr);
            SearchCb.ValueMember = "Kategori";
            SearchCb.DataSource = dt;
            Con.Close();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
            fillSearchcombo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //This is the implementation of Singleton
            CategoryForm cat = CategoryForm.Instance;

            cat.Show();
            this.Hide();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into ProductTbl values(" + ProdId.Text + " ,'" + ProdName.Text + "' , " + ProdQty.Text + " , " + ProdPrice.Text + " , '" + CatCb.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produk berhasil ditambahkan");
                Con.Close();
                populate();
                ProdId.Text = "";
                ProdName.Text = "";
                ProdPrice.Text = "";
                ProdQty.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProdDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.ProdDGV.Rows[e.RowIndex];

                ProdId.Text = row.Cells["Id"].Value.ToString();
                ProdName.Text = row.Cells["Produk"].Value.ToString();
                ProdQty.Text = row.Cells["Jumlah"].Value.ToString();
                ProdPrice.Text = row.Cells["Harga"].Value.ToString();
                CatCb.SelectedValue = row.Cells["Kategori"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "" || ProdName.Text == "" || ProdPrice.Text == "" || ProdQty.Text == "")
                {
                    MessageBox.Show("Informasi kurang!");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductTbl set Produk = '" + ProdName.Text + "' , Harga = " + ProdPrice.Text + " , Jumlah = " + ProdQty.Text + " , Kategori = '" + CatCb.SelectedValue.ToString() + "' where Id = " + ProdId.Text + " ;";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produk berhasil diubah");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "")
                {
                    MessageBox.Show("Pilih Produk untuk dihapus");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProductTbl where Id=" + ProdId.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produk berhasil dihapus");
                    Con.Close();
                    populate();
                    ProdId.Text = "";
                    ProdName.Text = "";
                    ProdPrice.Text = "";
                    ProdQty.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from ProductTbl where Kategori = '" + SearchCb.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();

            this.Hide();
            prod.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm sell = new SellerForm();

            this.Hide();
            sell.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();

            this.Hide();
            cat.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();

            this.Hide();
            login.Show();
        }
    }
}
