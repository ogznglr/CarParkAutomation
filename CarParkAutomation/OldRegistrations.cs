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
    public partial class OldRegistrations : Form
    {
        public DatabaseConnection databaseconnection;
        public OldRegistrations()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OldRegistrations_Load(object sender, EventArgs e)
        {
            List<IORegisteration> registers = databaseconnection.GetNotNullRegisters();

            List<Subscription> subs = databaseconnection.GetAllSubscriptions();

            //Kayıtları filtreleme işlemi yapıyoruz. Sadece abone olmayanlar kalsın. Çünkü abonelerden ek ücret almıyoruz.
            registers.RemoveAll(r => r.Price == 0);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = registers;
            dataGridView1.DataSource = bindingSource;
            // Sütunlar oluşturun ve özelleştirin
            dataGridView1.Columns["Id"].Visible = false; // Id sütunu gizle
            dataGridView1.ReadOnly = true;


            BindingSource bindingSource2 = new BindingSource();
            bindingSource2.DataSource = subs;
            dataGridView2.DataSource = bindingSource2;
            dataGridView2.Columns["Id"].Visible = false; // Id sütunu gizle
            dataGridView2.ReadOnly = true;



            this.Controls.Add(dataGridView1);
            this.Controls.Add(dataGridView2);


            //Toplam geliri hesapla
            int totalPrice = 0;
            foreach (IORegisteration register in registers)
            {
                totalPrice += register.Price;
            }
            foreach(Subscription sub in subs)
            {
                totalPrice += sub.Price;
            }

            label2.Text = $"{totalPrice} TL";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
