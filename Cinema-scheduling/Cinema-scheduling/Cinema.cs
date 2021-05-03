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
            CountHall = countHall;
            Halls = new List<Hall>();

            for (int i = 0; i < CountHall; i++)
            {
                Halls.Add(new Hall(i));
            }
        }

        public void SetSchedulesHalls()
        {
            if (_license.GetSumDurationAllFilms() <= GetSumAllTimeHalls())
            {
                Node node = new NodeForBestFillingHallFactory().GetNode(TimeClosed - TimeOpen);

                node.CreateGraph();
                node.GetListSchedule();
                List<Schedule> list = node.QQQ(CountHall);

                int index = 0;

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
                //for (int i = 0; i < Halls.Count; i++)
                //{
                //    for (int j = Halls.Count - 1; j > i; j--)
                //    {
                //        if (Halls[i].Schedule.Equals(Halls[j].Schedule))
                //        {
                //            Film tempFilm = Halls[j].Schedule.Films[0];
                //            Halls[j].Schedule.Films.Remove(tempFilm);
                //            Halls[j].Schedule.Films.Add(tempFilm);
                //        }
                //    }
                //}
                //foreach (Hall hall in Halls)
                //{
                //    foreach (Hall hall1 in Halls)
                //    {
                //        if (hall.Schedule.Equals(hall1.Schedule))
                //        {
                //            Film tempFilm = hall1.Schedule.Films[0];
                //            hall1.Schedule.Films.Remove(tempFilm);
                //            hall1.Schedule.Films.Add(tempFilm);
                //        }
                //    }
                //}
            }
            else
            {

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

        private int GetSumAllTimeHalls()
        {
            return (TimeClosed - TimeOpen) * Halls.Count;
        }
    }
}
