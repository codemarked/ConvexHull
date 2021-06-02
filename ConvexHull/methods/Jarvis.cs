using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class Jarvis : HullMethod
    {
        private List<Point> points, hull;
        private bool exe;

        public Jarvis()
        {
            this.exe = false;
            this.points = new List<Point>();
            this.hull = new List<Point>();
        }

        public void initialize(List<Point> points)
        {
            this.points = points;
            this.hull.Clear();
            this.exe = false;
        }

        public void execute()
        {
            this.hull.Clear();
            this.exe = true;
            if (this.points.Count == 0)
                return;
            Point p1 = this.points.Where(p => p.x == this.points.Min(min => min.x)).First(), p2;
            do
            {
                this.hull.Add(p1);
                p2 = this.points[0];
                for (int i = 1; i < this.points.Count; i++)
                {
                    if ((p1 == p2)
                        || (GeometryUtils.findSide(p1, p2, this.points[i]) == 1)) //!coliniar
                    {
                        p2 = this.points[i];
                    }
                }
                p1 = p2;
            }
            while (p2 != this.hull[0]);
        }

        public bool wasExecuted()
        {
            return this.exe;
        }

        public List<Point> getResult()
        {
            return this.hull;
        }
    }
}
