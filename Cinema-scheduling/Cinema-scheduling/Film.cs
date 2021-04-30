using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Film
    {
        public int Duration { get; set; }
        public string Name { get; set; }

        public Film(int duration, string name)
        {
            Duration = duration;
            Name = name;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
