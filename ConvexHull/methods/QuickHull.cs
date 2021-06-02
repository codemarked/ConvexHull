using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class QuickHull : HullMethod
    {
        private List<Point> points, hull;
        private bool exe;

        public QuickHull()
        {
            this.exe = false;
            this.points = new List<Point>();
            this.hull = new List<Point>();
        }

        public void initialize(List<Point> points)
        {
            this.points = points;
            this.exe = false;
            this.hull.Clear();
        }

        public void execute()
        {
            this.hull.Clear();
            this.exe = true;
            if (points.Count < 4)
            {
                hull = new List<Point>(this.points);
                return;
            }

            Point min = this.points[0], max = this.points[0];
            for (int i = 1; i < this.points.Count; i++)
            {
                if (this.points[i].x < min.x)
                    min = this.points[i];
                if (this.points[i].x > max.x)
                    max = this.points[i];
            }

            this.hull.Add(min);
            this.hull.Add(max);

            List<Point> left = new List<Point>();
            List<Point> right = new List<Point>();

            for (int i = 0; i < this.points.Count; i++)
            {
                Point p = this.points[i];
                if (GeometryUtils.findSide(min, max, p) == 1)
                    left.Add(p);
                else if (GeometryUtils.findSide(min, max, p) == -1)
                    right.Add(p);
            }
            FindHull(left, min, max);
            FindHull(right, max, min);
        }

        private void FindHull(List<Point> points, Point a, Point b)
        {
            int pos = hull.IndexOf(b);

            if (points.Count == 0)
                return;

            if (points.Count == 1)
            {
                Point pp = points[0];
                hull.Insert(pos, pp);
                return;
            }

            int dist = int.MinValue;
            int point = 0;

            for (int i = 0; i < points.Count; i++)
            {
                Point pp = points[i];
                int distance = GeometryUtils.lineDist(a, b, pp);
                if (distance > dist)
                {
                    dist = distance;
                    point = i;
                }
            }

            Point p = points[point];
            hull.Insert(pos, p);
            List<Point> ap = new List<Point>();
            List<Point> pb = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                Point pp = points[i];
                if (GeometryUtils.findSide(a, p, pp) == 1)
                {
                    ap.Add(pp);
                }
            }
            for (int i = 0; i < points.Count; i++)
            {
                Point pp = points[i];
                if (GeometryUtils.findSide(p, b, pp) == 1)
                {
                    pb.Add(pp);
                }
            }
            FindHull(ap, a, p);
            FindHull(pb, p, b);
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
