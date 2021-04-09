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
    public partial class SellingForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\Documents\dwarungdb.mdf;Integrated Security=True;Connect Timeout=30");
        int Grdtotal = 0, n = 0;

        public SellingForm()
        {
            InitializeComponent();
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
            SearchCb.ValueMember = "Kategori";
            SearchCb.DataSource = dt;
            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string query = "select Produk, Jumlah, Harga from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populatebills()
        {
            Con.Open();
            string query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void restart()
        {
            string query = "delete from Tbl where Id = " + BillId.Text + " ";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
        }
        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            fillcombo();
            SellerNamelbl.Text = Form1.Sellername;
        }

        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.ProdDGV1.Rows[e.RowIndex];

                ProdName.Text = row.Cells["Produk"].Value.ToString();
                ProdPrice.Text = row.Cells["Harga"].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Panelbase_Paint(object sender, PaintEventArgs e)
        {
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.BillDGV.Rows[e.RowIndex];

                BillId.Text = row.Cells["Id"].Value.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void SearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select Produk, Jumlah, Harga from ProductTbl where Kategori = '" + SearchCb.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BillId.Text == "")
            {
                MessageBox.Show("Bill ID masih kosong!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTbl values(" + BillId.Text + " ,'" + SellerNamelbl.Text + "' , '" + Datelbl.Text + "' , " + Amtlbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penjualan berhasil ditambahkan");
                    
                    Con.Close();
                    populatebills();
                    BillId.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();

            this.Hide();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (BillId.Text == "")
                {
                    MessageBox.Show("Pilih Data Penjualan untuk dihapus");
                }
                else
                {
                    Con.Open();
                    string query = "delete from BillTbl where Id=" + BillId.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data penjualan berhasil dihapus");
                    Con.Close();
                    populatebills();
                    BillId.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OrderDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ProdName.Text == "" || ProdQty.Text == "")
            {
                MessageBox.Show("Data tidak lengkap!");
            }
            else
            {
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                Grdtotal += total;

                newRow.CreateCells(OrderDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdName.Text;
                newRow.Cells[2].Value = ProdPrice.Text;
                newRow.Cells[3].Value = ProdQty.Text;
                newRow.Cells[4].Value = total;
                OrderDGV.Rows.Add(newRow);
                n++;
                Amtlbl.Text = "" + Grdtotal;
                ProdName.Text = "";
                ProdPrice.Text = "";
                ProdQty.Text = "";
            }
        }
    }
}
