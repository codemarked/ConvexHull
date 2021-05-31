using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class DivideHull : HullMethod
    {
        private List<Point> points, hull;
        private bool exe;

        public DivideHull()
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
            this.exe = true;
            //exec
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
