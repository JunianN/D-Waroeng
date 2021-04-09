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
    public partial class SellerForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\Documents\dwarungdb.mdf;Integrated Security=True;Connect Timeout=30");
        public SellerForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.SDGV.Rows[e.RowIndex];

                SId.Text = row.Cells["Id"].Value.ToString();
                SName.Text = row.Cells["Nama"].Value.ToString();
                SPhone.Text = row.Cells["NoHP"].Value.ToString();
                SPass.Text = row.Cells["Password"].Value.ToString();
                SAge.Text = row.Cells["Umur"].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into SellerTbl values(" + SId.Text + " ,'" + SName.Text + "' , " + SPhone.Text + " , '" + SPass.Text + "' , " + SAge.Text + ")";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Penjual berhasil ditambahkan");
                Con.Close();
                populate();
                SId.Text = "";
                SName.Text = "";
                SPhone.Text = "";
                SPass.Text = "";
                SAge.Text = "";
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
                if (SId.Text == "" || SName.Text == "" || SPhone.Text == "" || SPass.Text == "" || SAge.Text == "")
                {
                    MessageBox.Show("Informasi kurang!");
                }
                else
                {
                    Con.Open();
                    string query = "update SellerTbl set Nama = '" + SName.Text + "' , NoHP = " + SPhone.Text + " , Password = '" + SPass.Text + "' , Umur = " + SAge.Text + " where Id = " + SId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penjual berhasil diubah");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "")
                {
                    MessageBox.Show("Pilih Penjual untuk dihapus");
                }
                else
                {
                    Con.Open();
                    string query = "delete from SellerTbl where Id=" + SId.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penjual berhasil dihapus");
                    Con.Close();
                    populate();
                    SId.Text = "";
                    SName.Text = "";
                    SPhone.Text = "";
                    SPass.Text = "";
                    SAge.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void button2_Click(object sender, EventArgs e)
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
    }
}
