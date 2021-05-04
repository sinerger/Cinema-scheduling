using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Node2
    {
        private License _license;
        public List<Schedule> AllSchedule { get; set; }
        public List<Node2> Next { get; set; }
        public List<Schedule> CurrentSchedules { get; set; }
        public int CountHall { get; set; }
        public Schedule Schedule { get; set; }
        public Node2(int countHall, List<Schedule> currentSchedules = null, Schedule schedule = null)
        {
            Schedule = schedule;
            _license = License.GetLicense();
            Next = new List<Node2>();
            AllSchedule = _license.AllSchedules;
            CountHall = countHall;

            if (currentSchedules is null)
            {
                CurrentSchedules = new List<Schedule>();
            }
            else
            {
                CurrentSchedules = currentSchedules;
            }
        }

        public void CreateGraph()
        {
            int maxCapacityFilmsHalls = (Cinema.TimeClosed - Cinema.TimeOpen);
            maxCapacityFilmsHalls = maxCapacityFilmsHalls / _license.AverageDuration;
            if (_license.Films.Count < maxCapacityFilmsHalls)
            {
                foreach (Schedule schedule in _license.AllSchedules)
                {
                    if (CurrentSchedules.Count < CountHall)
                    {
                        if (Schedule != schedule)
                        {
                            List<Schedule> tempSchedules = new List<Schedule>(CurrentSchedules);
                            tempSchedules.Add(schedule);
                            Node2 newNode2 = new Node2(CountHall, tempSchedules, schedule);
                            Next.Add(newNode2);
                            newNode2.CreateGraph();
                        }
                    }
                }
            }
        }
        public List<List<Schedule>> GetAllSchedules()
        {
            if (Next.Count == 0)
            {
                return new List<List<Schedule>>() { new List<Schedule>(CurrentSchedules) };
            }
            else
            {
                List<List<Schedule>> allSchedules = new List<List<Schedule>>();

                foreach (Node2 n in Next)
                {
                    allSchedules.AddRange(n.GetAllSchedules());
                }

                //allSchedules.Sort();

                return allSchedules;
            }
        }
    }
}
