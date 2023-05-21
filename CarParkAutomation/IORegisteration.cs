using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppnetfreamwork
{
    public class IORegisteration
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Plaka { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? OutDate {get; set;}
        public int Time { get; set; }
        public int Price { get; set; }
    
        public IORegisteration(int CustomerId, string Plaka, DateTime EntryDate)
        {
            this.CustomerId = CustomerId;
            this.Plaka = Plaka;
            this.EntryDate = EntryDate;
            OutDate = null;
        }
        public IORegisteration() { }
    }
}
