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
    public partial class manager : Form
    {
        public manager()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data source =.\SQLEXPRESS; initial catalog = телемонтаж; integrated security = SSPI");
        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand("update Заказы set [Ф.И.О. исполнителя] = '" + comboBox5.Text + "' where [Ф.И.О. заказчика] = '" + dataGridView1.Rows[0].Cells[1].Value + "' ", con);
            SqlDataReader dataReader = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(dataReader);
            con.Close();
            MessageBox.Show("Исполнитель прикреплен");
            this.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand(@"select distinct [Номер заказа],Заказы.[Ф.И.О. заказчика],[Наименование услуги],Стоимость,[Статус оплаты],[Ф.И.О. исполнителя],адрес
from Заказы join Заказчики ON Заказчики.[Ф.И.О. заказчика] = Заказы.[Ф.И.О. заказчика]", con);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView1.DataSource = DT;
            con.Close();
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("выберите статус");
            }
            else
            {
                con.Open();
                SqlCommand com = new SqlCommand("update Заказы set [Статус оплаты] = '" + comboBox1.Text + "' where [Ф.И.О. заказчика] = '" + dataGridView1.Rows[0].Cells[1].Value + "' ", con);
                SqlDataReader dataReader = com.ExecuteReader();
                DataTable DT = new DataTable();
                DT.Load(dataReader);
                con.Close();
                MessageBox.Show("Статус оплаты обнавлен");
                this.Refresh();
            }
        }

        private void manager_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "телемонтажDataSet2.Сотрудники". При необходимости она может быть перемещена или удалена.
            this.сотрудникиTableAdapter.Fill(this.телемонтажDataSet2.Сотрудники);
            con.Open();
            SqlCommand com = new SqlCommand(@"select distinct [Номер заказа],Заказы.[Ф.И.О. заказчика],[Наименование услуги],Стоимость,[Статус оплаты],[Ф.И.О. исполнителя],адрес
from Заказы join Заказчики ON Заказчики.[Ф.И.О. заказчика] = Заказы.[Ф.И.О. заказчика]", con);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView1.DataSource = DT;
            con.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + ("[Ф.И.О. заказчика]") + ", System.String) like '%" + textBox1.Text + "%'");
            dataGridView1.DataSource = bs;
        }
    }
}
