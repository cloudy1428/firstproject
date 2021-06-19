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
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using System.IO;


namespace WindowsFormsApp1
{
    public partial class Personal : Form
    {
        Word._Application oWord = new Word.Application();
        public Personal()
        {
            InitializeComponent();
        }
        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                //oDoc.Application.Selection.Tables[1].Select();
                //oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                //oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                //oDoc.Application.Selection.Tables[1].Rows[1].Select();
                //oDoc.Application.Selection.InsertRowsAbove(1);
                //oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Times New Roman";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                //for (int c = 0; c <= ColumnCount - 1; c++)
                //{
                //    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                //}

                //table style 
                //oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                //oDoc.Application.Selection.Tables[1].Rows[1].Select();
                //oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                //foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                //{
                //    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                //    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                //    headerRange.Text = "your header text";
                //    headerRange.Font.Size = 16;
                //    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                //}

                //save the file
                oDoc.SaveAs2(filename);

                //NASSIM LOUCHANI
            }
        }
        SqlConnection con = new SqlConnection(@"Data source = DESKTOP-9FFBKHM; initial catalog =  телемонтаж; integrated security = SSPI");
        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "телемонтажDataSet2.Сотрудники". При необходимости она может быть перемещена или удалена.
            //this.сотрудникиTableAdapter.Fill(this.телемонтажDataSet2.Сотрудники);
            //comboBox5.Items.RemoveAt(1);
            con.Open();
            SqlCommand com = new SqlCommand(@"select distinct [Номер заказа],Заказы.[Ф.И.О. заказчика],[Наименование услуги],Стоимость,[Статус оплаты],[Ф.И.О. исполнителя],адрес
from Заказы join Заказчики ON Заказчики.[Ф.И.О. заказчика] = Заказы.[Ф.И.О. заказчика]", con);
            SqlDataReader red = com.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(red);
            dataGridView1.DataSource = DT;
            con.Close();
            //dataGridView1.Columns[0].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + ("[Ф.И.О. заказчика]") + ", System.String) like '%" + textBox1.Text + "%'");
            dataGridView1.DataSource = bs;
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
                MessageBox.Show("Статус оплаты обновлен");
                this.Refresh();
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(dataGridView1, sfd.FileName);
            }
        }
        object oMissing = System.Reflection.Missing.Value;
        private void Personal_FormClosing(object sender, FormClosingEventArgs e)
        {
            oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
        }
        private _Document GetDoc(string path)
        {
            _Document oDoc = oWord.Documents.Add(path);
            SetTemplate(oDoc);
            return oDoc;
        }

        private void SetTemplate(_Document oDoc)
        {
            throw new NotImplementedException();
        }

        private void SaveToDisk(Word._Document oDoc, string filePath)
        {
            object fileName = filePath;
            oDoc.SaveAs(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        }
       
        //private readonly string tmpword = AppDomain.CurrentDomain.BaseDirectory + "чек на составление заказа.dotx";

        private void button5_Click(object sender, EventArgs e)
        {

            //Создаём новый Word.Application
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //Загружаем документ
            Microsoft.Office.Interop.Word.Document doc = null;

            object fileName = @"C:\Users\atsyr\OneDrive\Рабочий стол\Чек на составление заказа.dotx";
            object falseValue = false;
            object trueValue = true;
            object missing = Type.Missing;

            doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);

            //Теперь у нас есть документ который мы будем менять.

            //Очищаем параметры поиска
            app.Selection.Find.ClearFormatting();
            app.Selection.Find.Replacement.ClearFormatting();
            Word.Bookmarks wBookmarks = doc.Bookmarks;
            Word.Range wRange;
            int i = 0;
            object replaceWith = dataGridView1.Rows[0].Cells[0];
            Convert.ToString(replaceWith);
            object replaceWith1 = dataGridView1.Rows[0].Cells[1];
            Convert.ToString(replaceWith1);
            object replaceWith2 = dataGridView1.Rows[0].Cells[2];
            Convert.ToString(replaceWith2);
            object replaceWith3 = dataGridView1.Rows[0].Cells[3];
            Convert.ToString(replaceWith3);
            object replaceWith4 = dataGridView1.Rows[0].Cells[4];
            Convert.ToString(replaceWith4);
            object replaceWith5 = dataGridView1.Rows[0].Cells[5];
            Convert.ToString(replaceWith5);
            object replaceWith6 = dataGridView1.Rows[0].Cells[6];
            Convert.ToString(replaceWith6);
            //string[] data = new string[7] { dataGridView1.Rows[0].Cells[6], replaceWith2, replaceWith, replaceWith1, replaceWith3, replaceWith4, replaceWith5, replaceWith6 };
            foreach (Word.Bookmark mark in wBookmarks)
            {

                wRange = mark.Range;
                //wRange.Text = data[i];
                i++;
            }
            //Задаём параметры замены и выполняем замену.
            //object findText = "номер";
            //object replaceWith = dataGridView1.Rows[0].Cells[0];
            //object replace = 2;

         
           
            //object findText1 = "заказчик";
            //object replaceWith1 = dataGridView1.Rows[0].Cells[1];
            //object replace1 = 2;



            //Открываем документ для просмотра.
            app.Visible = true;
        }

        private void ReplaceWordStub(string v, string uchastok, ref Document wordDocument)
        {
            throw new NotImplementedException();
        }

        private void ReplaceWordStub(string v, string uchastok, ref object wordDocument)
        {
            throw new NotImplementedException();
        }

        private void PrintDoc(Document oDoc)
        {
            throw new NotImplementedException();
        }

    }
}
