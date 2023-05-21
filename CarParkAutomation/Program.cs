using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;


namespace WindowsFormsAppnetfreamwork
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabaseConnection Db = new DatabaseConnection();
            Db.AutoMigrate();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            MainForm mainform = new MainForm(Db);


            Application.Run(mainform);
        }
    }

    public class DatabaseConnection
    {
        private const string connectionString = @"Server=localhost\SQLEXPRESS;Database=OtoparkDatabase;Trusted_Connection=True;";
        SqlConnection connection;
        public DatabaseConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void AutoMigrate()
        {
            //Daha önceden MigrateDatabase olarak veritabanımıza yüklemiş olduğumuz prosedür ismini kayıt ediyoruz.
            string ProcedureName = "dbo.MigrateDatabase";

            using (SqlCommand command = new SqlCommand(ProcedureName, connection))
            {
                //Command type'ı StoredProcedure olarak ayarlıyoruz. Bu sayede prosedür çağırabileceğiz.
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public void AddNewCustomer(Customer customer) {
            //Daha önceden MigrateDatabase olarak veritabanımıza yüklemiş olduğumuz prosedür ismini kayıt ediyoruz.
            string ProcedureName = "dbo.CreateCustomer";

            using (SqlCommand command = new SqlCommand(ProcedureName, connection))
            {
                //Command type'ı StoredProcedure olarak ayarlıyoruz. Bu sayede prosedür çağırabileceğiz.
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Parametre olarak değerleri girelim. Prosedürümüze gönderelim.
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@Province", customer.Province);
                command.Parameters.AddWithValue("@District", customer.District);
                command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                try
                {
                    command.ExecuteNonQuery();
                    
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Dispose();
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {
            string sql = "dbo.GetAllCustomers";

            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader;

            try
            {
                reader = command.ExecuteReader();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }

            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                Customer customer = new Customer();
                customer.Id = (int)reader["Id"];
                customer.FirstName = (string)reader["FirstName"];
                customer.LastName = (string)reader["LastName"];
                customer.PhoneNumber = (string)reader["PhoneNumber"];
                customer.RegistrationDate = (DateTime)reader["RegistrationDate"];
                customers.Add(customer);
            }
            reader.Close();

            return customers;
        }
        public List<Customer> GetUnsubscripbedCustomers()
        {
            string sql = "dbo.GetUnsubscribedCustomers";
            List<Customer> customers = new List<Customer>();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = int.Parse(reader["Id"].ToString());
                        customer.FirstName = reader["FirstName"].ToString();
                        customer.LastName = reader["LastName"].ToString();
                        customer.PhoneNumber = reader["PhoneNumber"].ToString();
                        customer.Province = reader["Province"].ToString();
                        customer.District= reader["District"].ToString();
                        customer.RegistrationDate = (DateTime)reader["RegistrationDate"];

                        customers.Add(customer);
                    }
                }            
            }
            return customers;
        }
        public Customer GetCustomer(int id)
        {
            string sql = "dbo.GetCustomerById @CustomerId";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@customerId", id);
                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.Read())
                    {
                        int customerId = (int)reader["Id"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];
                        string phoneNumber = (string)reader["PhoneNumber"];
                        string province = (string)reader["Province"];
                        string district = (string)reader["District"];
                        DateTime registrationDate = (DateTime)reader["RegistrationDate"];
                        bool subscribed = (bool)reader["Subscribed"];

                        Customer customer = new Customer(firstName, lastName, phoneNumber, province, district);
                        customer.RegistrationDate = registrationDate;
                        customer.Subscribed = subscribed;

                        return customer;
                    }
                return null;
            }
        }

        public bool IsThereAvaibleRegister(string Plaka)
        {
            string sql = "EXEC dbo.GetActiveRegister @Plaka";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Plaka", Plaka);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            reader.Close();
            command.Dispose();
            return false;
        }

        public void AddNewEntrance(IORegisteration entrance)
        {
            string sql = "EXEC dbo.InsertRegister @CustomerId, @Plaka, @EntryDate";
            SqlCommand command = new SqlCommand(sql, connection);

            // Set parameter values
            command.Parameters.AddWithValue("@CustomerId", entrance.CustomerId);
            command.Parameters.AddWithValue("@Plaka", entrance.Plaka);
            command.Parameters.AddWithValue("@EntryDate", entrance.EntryDate);

            // Execute the command
            command.ExecuteNonQuery();

            command.Dispose();
        }
        public List<IORegisteration> GetAllRegisters()
        {
            //OutDate null olan kayıtları getir. Bu sayede hala otoparktaki kayıtları buluyoruz.

            string sql = "dbo.GetActiveRegisters";
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<IORegisteration> registers = new List<IORegisteration>();

            while (reader.Read())
            {
                IORegisteration register = new IORegisteration();
                register.Id = (int)reader["Id"];
                register.CustomerId = (int)reader["CustomerId"];
                register.Plaka = (string)reader["Plaka"];
                register.EntryDate = (DateTime)reader["EntryDate"];
                registers.Add(register);
            }
            reader.Close();
            command.Dispose();

            return registers;
        }

        public void UpdateRegister(IORegisteration updatedRegister)
        {
            string sql = "dbo.UpdateRegister @Id, @OutDate, @Price, @Time";
            
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                // Değiştirilecek kaydın Id'si
                command.Parameters.AddWithValue("@Id", updatedRegister.Id);

                // Yeni değerler
                command.Parameters.AddWithValue("@OutDate", updatedRegister.OutDate);
                command.Parameters.AddWithValue("@Price", updatedRegister.Price);
                command.Parameters.AddWithValue("@Time", updatedRegister.Time);

                int result = command.ExecuteNonQuery();

                //Execute metodumuz etkilenen row sayısını döndürüyor. Eğer 0'dan büyükse, update işlemimiz başarılıdır.
                if (result == 0)
                {
                    MessageBox.Show("Kayıt güncellenemedi.");
                }
            }
        }
        public void AddNewSubscription(Subscription newsub)
        {
            string sql = "dbo.CreateSubscription @customerIdParam, @registerDateParam, @finishDateParam, @priceParam";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@customerIdParam", newsub.CustomerId);
                command.Parameters.AddWithValue("@registerDateParam", newsub.RegisterDate);
                command.Parameters.AddWithValue("@finishDateParam", newsub.FinishDate);
                command.Parameters.AddWithValue("@priceParam", newsub.Price);


                int result = 0;
                try
                {
                    result = command.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Dispose();
                }

                //Execute metodumuz, etkilenen row sayısını döndürecek. Eğer 0'dan fazlaysa ekleme işlemimiz başarılıdır.
                if (result > 0)
                {
                    MessageBox.Show("Yeni Abonelik Oluşturuldu.");
                }
            }
        }
        public Subscription GetSubscription(int customerId)
        {
            string sql = "dbo.GetSubscription @customerId";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@customerId", customerId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        int CustomerId= (int)reader["CustomerId"];
                        DateTime RegisterDate = (DateTime)reader["RegisterDate"];
                        DateTime FinishDate = (DateTime)reader["FinishDate"];
                        int Price = (int)reader["Price"];

                        Subscription sub = new Subscription();
                        sub.Id = Id;
                        sub.CustomerId = CustomerId;
                        sub.RegisterDate = RegisterDate;
                        sub.FinishDate = FinishDate;
                        sub.Price = Price;

                        return sub;
                    }
                    return null;
                }
            }
        }
        public List<Subscription> GetAllSubscriptions()
        {
            string sql = "Select * FROM Subscriptions";
            
            //Abonelikleri tutacağımız liste
            List<Subscription> subs = new List<Subscription>();
            
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        Subscription sub = new Subscription();
                        sub.Id = int.Parse(reader["Id"].ToString());
                        sub.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        sub.RegisterDate = (DateTime)reader["RegisterDate"];
                        sub.FinishDate = (DateTime)reader["FinishDate"];
                        sub.Price = int.Parse(reader["Price"].ToString());

                        subs.Add(sub);
                    }
                }
            }
            return subs;

        }
        public List<IORegisteration> GetNotNullRegisters()
        {
            string sql = "dbo.GetCompletedRegisters";
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<IORegisteration> registers = new List<IORegisteration>();

            while (reader.Read())
            {
                IORegisteration register = new IORegisteration();
                register.Id = (int)reader["Id"];
                register.CustomerId = (int)reader["CustomerId"];
                register.Plaka = (string)reader["Plaka"];
                register.EntryDate = (DateTime)reader["EntryDate"];
                register.OutDate = (DateTime)reader["OutDate"];
                register.Price = (int)reader["Price"];
                registers.Add(register);
            }
            reader.Close();
            command.Dispose();

            return registers;
        }
        public void AddNewTariff(Tariff newtariff) 
        {
            string sql = "dbo.InsertTariff @FirstPrice, @SecondPrice, @ThirthPrice, @DefaultPrice";
            
            using(SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@FirstPrice", newtariff.FirstPrice);
                command.Parameters.AddWithValue("@SecondPrice", newtariff.SecondPrice);
                command.Parameters.AddWithValue("@ThirthPrice", newtariff.ThirdPrice);
                command.Parameters.AddWithValue("@DefaultPrice", newtariff.DefaultPrice);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public Tariff GetTheLastTariff()
        {
            string sql = "dbo.AvaibleTariff";
            Tariff tariff = new Tariff();

            using (SqlCommand command = new SqlCommand(sql,connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tariff.Id = int.Parse(reader["Id"].ToString());
                        tariff.FirstPrice = int.Parse(reader["FirstPrice"].ToString());
                        tariff.SecondPrice = int.Parse(reader["SecondPrice"].ToString());
                        tariff.ThirdPrice = int.Parse(reader["ThirthPrice"].ToString());
                        tariff.DefaultPrice = int.Parse(reader["DefaultPrice"].ToString());
                    }
                }
            }
                return tariff;
        }
    }
}
