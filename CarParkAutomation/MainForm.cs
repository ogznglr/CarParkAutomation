using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppnetfreamwork
{
    public partial class MainForm : Form
    {

        Form1 newcustomer;
        AddRegister addregister;
        AvaibleRegistersForm avaibleregistersform;
        NewSubscription newsubscriptionform;
        OldRegistrations oldRegistrationsform;
        NewTariff newtariff;


        public MainForm(DatabaseConnection db)
        {
            InitializeComponent();



            //Uygulamamızın sayfalarını oluşturalım.
            newcustomer = new Form1();
            newcustomer.databaseConnection = db;

            addregister = new AddRegister();
            addregister.databaseConnection = db;

            avaibleregistersform = new AvaibleRegistersForm();
            avaibleregistersform.databaseconnection = db;

            newsubscriptionform = new NewSubscription();
            newsubscriptionform.databaseconnection = db;

            oldRegistrationsform = new OldRegistrations();
            oldRegistrationsform.databaseconnection = db;

            newtariff = new NewTariff();
            newtariff.databaseconnection = db;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            newcustomer.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addregister.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            avaibleregistersform.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            newsubscriptionform.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            oldRegistrationsform.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            newtariff.ShowDialog();
        }
    }
}
