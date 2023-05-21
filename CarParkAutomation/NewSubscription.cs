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
    public partial class NewSubscription : Form
    {
        public DatabaseConnection databaseconnection;

        int price;
        public NewSubscription()
        {
            InitializeComponent();
        }

        private void NewSubscription_Load(object sender, EventArgs e)
        {
            List<Customer> customers;
            customers = databaseconnection.GetUnsubscripbedCustomers();

            //ComboBox'ımızı oluşturalım.
            comboBox1.DataSource = customers;
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "Id";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //DataGridView içerisine, mevcut aboneliklerimizi koyalım.
            List<Subscription> subs = databaseconnection.GetAllSubscriptions();
            BindingSource bindingsource = new BindingSource();
            bindingsource.DataSource = subs;
            dataGridView1.DataSource = bindingsource;
            
            dataGridView1.ReadOnly = true;
            this.Controls.Add(dataGridView1);
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            price = (int)numericUpDown1.Value * Subscription.PricePerMonth;

            label4.Text = $"{price} TL";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Subscription newsub = new Subscription();
            Customer selectedCustomer = (Customer)comboBox1.SelectedItem;

            newsub.CustomerId = selectedCustomer.Id;
            newsub.RegisterDate = DateTime.Now;
            newsub.FinishDate= DateTime.Now.AddMonths((int)numericUpDown1.Value);
            newsub.Price = price;

            try
            {
                databaseconnection.AddNewSubscription(newsub);
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }

            
        }
    }
}
