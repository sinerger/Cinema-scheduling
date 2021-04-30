using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Node
    {
        public License License;
        public int EmptyTime { get; set; }
        public List<Film> CurrentFilms { get; set; }
        public List<Node> Next { get; set; }

        public Node(int emptyTime, List<Film> currentFilms = null)
        {
            EmptyTime = emptyTime;
            Next = new List<Node>();
            License = License.GetLicense();

            if(currentFilms is null)
            {
                CurrentFilms = new List<Film>();
            }
            else
            {
                CurrentFilms = currentFilms;
            }
        }

        public void CreateGraph()
        {
            foreach (Film film in License.Films)
            {
                if (EmptyTime > film.Duration)
                {
                    List<Film> tempFilmList = new List<Film>(CurrentFilms);
                    tempFilmList.Add(film);
                    Node newNode = new Node(EmptyTime - film.Duration, tempFilmList);
                    Next.Add(newNode);
                    newNode.CreateGraph();
                }
            }
        }

        public void WriteAllLeaves()
        {
            if (Next.Count == 0)
            {
                foreach (Film film in CurrentFilms)
                {
                    
                }
            }
            else
            {
                foreach (Node node in Next)
                {
                    node.WriteAllLeaves();
                }
            }
        }

        public Sheduling FindMinEmptyTimeSheduling()
        {
            if (Next.Count == 0)
            {
                return new Sheduling(EmptyTime, CurrentFilms);
            }
            else
            {
                List<Sheduling> shedulings = new List<Sheduling>();

                foreach (Node n in Next)
                {
                    shedulings.Add(n.FindMinEmptyTimeSheduling());
                }

                Sheduling minSheduling = shedulings[0];

                foreach (Sheduling r in shedulings)
                {
                    if (minSheduling.EmptyTime >= r.EmptyTime)
                    {
                        minSheduling = r;
                    }
                    else if ((minSheduling.EmptyTime == r.EmptyTime) && (minSheduling.Films.Count > r.Films.Count))
                    {
                        minSheduling = r;
                    }
                }

                return minSheduling;
            }
        }
    }
}
