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
            Point leftPoint = this.points.Where(p => p.x == this.points.Min(min => min.x)).First(), endPoint;
            do
            {
                hull.Add(leftPoint);
                endPoint = this.points[0];
                for (int i = 1; i < this.points.Count; i++)
                {
                    if ((leftPoint == endPoint)
                        || (GeometryUtils.findSide(leftPoint, endPoint, this.points[i]) == 1))
                    {
                        endPoint = this.points[i];
                    }
                }
                leftPoint = endPoint;
            }
            while (endPoint != hull[0]);
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
