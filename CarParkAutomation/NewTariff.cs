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
    public partial class NewTariff : Form
    {
        public DatabaseConnection databaseconnection;
        public NewTariff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tariff newtariff = new Tariff();

            newtariff.FirstPrice = (int)numericUpDown1.Value;
            newtariff.SecondPrice = (int)numericUpDown2.Value;
            newtariff.ThirdPrice = (int)numericUpDown3.Value;
            newtariff.DefaultPrice = (int)numericUpDown4.Value;

            try
            {
                databaseconnection.AddNewTariff(newtariff);
                MessageBox.Show("Yeni Tarife Başarıyla Eklendi.");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
            }
            
        }

        private void NewTariff_Load(object sender, EventArgs e)
        {
            Tariff tariff = databaseconnection.GetTheLastTariff();
            label13.Text = tariff.FirstPrice.ToString();
            label12.Text = tariff.SecondPrice.ToString();
            label11.Text = tariff.ThirdPrice.ToString();
            label10.Text = tariff.DefaultPrice.ToString();

        }
    }
}
