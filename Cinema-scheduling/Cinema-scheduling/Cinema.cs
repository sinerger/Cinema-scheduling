using Cinema_scheduling.Factory;
using Cinema_scheduling.GraphCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Cinema
    {
        private License _license;
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
            _license = License.GetLicense();
            Halls = new List<Hall>();
            CountHall = countHall;

            for (int i = 1; i <= CountHall; i++)
            {
                Halls.Add(new Hall(i));
            }
        }

        public void SetSchedulesHalls()
        {
            Node node = new NodeForBestFillingHallFactory().GetNode(TimeClosed - TimeOpen);
            node.CreateGraph();
            List<Schedule> list = new List<Schedule>();
            if ((TimeClosed - TimeOpen) * CountHall > _license.GetAllDurationFilms())
            {
                list = node.GetListSchedulesforHalls(CountHall);
            }
            else
            {
                list = node.GetListSchedulesMaxUniqueFilmForHalls(CountHall);
            }

            int index = 0;

            if (list.Count > 0)
            {
                foreach (Hall hall in Halls)
                {
                    hall.Schedule = list[index];

                    if (index >= list.Count - 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }

        public string GetSchedulesAllHalls()
        {
            if (Halls != null)
            {
                StringBuilder result = new StringBuilder();

                foreach (Hall hall in Halls)
                {
                    result.Append($"\n Hall #{hall.Number}\n{hall.GetSheduling()}");
                }

                return result.ToString();
            }

            throw new ArgumentNullException("List Halls is null");
        }
    }
}
