using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class Graham : HullMethod
    {
        private List<Point> points,hull;
        private bool exe;

        public Graham()
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
            this.points.Sort();
            
            foreach (Point p in points)
            {
                while (this.hull.Count >= 2 && !GeometryUtils.check(this.hull[this.hull.Count - 2], this.hull[this.hull.Count - 1], p) /*!Sens antitrigonometric*/)
                {
                    this.hull.RemoveAt(this.hull.Count - 1);
                }
                this.hull.Add(p);
            }
            Point pt;
            int t = this.hull.Count + 1;
            for (int i = this.points.Count - 1; i >= 0; i--)
            {
                pt = this.points[i];
                while (this.hull.Count >= t && !GeometryUtils.check(this.hull[this.hull.Count - 2], this.hull[this.hull.Count - 1], pt)/*!Sens antitrigonometric*/)
                {
                    this.hull.RemoveAt(this.hull.Count - 1);
                }
                this.hull.Add(pt);
            }
            this.hull.RemoveAt(this.hull.Count - 1);
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
