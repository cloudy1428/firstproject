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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data source =DESKTOP-9FFBKHM; initial catalog = телемонтаж; integrated security = SSPI");
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand(@"select * from Заказы", con);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView2.DataSource = DT;
            con.Close();
            dataGridView2.Columns[0].Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("выберите статус");
            }
            {
                con.Open();
                SqlCommand com = new SqlCommand("update Заказы set [Статус оплаты] = '" + comboBox1.Text + "' where [Ф.И.О. заказчика] = '" + dataGridView2.Rows[0].Cells[1].Value + "' ", con);
                SqlDataReader dataReader = com.ExecuteReader();
                DataTable DT = new DataTable();
                DT.Load(dataReader);
                con.Close();
                this.Refresh();
                MessageBox.Show("Статус оплаты обнавлен");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("вы точно хотите удалить заказ?", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                SqlDataReader read = new SqlCommand(@" delete from Заказы where [Ф.И.О. заказчика] = '" + dataGridView2.Rows[0].Cells[1].Value + "'", con).ExecuteReader();
                read.Close();
                con.Close();
                MessageBox.Show("Пользователь удален");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand(@"select distinct [Номер заказа],Заказы.[Ф.И.О. заказчика],[Наименование услуги],Стоимость,[Статус оплаты],[Ф.И.О. исполнителя],адрес
from Заказы join Заказчики ON Заказчики.[Ф.И.О. заказчика] = Заказы.[Ф.И.О. заказчика]", con);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView2.DataSource = DT;
            con.Close();
            //dataGridView2.Columns[0].Visible = false;

            con.Open();
            SqlCommand com1 = new SqlCommand(@"select * from Сотрудники", con);
            SqlDataReader red1 = com1.ExecuteReader();
            DataTable DT1 = new DataTable();
            DT1.Load(red1);
            dataGridView1.DataSource = DT1;
            con.Close();

            con.Open();
            SqlCommand com2 = new SqlCommand(@"select [Ф.И.О. заказчика],Адрес,[номер телефона] from заказчики", con);
            SqlDataReader red2 = com2.ExecuteReader();
            DataTable DT2 = new DataTable();
            DT2.Load(red2);
            dataGridView3.DataSource = DT2;
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView2.DataSource;
            bs.Filter = string.Format("CONVERT(" + ("[Ф.И.О. заказчика]") + ", System.String) like '%" + textBox1.Text + "%'");
            dataGridView2.DataSource = bs;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Ф.И.О." || textBox3.Text == "Адрес")
            {
                MessageBox.Show("введите данные");
            }
            else
            {
            
            if (comboBox2.GetItemText(comboBox2.Text) == "Менеджер")
            {
                con.Open();
                SqlCommand com1 = new SqlCommand("insert Администратор([Ф.И.О.],адрес,Должность,Login,password) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "','" + textBox6.Text + "')", con);
                SqlDataReader red1 = com1.ExecuteReader();
                DataTable table1 = new DataTable();
                table1.Load(red1);
                con.Close();
                dataGridView1.Refresh();
                MessageBox.Show("Сотрудник успешно добавился");
            }
            else
            {
                    int ColumnNumber = 0;
                    for (int RowNumber = 0; RowNumber < dataGridView1.Rows.Count - 1; RowNumber++)
                    {

                        try 
                        {
                            if ((string)dataGridView1[ColumnNumber, RowNumber].Value != textBox2.Text)
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand("insert Сотрудники([Ф.И.О. сотрудника],адрес,Должность,Login,password) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "','" + textBox6.Text + "')", con);
                                SqlDataReader red = com.ExecuteReader();
                                DataTable table = new DataTable();
                                table.Load(red);
                                MessageBox.Show("Сотрудник успешно добавился");
                                con.Close();
                                
                            }
                      
                         
                        }
                       catch
                        {
                            MessageBox.Show("Пользователь с такими данными уже есть");
                            dataGridView1.Enabled = false;
                        }
                    }
            }
        }
        }
    

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand("update Сотрудники set Должность = '" + comboBox2.Text + "' where [Ф.И.О.]= '" + dataGridView1.Rows[0].Cells[1].Value + "' ", con);
            SqlDataReader dataReader = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(dataReader);
            con.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            bs1.DataSource = dataGridView1.DataSource;
            bs1.Filter = string.Format("CONVERT(" + ("[Ф.И.О. сотрудника]") + ", System.String) like '%" + textBox5.Text + "%'");
            dataGridView1.DataSource = bs1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("вы точно хотите удалить сотрудника?", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                SqlDataReader read = new SqlCommand(@" delete from Сотрудники where [Ф.И.О. сотрудника] = '" + dataGridView1.Rows[0].Cells[0].Value + "'", con).ExecuteReader();
                read.Close();
                con.Close();
                MessageBox.Show("Сотрудник удален");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com2 = new SqlCommand(@"select [Ф.И.О. заказчика],Адрес,[номер телефона] from заказчики", con);
            SqlDataReader red2 = com2.ExecuteReader();
            DataTable DT2 = new DataTable();
            DT2.Load(red2);
            dataGridView3.DataSource = DT2;
            con.Close();


            con.Open();
            SqlCommand com = new SqlCommand(@"select distinct [Номер заказа],Заказы.[Ф.И.О. заказчика],[Наименование услуги],Стоимость,[Статус оплаты],[Ф.И.О. исполнителя],адрес
from Заказы join Заказчики ON Заказчики.[Ф.И.О. заказчика] = Заказы.[Ф.И.О. заказчика]", con);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView2.DataSource = DT;
            con.Close();
            //dataGridView2.Columns[0].Visible = false;

            con.Open();
            SqlCommand com1 = new SqlCommand(@"select * from Сотрудники", con);
            SqlDataReader red1 = com1.ExecuteReader();
            DataTable DT1 = new DataTable();
            DT1.Load(red1);
            dataGridView1.DataSource = DT1;
            con.Close();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                MessageBox.Show("введите текст");
            e.Handled = true;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("вы точно хотите удалить сотрудника?", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                SqlDataReader read = new SqlCommand(@" delete from Заказчики where [Ф.И.О. заказчика] = '" + dataGridView3.Rows[0].Cells[1].Value + "'", con).ExecuteReader();
                read.Close();
                con.Close();
                MessageBox.Show("Сотрудник удален");
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            bs1.DataSource = dataGridView3.DataSource;
            bs1.Filter = string.Format("CONVERT(" + ("[Ф.И.О. заказчика]") + ", System.String) like '%" + textBox7.Text + "%' WHERE Product = 'Bikes'");
            dataGridView3.DataSource = bs1;
        }
    }
}
