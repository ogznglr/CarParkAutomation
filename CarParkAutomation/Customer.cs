using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppnetfreamwork
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Subscribed { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public Customer(string FirstName, string LastName, string PhoneNumber,string Province, string District)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Province = Province;
            this.District = District;
            Subscribed = false;
        }

        public Customer() { }
    }
}
