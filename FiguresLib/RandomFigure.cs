using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FiguresLib
{
	[DebuggerDisplay("({X},{Y})")]
	public class Point
	{
		public double X;
		public double Y;

		public Point(double _x, double _y)
		{
			X = _x;
			Y = _y;
		}
	}

	/// <summary>
	/// Фигура не должна иметь пересечений
	/// Обход вершин фигуры должен производиться по часовому направлению
	/// Положим, что количество точек определяется при инициализации и не может меняться
	/// </summary>
	public class RandomFigure : Figure
	{
		public List<Point> VList { get;	private set; }

		public Point this[int index]
		{
			get
			{
				return VList[index];
			}
            set
            {
				VList[index] = value;
				if (!IsBlocked)
                {
					CheckLegal();
                }
            }
		}

		public RandomFigure(List<Point> vList)
		{
			Block();
			VList = new List<Point>(vList);
			Unblock();
		}

		protected override void CheckLegal()
        {
			int size = VList.Count;
			bool isWrong = false;

			// проверить, корректна ли фигура, составляемая списком точек - нет ли самопересечений!!!
			for (int i = 0; i < size; i++)
			{
				int v11 = i;
				int v12 = i == size - 1 ? 0 : i + 1;

				for (int j = 0; j < size; j++)
				{
					int v21 = j;
					int v22 = j == size - 1 ? 0 : j + 1;

					if (v11 == v21 || v11 == v22 || v12 == v21)
					{
						// совпадает со стороной или является смежной
						continue;
					}

					Point point = GeometryHelper.GetSidesIntersection(VList[v11], VList[v12], VList[v21], VList[v22]);
					if (point != null)
					{						
						isWrong = true;
						break;
					}
				}
			}

			if (isWrong)
			{
				throw new ArgumentException("Figure has selfintersection(s)");
			}

			if (!GeometryHelper.DirectionIsClockwise(VList))
			{
				throw new ArgumentException("Figure points direction is not clockwise");
			}
		}
	}
}
