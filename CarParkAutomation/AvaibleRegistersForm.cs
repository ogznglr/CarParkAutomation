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
    public partial class AvaibleRegistersForm : Form
    {
        public DatabaseConnection databaseconnection;

        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();

        public AvaibleRegistersForm()
        {
            InitializeComponent();

            buttonColumn.HeaderText = "Exit";
            buttonColumn.Text = "Çıkış Yap";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(buttonColumn);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Seçilen rowdan datayı getir.
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                int Id = int.Parse(selectedRow.Cells["Id"].Value.ToString());
                int CustomerId = int.Parse(selectedRow.Cells["CustomerId"].Value.ToString());

                DateTime EntryDate = (DateTime)selectedRow.Cells["EntryDate"].Value;

                //Çıkış yapılacak olan kaydın yeni halini oluşturalım.
                IORegisteration updatedRegister = new IORegisteration();
                updatedRegister.Id = Id;
                updatedRegister.OutDate = DateTime.Now;
                
                TimeSpan timeDiff = DateTime.Now.Subtract(EntryDate);
                updatedRegister.Time = (int)timeDiff.TotalHours;

                Customer customer = databaseconnection.GetCustomer(CustomerId);

                //Ödenecek tutarı hesaplayalım. Abone ise para almayacağız abone değilse ücret hesabı tarifeden hesaplanacak.
                if (customer.Subscribed)
                {
                    Subscription sub = databaseconnection.GetSubscription(CustomerId);
                    if (DateTime.Compare(sub.FinishDate, DateTime.Now) > 0)
                    {
                        updatedRegister.Price = 0;
                    }
                }else
                {
                    //Aracın kaldığı süreyi parametre olarak yollayarak, ödenmesi gereken ücreti buluyoruz.
                    Tariff tariff = databaseconnection.GetTheLastTariff();
                    updatedRegister.Price = tariff.Calculate(updatedRegister.Time);
                }
                databaseconnection.UpdateRegister(updatedRegister);
                MessageBox.Show($"Müşterinin borcu {updatedRegister.Price} TL'dir.");
                //Sayfa update olmalı.
            }
        }

        private void AvaibleRegistersForm_Load(object sender, EventArgs e)
        {
            List<IORegisteration> registers = databaseconnection.GetAllRegisters();

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = registers;

            dataGridView1.DataSource = bindingSource;

            // Sütunlar oluşturun ve özelleştirin
            dataGridView1.Columns["Id"].Visible = false; // Id sütunu gizle

            
            

            dataGridView1.ReadOnly = true;
            

            this.Controls.Add(dataGridView1);
        }
    }
}
