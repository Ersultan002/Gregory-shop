using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopp
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(PasswordTb.Text == "1111")
            {
                MessageBox.Show("Enter Password");
            }else if(PasswordTb.Text =="Pass")
            {
                Employees Emp = new Employees();
                Emp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password");
            }
        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                button1_Click(button1, null);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
