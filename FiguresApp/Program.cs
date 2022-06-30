using System;
using System.Collections.Generic;
using FiguresLib;

namespace FiguresApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Point>()
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(1, 0)
            };
            var rfigure = new RandomFigure(list);

            list[2] = new Point(1, 1);

            var p = rfigure.VList[2];
        }
    }
}
