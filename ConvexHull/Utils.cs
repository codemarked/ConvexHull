using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
	public class SortPointsClockwise : IComparer<Point>
	{
		private Point center;

		public SortPointsClockwise(Point center)
        {
			this.center = center;
		}
		public int Compare(Point a, Point b)
		{
			double a1 = (GeometryUtils.RadianToDegree(Math.Atan2(a.x - center.x, a.y - center.y)) + 360) % 360;
			double a2 = (GeometryUtils.RadianToDegree(Math.Atan2(b.x - center.x, b.y - center.y)) + 360) % 360;
			return (int)(a1 - a2);
		}
	}

	public class SortPointsAntiClockwise : IComparer<Point>
	{
		private Point center;

		public SortPointsAntiClockwise(Point center)
		{
			this.center = center;
		}
		public int Compare(Point a, Point b)
		{
			double a1 = (GeometryUtils.RadianToDegree(Math.Atan2(a.x - center.x, a.y - center.y)) + 360) % 360;
			double a2 = (GeometryUtils.RadianToDegree(Math.Atan2(b.x - center.x, b.y - center.y)) + 360) % 360;
			return (int)(a2 - a1);
		}
	}

	public class Hull
    {
		List<Point> points;
		Point rightMostPoint;
		Dictionary<Point, int> pointIndexDict;

		public Point getNext(Point point)
		{
			int currentIndex = pointIndexDict[point];
			int newIndex = (currentIndex + 1) % points.Count;
			return points[newIndex];
		}

		public int getNextIndex(int currentIndex)
		{
			if (currentIndex == this.points.Count - 1)
			{
				return 0;
			}
			else
			{
				return currentIndex + 1;
			}
		}

		public Point getPrev(Point point)
		{
			try
			{
				int currentIndex = pointIndexDict[point];
				int newIndex = (currentIndex - 1) % points.Count;
				if (newIndex < 0)
				{
					newIndex = ~newIndex + 1;
				}
				return points[newIndex];
			}
			catch (KeyNotFoundException ex)
			{
				Console.WriteLine(ex.Message);
				return new Point(0, 0);
			}
		}

		public int getPrevIndex(int currentIndex)
		{
			if (currentIndex == 0)
			{
				return this.points.Count - 1;
			}
			else
			{
				return currentIndex - 1;
			}
		}

		public Hull()
		{
		}

		public List<Point> getPoints()
		{
			return this.points;
		}

		public Hull(List<Point> points)
		{
			this.pointIndexDict = new Dictionary<Point, int>();
			this.points = points;
			//for (int i = 0; i < points.Count;i++){
			//	pointIndexDict.Add(points[i], i);
			//}
		}

		public void setRightMost(Point point)
		{
			this.rightMostPoint = point;
		}

		public Point getRightMost()
		{
			return this.rightMostPoint;
		}

		public Point getRightMostPoint()
		{
			Point rightMost = new Point();
			foreach (Point point in this.points)
			{
				if (point.x > rightMost.x)
				{
					rightMost = point;
				}
			}
			return rightMost;
		}

		public int getRightMostIndex()
		{
			int max = 0;
			for (int i = 0; i < points.Count; i++)
			{
				if (points[i].x > points[max].x)
				{
					max = i;
				}
			}
			return max;
		}
		public String printPointInfo(int index)
		{
			return "[" + points[index].x + ", " + points[index].y + "]";
		}

		public int getLeftMostIndex()
		{
			int max = 0;
			for (int i = 0; i < points.Count; i++)
			{
				if (points[i].x < points[max].x)
				{
					max = i;
				}
			}
			return max;
		}
	}

	public class Point : IComparable<Point>
	{
		public static Random random = new Random();
		public int x, y;
		public int location;

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
			return new Point(point.X - 17, point.Y - 17);
		}
		public int CompareTo(Point other)
		{
			if (this.x == other.x)
				return this.y.CompareTo(other.y);
			return this.x.CompareTo(other.x);
		}

		public override String ToString()
		{
			return $"{this.x}, {this.y}";
		}

		public static Point getByLocation(List<Point> points, int loc)
        {
			for (int i = 0; i < points.Count; i++) 
            {
				if (points[i].location == loc)
					return points[i];
            }
			return null;
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

		public static Point findCentroid(List<Point> points)
		{
			int x = 0;
			int y = 0;
			foreach (Point p in points)
			{
				x += p.x;
				y += p.y;
			}
			Point center = new Point(0, 0);
			center.x = x / points.Count;
			center.y = y / points.Count;
			return center;
		}

		public static List<Point> sortVerticies(List<Point> points)
		{
			Point center = findCentroid(points);
			points.Sort(new SortPointsClockwise(center));
			return points;
		}

		public static double RadianToDegree(double angle)
		{
			return angle * (180.0 / Math.PI);
		}
	}
}
