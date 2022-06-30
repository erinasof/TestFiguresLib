using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib
{
	public static class GeometryHelper
	{
		public static double e = 0.000001;

		public static Point GetSidesIntersection(Point A, Point B, Point C, Point D)
		{
			double xo = A.X, yo = A.Y;
			double p = B.X - A.X, q = B.Y - A.Y;
			double x1 = C.X, y1 = C.Y;
			double p1 = D.X - C.X, q1 = D.Y - C.Y;

			if (Math.Abs(q * p1 - q1 * p) < e || Math.Abs(p * q1 - p1 * q) < e)
			{
				return null;
			}

			Point ThePoint = new Point((xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) / (q * p1 - q1 * p),
				(yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) / (p * q1 - p1 * q));

			if (PointIsOnSection(A, B, ThePoint) && PointIsOnSection(C, D, ThePoint))
			{
				return ThePoint;
			}
			else return null;
		}

		public static bool PointIsOnSection(Point A, Point B, Point ThePoint)
		{
			return (ThePoint.X <= Math.Max(A.X, B.X)) && (ThePoint.X >= Math.Min(A.X, B.X)) &&
				(ThePoint.Y <= Math.Max(A.Y, B.Y)) && (ThePoint.Y >= Math.Min(A.Y, B.Y));
		}

		public static int GetFirstAppropriateAngle(List<Point> PList)
		{
			int size = PList.Count;			
			for (int i = 0; i < size; i++)
			{
				int ind1 = i % size;
				int ind2 = (i + 1) % size;
				int ind3 = (i + 2) % size;
				Point P1 = PList[ind1];
				Point P2 = PList[ind2];
				Point P3 = PList[ind3];

				// есть ли самопересечение?
				Point ab = new Point(P2.X - P1.X, P2.Y - P1.Y);
				Point bc = new Point(P3.X - P2.X, P3.Y - P2.Y);
				bool isConcave = (ab.X * bc.Y - ab.Y * bc.X) > 0;

				// лежит ли внутри треугольника какая-то вершина?
				bool isIn = false;
				for (int j = 0; j < size; j++)
				{
					if (ind1 == j || ind2 == j || ind3 == j)
					{
						continue;
					}

					Point Pj = PList[j];
					bool z1 = (P1.X - Pj.X) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - Pj.Y) > 0;
					bool z2 = (P2.X - Pj.X) * (P3.Y - P2.Y) - (P3.X - P2.X) * (P2.Y - Pj.Y) > 0;
					bool z3 = (P3.X - Pj.X) * (P1.Y - P3.Y) - (P1.X - P3.X) * (P3.Y - Pj.Y) > 0;
					if (z1 == z2 && z1 == z3 && z2 == z3)
                    {
						isIn = true;
					}
				}

				if (!isConcave && !isIn)
                {
					return ind2;
				}
			}

			return -1;
		}

		public static double GetTriangleSquareBySides(double side1, double side2, double side3)
		{
			double p = (side1 + side2 + side3) / 2;
			return Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
		}

		public static double GetTriangleSquareByCoordinates(Point P1, Point P2, Point P3)
		{
			return GetTriangleSquareBySides(GetLengthByPoints(P1, P2), GetLengthByPoints(P2, P3), GetLengthByPoints(P3, P1));
		}

		public static double GetLengthByPoints(Point First, Point Second)
		{
			return Math.Sqrt(Math.Pow((Second.X - First.X), 2) + Math.Pow((Second.Y - First.Y), 2));
		}

		public static bool DirectionIsClockwise(List<Point> Points)
		{
			int size = Points.Count;
			double p = 0;
			for (int i = 0; i < size; i++)
			{
				Point p1 = Points[i];
				Point p2 = Points[(i + 1) % size];
				p += (p2.X - p1.X) * (p2.Y + p1.Y);
			}
			return p > 0;
		}

		public static double GetSquareByPointsList(List<Point> vList)
        {
			double Sum = 0;
			var CurPointsList = new List<Point>(vList);
			while (CurPointsList.Count > 2)
			{
				int ind = GetFirstAppropriateAngle(CurPointsList);
				if (ind == -1)
                {
					break;
				}

				int size = CurPointsList.Count;
				int ind1 = (ind - 1 < 0) ? (ind - 1 + size) : ind - 1;
				int ind2 = ind % size;
				int ind3 = (ind + 1) % size;
				Point P1 = CurPointsList[ind1];
				Point P2 = CurPointsList[ind2];
				Point P3 = CurPointsList[ind3];

				Sum += GetTriangleSquareByCoordinates(P1, P2, P3);

				CurPointsList.RemoveAt(ind2);
			}

			return Sum;
		}

		public static double GetSquare(Figure figure)
        {
			if (figure is Circle circle)
            {
				return Math.PI * circle.Radius * circle.Radius;
			}

			if (figure is Triangle triangle)
            {
				return GetTriangleSquareBySides(triangle.Side1, triangle.Side2, triangle.Side3);
            }

			if (figure is RandomFigure randomFigure)
            {
				return GetSquareByPointsList(randomFigure.VList);
            }

			throw new ArgumentException("Unknown type was used");
        }
	}
}
