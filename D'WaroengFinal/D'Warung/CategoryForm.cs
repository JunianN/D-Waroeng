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
    public partial class CategoryForm : Form
    {
        //This is my Singleton
        private static CategoryForm instance = null;

        public static CategoryForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryForm();
                }
                return instance;
            }
        }

        public CategoryForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Perkuliahan\Semester 2\Pemrograman Berorientasi Objek\D'Warung\D'Warung\dwarungdb.mdf;Integrated Security=True");
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into CategoryTbl values(" + CatIdTb.Text + " ,'" + CatNameTb.Text + "' , '" + CatDescTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kategori berhasil ditambahkan");
                Con.Close();
                populate();
                CatIdTb.Text = "";
                CatNameTb.Text = "";
                CatDescTb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query , Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatGDV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CatGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.CatGDV.Rows[e.RowIndex];

                CatIdTb.Text = row.Cells["Id"].Value.ToString();
                CatNameTb.Text = row.Cells["Kategori"].Value.ToString();
                CatDescTb.Text = row.Cells["Deskripsi"].Value.ToString();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "")
                {
                    MessageBox.Show("Pilih Kategori untuk dihapus");
                }
                else
                {
                    Con.Open();
                    string query = "delete from CategoryTbl where Id=" + CatIdTb.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kategori berhasil dihapus");
                    Con.Close();
                    populate();
                    CatIdTb.Text = "";
                    CatNameTb.Text = "";
                    CatDescTb.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Informasi kurang!");
                }
                else
                {
                    Con.Open();
                    string query = "update CategoryTbl set Kategori = '" + CatNameTb.Text + "' , Deskripsi = '" + CatDescTb.Text + "' where Id = " + CatIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kategori berhasil diubah");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();

            prod.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();

            this.Hide();
            prod.Show();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
