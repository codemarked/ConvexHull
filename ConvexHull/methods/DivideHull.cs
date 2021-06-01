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
            this.points.Sort();

			this.hull = getHull(this.points).getPoints();
		}

		public Hull getHull(List<Point> pointList)
		{
			if (pointList.Count <= 1)
			{
				Hull result = new Hull(pointList);
				result.setRightMost(pointList[pointList.Count - 1]);
				return result;
			}
			else
			{
				List<List<Point>> sets = divideSet(pointList);

				Hull leftHull = getHull(sets[0]);
				Hull rightHull = getHull(sets[1]);

				return merge(leftHull, rightHull);
			}
		}

		public Hull merge(Hull left, Hull right)
		{
			int rightMost = left.getRightMostIndex();
			int leftMost = right.getLeftMostIndex();

			int currentLeftIndex = rightMost;
			int currentRightIndex = leftMost;

			int upperLeft = -1;
			int upperRight = -1;
			int lowerLeft = -1;
			int lowerRight = -1;

			bool leftIndexChanged = false;
			bool rightIndexChanged = false;
			bool firstRight = true;
			bool firstLeft = true;

			//get upper common tangent
			while (leftIndexChanged || rightIndexChanged || firstLeft || firstRight)
			{
				if (firstRight || leftIndexChanged)
				{
					firstRight = false;
					upperRight = getRightUpper(left, right, currentLeftIndex, currentRightIndex);
					if (upperRight == currentRightIndex)
					{
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else
					{
						rightIndexChanged = true;
						currentRightIndex = upperRight;
					}
				}
				if (firstLeft || rightIndexChanged)
				{
					firstLeft = false;
					upperLeft = getLeftUpper(left, right, currentLeftIndex, currentRightIndex);
					if (upperLeft == currentLeftIndex)
					{
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else
					{
						leftIndexChanged = true;
						currentLeftIndex = upperLeft;
					}
				}
			}

			//get lower common tangentt
			currentLeftIndex = rightMost;
			currentRightIndex = leftMost;

			leftIndexChanged = false;
			rightIndexChanged = false;
			//iterate through at least once
			firstRight = true;
			firstLeft = true;
			while (leftIndexChanged || rightIndexChanged || firstLeft || firstRight)
			{
				if (firstLeft || rightIndexChanged)
				{
					firstLeft = false;
					lowerLeft = getLeftLower(left, right, currentLeftIndex, currentRightIndex);
					if (lowerLeft == currentLeftIndex)
					{
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else
					{
						leftIndexChanged = true;
						currentLeftIndex = lowerLeft;
					}
				}

				if (firstRight || leftIndexChanged)
				{
					firstRight = false;
					lowerRight = getRightLower(left, right, currentLeftIndex, currentRightIndex);
					if (lowerRight == currentRightIndex)
					{
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else
					{
						rightIndexChanged = true;
						currentRightIndex = lowerRight;
					}
				}
			}

			//join points
			List<Point> resultPoints = new List<Point>();
			//add up to (and including) upperLeft
			for (int i = 0; i <= upperLeft; i++)
			{
				resultPoints.Add(left.getPoints()[i]);
			}
			//add up to lowerRight
			for (int i = upperRight; i != lowerRight; i = right.getNextIndex(i))
			{
				resultPoints.Add(right.getPoints()[i]);
			}
			//add lowerRight
			resultPoints.Add(right.getPoints()[lowerRight]);
			//add from lowerLeft to beginning
			for (int i = lowerLeft; i != 0; i = left.getNextIndex(i))
			{
				resultPoints.Add(left.getPoints()[i]);
			}

			return new Hull(resultPoints);
		}
		private int getLeftUpper(Hull left, Hull right, int leftIndex, int rightIndex)
		{
			List<Point> leftPoints = left.getPoints();
			List<Point> rightPoints = right.getPoints();
			while (calculateSlope(rightPoints[rightIndex], leftPoints[left.getPrevIndex(leftIndex)]) <
				  calculateSlope(rightPoints[rightIndex], leftPoints[leftIndex]))
			{
				leftIndex = left.getPrevIndex(leftIndex);
			}
			return leftIndex;
		}

		private int getRightUpper(Hull left, Hull right, int leftIndex, int rightIndex)
		{
			List<Point> rightPoints = right.getPoints();
			List<Point> leftPoints = left.getPoints();
			while (calculateSlope(leftPoints[leftIndex], rightPoints[right.getNextIndex(rightIndex)]) >
				  calculateSlope(leftPoints[leftIndex], rightPoints[rightIndex]))
			{
				rightIndex = right.getNextIndex(rightIndex);
			}

			return rightIndex;
		}

		private int getLeftLower(Hull left, Hull right, int leftIndex, int rightIndex)
		{
			List<Point> leftPoints = left.getPoints();
			List<Point> rightPoints = right.getPoints();
			while (calculateSlope(rightPoints[rightIndex], leftPoints[left.getNextIndex(leftIndex)]) >
				  calculateSlope(rightPoints[rightIndex], leftPoints[leftIndex]))
			{
				leftIndex = left.getNextIndex(leftIndex);
			}
			return leftIndex;
		}

		private int getRightLower(Hull left, Hull right, int leftIndex, int rightIndex)
		{
			List<Point> rightPoints = right.getPoints();
			List<Point> leftPoints = left.getPoints();
			while (calculateSlope(leftPoints[leftIndex], rightPoints[right.getPrevIndex(rightIndex)]) <
				  calculateSlope(leftPoints[leftIndex], rightPoints[rightIndex]))
			{
				rightIndex = right.getPrevIndex(rightIndex);
			}
			return rightIndex;
		}

		private int getIndexForPoint(Point point, Hull hull)
		{
			List<Point> points = hull.getPoints();
			for (int i = 0; i < points.Count; i++)
			{
				if (points[i].Equals(point))
				{
					return i;
				}
			}
			return -100;
		}

		//return a list of a list of points. 
		//The first list will be the left side. 
		//The second will be the right side
		public List<List<Point>> divideSet(List<Point> points)
		{
			List<Point> leftSide = points.Take(points.Count / 2).ToList();
			List<Point> rightSide = points.Skip(points.Count / 2).ToList();
			List<List<Point>> result = new List<List<Point>>();
			result.Add(leftSide);
			result.Add(rightSide);
			return result;
		}

		public Double calculateSlope(Point left, Point right)
		{
			return -(right.y - left.y) / (right.x - left.x);
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
