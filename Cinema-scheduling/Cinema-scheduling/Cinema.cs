using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Cinema
    {
        private static int _timeOpen = 600;
        private static int _timeClosed = 1440;
        public static int TimeOpen
        {
            get
            {
                return _timeOpen;
            }
            private set
            {
                _timeOpen = value;
            }
        }
        public static int TimeClosed
        {
            get
            {
                return _timeClosed;
            }
            private set
            {
                _timeClosed = value;
            }
        }
        public int CountHall { get; set; }
        public List<Hall> Halls { get; set; }

        public Cinema(int countHall)
        {
            CountHall = countHall;
            Halls = new List<Hall>();

            for (int i = 0; i < CountHall; i++)
            {
                Halls.Add(new Hall(i));
            }
        }

        public void SetShedulingHalls()
        {
            Node node = new Node(TimeClosed - TimeOpen);
            node.CreateGraph();
            List<Schedule> list = node.GetListSchedule();
            int index = 0;
            foreach (Hall hall in Halls)
            {
                hall.Schedule = list[index];
                list.Remove(list[index]);
                index++;
            }
        }

        public string GetSchedule()
        {
            StringBuilder result = new StringBuilder();
            foreach (Hall hall in Halls)
            {
                result.Append($"\n Hall #{hall.Number}\n{hall.GetSheduling()}");
            }
            return result.ToString();
        }
    }
}
