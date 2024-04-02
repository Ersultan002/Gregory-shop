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
    public partial class Items2 : Form
    {
        public Items2()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ersul\Documents\GgroceryDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemmDGV.DataSource = ds.Tables[0];
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

        private void label1_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();    
            Obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Items3 Obj = new Items3();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Items4 Obj = new Items4();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
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
                    SqlCommand cmd = new SqlCommand("insert into ItemsTbl values('" + ItNameTb.Text + "'," + ItQtyTb.Text + "," + PriceTb.Text + ",'" + CatCb.SelectedItem.ToString() + "')", Con);
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
                    string query = "Update ItemsTbl set ItName='" + ItNameTb.Text + "',ItQty='" + ItQtyTb.Text + "',ItPrice='" + PriceTb.Text + "',ItCat='" + CatCb.SelectedItem.ToString() + "' where ItId=" + Key + ";";
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
                    string query = "Delete from ItemsTbl where ItId=" + Key + ";";
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
        int Key = 0;
        private void ItemmDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemmDGV.SelectedRows[0].Cells[1].Value.ToString();
            ItQtyTb.Text = ItemmDGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ItemmDGV.SelectedRows[0].Cells[3].Value.ToString();
            CatCb.SelectedItem = ItemmDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ItNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ItemmDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
    
}
