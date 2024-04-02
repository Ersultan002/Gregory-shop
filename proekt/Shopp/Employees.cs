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
namespace Shopp
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ersul\Documents\GgroceryDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl values('" + EmpNameTb.Text + "','" + EmpPhoneTb.Text + "','" + EmpAddTb.Text + "','" + EmpPassTb.Text + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpAddTb.Text = "";
            Key = 0;
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }
        int Key = 0;
        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPassTb.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (EmpNameTb.Text == "") 
            {
                Key = 0;
            }else
            {
                Key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Employee To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Delete from EmployeeTbl where EmpId=" + Key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully");
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
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Select The Employee To Be Updated");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Update EmployeeTbl set EmpName='"+EmpNameTb.Text+"',EmpPhone='"+EmpPhoneTb.Text+"',EmpAdd='"+EmpAddTb.Text+"',EmpPass='"+EmpPassTb.Text+"' where EmpId=" + Key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated Successfully");
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

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Items obj = new Items();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Items2 Obj = new Items2();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EmpNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
