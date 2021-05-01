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
        public Schedule Schedule { get; set; }

        public Hall(int number)
        {
            Number = number;
            Schedule = new Schedule(Cinema.TimeClosed - Cinema.TimeOpen);

        }
        public string GetSheduling()
        {
            if (Schedule != null)
            {
                return Schedule.ToString();
            }

            throw new ArgumentNullException("Schedule is null");
        }
    }
}
