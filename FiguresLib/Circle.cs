using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib
{
	public class Circle : Figure
	{
		private double _radius;
		public double Radius
        {
			get { return _radius; }
			set
			{
				_radius = value;
				if (!IsBlocked)
					CheckLegal();
			}
        }

		public Circle(double radius)
		{
			Block();
			Radius = radius;
			Unblock();
		}

		protected override void CheckLegal()
        {
			if (Radius < 0)
			{
				throw new ArgumentException(string.Format("{0} is not a valid radius", Radius));
			}
		}
	}
}
