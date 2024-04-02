using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopp
{
    public partial class Items4 : Form
    {
        public Items4()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ersul\Documents\GgroceryDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from IteemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            ItNameTb.Text = "";
            ItQtyTb.Text = "";
            PriceTb.Text = "";
            CatCb.SelectedIndex = -1;
            Key = 0;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Items obj = new Items();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Items2 obj = new Items2();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Items3 obj = new Items3();
            obj.Show();
            this.Hide();
        }
        int Key = 0;    
        private void ItemsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemsDGV.SelectedRows[0].Cells[1].Value.ToString();
            ItQtyTb.Text = ItemsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ItemsDGV.SelectedRows[0].Cells[3].Value.ToString();
            CatCb.SelectedItem = ItemsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ItNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ItemsDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into IteemTbl values('" + ItNameTb.Text + "'," + ItQtyTb.Text + "," + PriceTb.Text + ",'" + CatCb.SelectedItem.ToString() + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Saved Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select The Item To Be Updated");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Update IteemTbl set ItName='" + ItNameTb.Text + "',ItQty='" + ItQtyTb.Text + "',ItPrice='" + PriceTb.Text + "',ItCat='" + CatCb.SelectedItem.ToString() + "' where ItId=" + Key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Updated Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Item To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Delete from IteemTbl where ItId=" + Key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Deleted Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
