﻿using System;
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
    public partial class Form1 : Form
    {
        public DatabaseConnection databaseConnection;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                databaseConnection.AddNewCustomer(new Customer(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text));
                MessageBox.Show("Müşteri Eklendi");   
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
