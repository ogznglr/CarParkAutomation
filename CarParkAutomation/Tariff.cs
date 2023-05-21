using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppnetfreamwork
{
    public class Tariff
    {
        public int Id { get; set; }
        public int FirstPrice { get; set; }
        public int SecondPrice { get; set; }
        public int ThirdPrice { get; set; }
        public int DefaultPrice { get; set; }

        public int Calculate(int time)
        {
            if(time >=0 && time <= 2)
            {
                if (time == 0)
                    time = 1;

                return time * FirstPrice;
            }else if (time <= 4)
            {
                return time * SecondPrice;
            }
            else if(time <= 6)
            {
                return time * ThirdPrice;
            }
            else
            {
                return time * DefaultPrice;
            }
        }
    }
}
