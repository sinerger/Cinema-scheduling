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
            foreach (Hall hall in Halls)
            {
                Node node = new Node(TimeClosed-TimeOpen);
                node.CreateGraph();
                hall.Schedule = node.FindMinEmptyTimeSchedule();
            }
        }

        public string GetSchedule()
        {
            return Halls[0].GetSheduling();
        }
    }
}
