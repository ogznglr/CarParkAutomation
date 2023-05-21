using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppnetfreamwork
{
    public class Subscription
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime FinishDate { get; set; }
        public static int PricePerMonth = 100;
        public int Price { get; set; }
    }
}
