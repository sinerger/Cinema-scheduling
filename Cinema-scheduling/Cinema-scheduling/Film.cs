using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Film : IComparable
    {
        public int Duration { get; set; }
        public string Name { get; set; }

        public Film(int duration, string name)
        {
            Duration = duration;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is Film)
            {
                Film film = (Film)obj;

                if (Duration == film.Duration && Name == film.Name)
                {
                    result = true;
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string time = $"{Duration / 60}:{Duration % 60}";
            result.Append($"{ time} {Name}");

            return result.ToString();
        }

        public int CompareTo(object obj)
        {
            if (obj is Film)
            {
                int result = 1;
                Film film = (Film) obj;
                if (Duration < film.Duration)
                {
                    result = -1;
                }
                else if (Duration == film.Duration)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }

                return result;
            }

            throw new ArgumentException("Incorrect type");
        }
    }
}
