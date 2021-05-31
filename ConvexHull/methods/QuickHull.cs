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
        }

        public void execute()
        {
            this.hull.Clear();
            this.exe = true;
            if (this.points.Count < 3)
            {
                return;
            }
            int min_x = 0, max_x = 0;
            for (int i = 1; i < this.points.Count; i++)
            {
                if (this.points[i].x < this.points[min_x].x)
                    min_x = i;
                if (this.points[i].x > this.points[max_x].x)
                    max_x = i;
            }
            recursion(this.points[min_x], this.points[max_x], 1);
            recursion(this.points[min_x], this.points[max_x], -1);
        }

        private void recursion(Point p1, Point p2, int side)
        {
            int ind = -1;
            int max_dist = 0;

            for (int i = 0; i < this.points.Count; i++)
            {
                int temp = GeometryUtils.lineDist(p1, p2, this.points[i]);
                if (GeometryUtils.findSide(p1, p2, this.points[i]) == side && temp > max_dist)
                {
                    ind = i;
                    max_dist = temp;
                }
            }

            if (ind == -1)
            {
                this.hull.Add(p1);
                this.hull.Add(p2);
                return;
            }

            recursion(this.points[ind], p1, -GeometryUtils.findSide(this.points[ind], p1, p2));
            recursion(this.points[ind], p2, -GeometryUtils.findSide(this.points[ind], p2, p1));
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
