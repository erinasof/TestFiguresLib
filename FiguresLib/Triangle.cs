using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib
{
	public class Triangle : Figure
	{
		private double _side1;
		private double _side2;
		private double _side3;

		public double Side1
		{
			get { return _side1; }
			set
			{
				_side1 = value;
				if (!IsBlocked)
                {
					CheckLegal();
                }
			}
		}

		public double Side2
		{
			get { return _side2; }
			set
			{
				_side2 = value;
				if (!IsBlocked)
				{
					CheckLegal();
				}
			}
		}

		public double Side3
		{
			get { return _side3; }
			set
			{
				_side3 = value;
				if (!IsBlocked)
				{
					CheckLegal();
				}
			}
		}

		public Triangle(double side1, double side2, double side3)
		{
			Block();
			Side1 = side1;
			Side2 = side2;
			Side3 = side3;
			Unblock();
		}

        protected override void CheckLegal()
        {
			if (Side1 <= 0 || Side2 <= 0 || Side3 <= 0)
			{
				throw new ArgumentException(string.Format("Figure with sides {0}, {1}, {2} has wrong sides value(s)", Side1, Side2, Side3));
			}

			if (IsNotTriangle(Side1, Side2, Side3))
			{
				throw new ArgumentException(string.Format("Figure with sides {0}, {1}, {2} is not a triangle", Side1, Side2, Side3));
			}
		}

        bool IsNotTriangle(double side1, double side2, double side3)
		{
			return side1 > side2 + side3 ||
				side2 > side1 + side3 ||
				side3 > side2 + side1;
		}

		public bool IsRightAngled()
		{
			return Side1 * Side1 == Side2 * Side2 + Side3 * Side3 ||
				Side2 * Side2 == Side1 * Side1 + Side3 * Side3 ||
				Side3 * Side3 == Side1 * Side1 + Side2 * Side2;
		}
	}
}
