using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string connectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=LibraryDB.mdb;";
        public Form1()
        {
            InitializeComponent();
            
            LoadBooks();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadBooks()
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            try
            {
                
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM библиотека", connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("РћС€РёР±РєР° Р·Р°РіСЂСѓР·РєРё РєРЅРёРі: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string auth = textBox2.Text;
            int god;
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(auth) || !int.TryParse(textBox3.Text, out god))
            {
                MessageBox.Show("Введены не все данные.");
                return;
            }
            
            try
            {
                string query = "INSERT INTO библиотека ([Title], [Author], [Year]) VALUES (titl, autho, yea)";
                OleDbCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("titl", name);
                command.Parameters.AddWithValue("autho", auth);
                command.Parameters.AddWithValue("yea", god);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                MessageBox.Show("Сохранено!");
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка заполнения данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDBDataSet.библиотека". При необходимости она может быть перемещена или удалена.
            this.библиотекаTableAdapter.Fill(this.libraryDBDataSet.библиотека);

        }
    }
}
