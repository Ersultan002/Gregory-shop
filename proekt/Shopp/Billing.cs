using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopp
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
            populat();
            popula();
            popul();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ersul\Documents\GgroceryDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populat()
        {
            Con.Open();
            string query = "select * from ItemsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder Build = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Item2DGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void popula()
        {
            Con.Open();
            string query = "select * from ItemmTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builde = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Item3DGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void popul()
        {
            Con.Open();
            string query = "select * from IteemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder bilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Item4DGV.DataSource = ds.Tables[0];
            Con.Close();
        }


        int n = 0,GrdTotal=0,Amount;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            
            if (ItQtyTb.Text=="" || Convert.ToInt32(ItQtyTb.Text) > stock||ItNameTb.Text =="")
            {
                MessageBox.Show("Enter Quantily");
            }else
            {
                int total = Convert.ToInt32(ItQtyTb.Text) * Convert.ToInt32(ItPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ItNameTb.Text;
                newRow.Cells[2].Value = ItPriceTb.Text;
                newRow.Cells[3].Value = ItQtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
                Amount = GrdTotal;
                TotalLbl.Text = GrdTotal + " тенге";
                n++;
                UpdateItem();
                Reset();
            }
        }
        private void UpdateItem()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Update ItemTbl set ItQty="+ newQty + " where ItId=" + Key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");
                Con.Close();
                populate();
                //Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateItems()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Update ItemsTbl set ItQty=" + newQty + " where ItId=" + Key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");
                Con.Close();
                populat();
                //Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateItemm()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Update ItemmTbl set ItQty=" + newQty + " where ItId=" + Key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");
                Con.Close();
                populat();
                //Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateIteem()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Update IteemTbl set ItQty=" + newQty + " where ItId=" + Key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");
                Con.Close();
                populat();
                //Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Reset()
        {
            ItPriceTb.Text = "";
            ItQtyTb.Text = "";
            ClientNameTb.Text = "";
            ItNameTb.Text = "";
        }
        private void ResertBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (ClientNameTb.Text == "" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl values('" + EmployeeLbl.Text + "','" + ClientNameTb.Text+ "',"+Amount+")", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Successfully");
                    Con.Close();
                    populate();
                    //Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if(printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            EmployeeLbl.Text = Login.EmployeeName;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Общий " + TotalLbl.Text, new Font("Century Gothic", 12, FontStyle.Regular), Brushes.Blue, new Point(130));
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TotalLbl_Click(object sender, EventArgs e)
        {

        }

        private void Billing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                AddToBillBtn_Click(AddToBillBtn, null);
            }
        }

        private void EmployeeLbl_Click(object sender, EventArgs e)
        {

        }

        private void касса1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void касса2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Billing2 obj = new Billing2();  
            obj.Show();
            this.Hide();
        }

        private void Item2DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = Item2DGV.SelectedRows[0].Cells[1].Value.ToString();

            ItPriceTb.Text = Item2DGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
                Key = 0;
            }
            else
            {
                stock = Convert.ToInt32(Item2DGV.SelectedRows[0].Cells[2].Value.ToString());
                Key = Convert.ToInt32(Item2DGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Item3DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = Item3DGV.SelectedRows[0].Cells[1].Value.ToString();

            ItPriceTb.Text = Item3DGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
                Key = 0;
            }
            else
            {
                stock = Convert.ToInt32(Item3DGV.SelectedRows[0].Cells[2].Value.ToString());
                Key = Convert.ToInt32(Item3DGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Item4DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = Item4DGV.SelectedRows[0].Cells[1].Value.ToString();

            ItPriceTb.Text = Item4DGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
                Key = 0;
            }
            else
            {
                stock = Convert.ToInt32(Item4DGV.SelectedRows[0].Cells[2].Value.ToString());
                Key = Convert.ToInt32(Item4DGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        int stock = 0, Key=0;
        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();
            
            ItPriceTb.Text = ItemDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
                Key = 0;
            }
            else
            {
                stock = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[2].Value.ToString());
                Key = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }
    }
}
