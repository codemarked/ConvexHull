using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
	public class Point : IComparable<Point>
	{
		public static Random random = new Random();
		public int x, y;

		public Point()
		{

		}
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static Point getRandomPoint(int width, int height)
		{
			return new Point(random.Next(10, width - 10), random.Next(10, height - 10));
		}

		public static Point FromDrawPoint(System.Drawing.Point point)
		{
			return new Point(point.X, point.Y);
		}
		public int CompareTo(Point other)
		{
			return this.x.CompareTo(other.x);
		}

		public override String ToString()
		{
			return $"{this.x}, {this.y}";

		}
	}

	public class GeometryUtils
    {
		public static bool check(Point a, Point b, Point c)
		{
			return ((b.x - a.x) * (c.y - a.y)) > ((b.y - a.y) * (c.x - a.x));
		}

		public static int lineDist(Point p1, Point p2, Point p)
		{
			return Math.Abs((p.y - p1.y) * (p2.x - p1.x) -
					   (p2.y - p1.y) * (p.x - p1.x));
		}

		public static int findSide(Point p1, Point p2, Point p)
		{
			int val = (p.y - p1.y) * (p2.x - p1.x) - (p2.y - p1.y) * (p.x - p1.x);
			if (val > 0)
				return 1;
			if (val < 0)
				return -1;
			return 0;
		}
	}
}
