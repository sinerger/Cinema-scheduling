using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Hall
    {
        public int Number { get; set; }
        public Sheduling Sheduling { get; set; }

        public Hall(int number)
        {
            Number = number;
            Sheduling = new Sheduling(Cinema.TimeClosed - Cinema.TimeOpen);

        }
        public string GetSheduling()
        {
            if (Sheduling != null)
            {
                return Sheduling.ToString();
            }

            throw new ArgumentNullException("Sheduling is null");
        }
    }
}
