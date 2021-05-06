using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_scheduling
{
    public class Film : IComparable
    {
        public int Duration { get; set; }
        public string Title { get; set; }

        public Film(int duration, string title)
        {
            Duration = duration;
            Title = title;
        }

        public Film (Film film)
        {
            Duration = film.Duration;
            Title = film.Title;
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is Film)
            {
                Film film = (Film)obj;

                if (Duration == film.Duration && Title == film.Title)
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
            result.Append($"{ time} {Title}");

            return result.ToString();
        }

        public int CompareTo(object obj)
        {
            if (obj is Film)
            {
                int result = 1;
                Film film = (Film)obj;

                return Duration.CompareTo(film.Duration);
            }

            throw new ArgumentException("Incorrect type");
        }
    }
}
