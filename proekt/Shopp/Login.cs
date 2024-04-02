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
using System.Xml;

namespace Shopp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string EmployeeName = "";
        public static string EmployeesName = "";
        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ersul\Documents\GgroceryDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from EmployeeTbl where EmpName='" + UNameTb.Text + "' and EmpPass='" + PasswordTb.Text + "'", Con);
            new SqlDataAdapter("Select count(*) from EmployeesTbl where EmppName='" + UNameTb.Text + "' and EmppPass='" + PasswordTb.Text + "'", Con);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                EmployeeName = UNameTb.Text;
                Billing Obj = new Billing();
                Obj.Show();
                this.Hide();
                Con.Close();
            }

            else
            {
                MessageBox.Show("Wrong UserName Or Password");
            }
            Con.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                button1_Click(button1, null);
            }
        }
    }
}
