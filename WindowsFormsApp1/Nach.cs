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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data source =DESKTOP-9FFBKHM; initial catalog =  телемонтаж; integrated security = SSPI");
        private void button1_Click(object sender, EventArgs e)
        {if (MessageBox.Show("вы уверны что хотете зайти","Warning",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            { 
            con.Open();
            SqlCommand sqlCommand = new SqlCommand(@"select Login,Password from Сотрудники where Login='" + textBox2.Text + "' and Password ='" + textBox1.Text + "'", con);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            string a = textBox1.Text;
            string b = textBox2.Text;
            string a1 = textBox1.Text;
            string b1 = textBox2.Text;
                if (a1 == "man1" && b1 == "man1" || a1 == "man2" && b1 == "man2")
                {
                    this.Hide();
                    manager f5 = new manager();
                    f5.Show();
                }
                else
                {
                    if (a == "admin" && b == "admin")
                    {
                        this.Hide();
                        Admin f4 = new Admin();
                        f4.Show();
                    }
                    else
                    {
                        while (dataReader.Read())

                        {
                            string Login = dataReader[0].ToString();
                            string Password = dataReader[1].ToString();

                            if (Login == dataReader[0].ToString() && Password == dataReader[1].ToString())
                            {
                                this.Hide();
                                lichka m1 = new lichka();
                                m1.aaa = textBox1.Text;
                                m1.Show();
                                MessageBox.Show("Вход");
                            }
                            else
                            {
                                MessageBox.Show("Не правильный логин/пароль");
                            }
                        }
                    }
                }
            }
            con.Close();
          
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SostZakaz f2 = new SostZakaz();
            f2.Show();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
