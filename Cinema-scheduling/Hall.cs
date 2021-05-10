using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_scheduling
{
    public class Hall
    {
        private Schedule _schedule;
        public string Title { get; set; }
        public Schedule Schedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                if (value != null)
                {
                    _schedule = value;
                }
            }
        }

        public Hall(string title = null)
        {
            Title = title == null ? string.Empty : title;
            _schedule = new Schedule();
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Hall)
            {
                Hall hall = (Hall)obj;

                if (((Schedule == null && hall.Schedule == null) || Schedule.Equals(hall.Schedule)) && Title == hall.Title)
                {
                    result = true;
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"{Title} Empty time: {Schedule.EmptyTime}; Count unique films: {Schedule.CountUniqueFilm}; {Schedule.ToString()}");

            return result.ToString();
        }
    }
}
