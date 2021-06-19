using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //подключение библиотеки БД
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class SostZakaz : Form
    {
        public SostZakaz()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data source =DESKTOP-9FFBKHM; initial catalog =  телемонтаж; integrated security = SSPI"); //подключение к самой БД
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("введите данные");
            }
            else
            {if (comboBox2.Text == "Коломна ")
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("insert заказчики([Ф.И.О. заказчика],Адрес,[номер телефона]) values('" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox3.Text + "') ", con);
                    SqlDataReader dataReader = com.ExecuteReader();
                    DataTable DT = new DataTable();
                    DT.Load(dataReader);
                    con.Close();
                }
            else
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("insert заказчики([Ф.И.О. заказчика],Адрес,[номер телефона]) values('" + textBox2.Text + "','" + comboBox4.Text + "','" + textBox3.Text + "') ", con);
                    SqlDataReader dataReader = com.ExecuteReader();
                    DataTable DT = new DataTable();
                    DT.Load(dataReader);
                    con.Close();
                }
                con.Open();
                SqlCommand com1 = new SqlCommand("insert Заказы([Ф.И.О. заказчика],[Наименование услуги],Стоимость) values ('" + textBox2.Text + "','" + comboBox1.Text + "','" + label3.Text + "' ) ", con);
                SqlDataReader dataReader1 = com1.ExecuteReader();
                DataTable DT1 = new DataTable();
                DT1.Load(dataReader1);
                con.Close();
                MessageBox.Show("Заказ успешно выполнен.В скором времени мы с вами свяжемся");
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        
            //this.список_услугTableAdapter.Fill(this.телемонтажDataSet1.Список_услуг);
     

        }
        private void label3_Click(object sender, EventArgs e)
        {
            con.Open();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = comboBox1.Text;

            string q1 = Convert.ToString(label3.Text);
            if (q1 == "установка системы кондиционирования воздуха")
            {
                label3.Text = "20000";
            }
            else
            {
                if (q1 == "устновка системы энергосбережения")
                {
                    label3.Text = "10000";
                }
                else
                {
                    if (q1 == "установка системы вентиляции, дымоудаления")
                    {
                        label3.Text = "40000";
                    }
                    else
                    {
                        if (q1 == "установкка системы «Умный дом»")
                        {
                            label3.Text = "60000";
                        }
                    }
                }
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                MessageBox.Show("введите символы");
            e.Handled = true;
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                MessageBox.Show("введите цифру");
            e.Handled = true;
        }
        private void списокУслугBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
                if (comboBox2.Text == "Коломна ")
                {
                    comboBox3.Visible = true;
                    comboBox4.Visible = false;

                }
                else
                {
                    if (comboBox2.Text == "Воскресенск ")
                    {
                    comboBox3.Visible = false;
                    comboBox4.Visible = true;
                    }
                    else
                    {
                    comboBox3.Visible = true;
                    comboBox4.Visible = true;
                    }
                }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
