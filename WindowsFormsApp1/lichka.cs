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
    public partial class lichka : Form
    {
        public lichka()
        {
            InitializeComponent();
        }
        public string aaa;
        SqlConnection Sql = new SqlConnection(@"Data source =DESKTOP-9FFBKHM;initial catalog = телемонтаж;integrated security = SSPI");
        
        
        private void lichka_Load(object sender, EventArgs e)
        {
            Sql.Open();
            SqlCommand com = new SqlCommand(@"select [Ф.И.О. сотрудника],Должность, адрес   From сотрудники  where login = '" + aaa + "'", Sql);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView1.DataSource = DT;
            String ss = dataGridView1[0, 0].Value.ToString();
            label6.Text = ss;
            String s = dataGridView1[1, 0].Value.ToString();
            label9.Text = s;
            String s1 = dataGridView1[2, 0].Value.ToString();
            label8.Text = s1; ;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Personal lh = new Personal();
            lh.Show();
        }
    }
}
