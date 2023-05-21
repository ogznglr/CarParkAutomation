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

namespace WindowsFormsAppnetfreamwork
{
    public partial class AddRegister : Form
    {
        public DatabaseConnection databaseConnection;
        public AddRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Seçilen müşterimizi aldık.
            Customer selectedCustomer = (Customer)comboBox1.SelectedItem;

            //Plakamızı aldık.
            string plaka = textBox1.Text;

            //Eğer zaten giriş yoksa yeni kayır oluşturalım.

            if (databaseConnection.IsThereAvaibleRegister(plaka))
            {
                MessageBox.Show("Kayıt Mevcut.");
                return;
            }
            

            IORegisteration newreg = new IORegisteration(selectedCustomer.Id, plaka, DateTime.Now);

            databaseConnection.AddNewEntrance(newreg);
            MessageBox.Show("Kayıt Başarılı.");
            this.Close();

        }

        private void AddRegister_Load(object sender, EventArgs e)
        {
            List<Customer> customers;
            customers = databaseConnection.GetAllCustomers();

            //ComboBox'ımızı oluşturalım.
            comboBox1.DataSource = customers;
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "Id";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
