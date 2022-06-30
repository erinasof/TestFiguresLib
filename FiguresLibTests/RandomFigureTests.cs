using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib.Tests
{
    [TestClass()]
    public class RandomFigureTests
    {
        [TestMethod()]
        public void RandomFigureTest_ValuesWithSelfInterception()
        {
            var list = new List<Point>() { new Point(2, 2), new Point(11, 5), new Point(2, 7), new Point(12, 2) };
            void CurRandomFigureAction() => new RandomFigure(list);

            Assert.ThrowsException<ArgumentException>(CurRandomFigureAction);
        }

        [TestMethod()]
        public void RandomFigureTest_RightValues()
        {
            var list = new List<Point>()
            {
                new Point(2, 2),
                new Point(2, 7),
                new Point(6, 7),
                new Point(6, 5),
                new Point(4, 5),
                new Point(4, 2)
            };

            double S = (new RandomFigure(list)).GetSquare();

            Assert.IsTrue(Math.Abs(S - 14) < GeometryHelper.e);
        }

        [TestMethod()]
        public void RandomFigureTest_RightValuesOfCompositeFigure()
        {
            var list = new List<Point>() {
                new Point(3, 1),
                new Point(0, 6),
                new Point(3, 7),
                new Point(4, 9),
                new Point(5, 8),
                new Point(8, 7),
                new Point(9, 9),
                new Point(11, 7),
                new Point(10, 5),
                new Point(9, 2),
                new Point(11, 1),
            };

            double S = (new RandomFigure(list)).GetSquare();

            Assert.IsTrue(Math.Abs(S - 57.5) < GeometryHelper.e);
        }

        [TestMethod()]
        public void RandomFigureTest_ValuesWithoutClockwiseDirection()
        {
            var listWrong = new List<Point>() {
                new Point(11, 1),
                new Point(9, 2),
                new Point(10, 5),
                new Point(11, 7),
                new Point(9, 9),
                new Point(8, 7),
                new Point(5, 8),
                new Point(4, 9),
                new Point(3, 7),
                new Point(0, 6),
                new Point(3, 1),
            };
            
            void CurRandomFigureAction() => new RandomFigure(listWrong);

            Assert.ThrowsException<ArgumentException>(CurRandomFigureAction);
        }

        [TestMethod()]
        public void RandomFigureTest_IndexTest()
        {
            var list = new List<Point>() {
                new Point(0, 0),
                new Point(0, 4),
                new Point(5, 4),
                new Point(5, 0),
            };

            RandomFigure figure = new RandomFigure(list);

            // фигура станет самопересеченной
            void SetIndexOfRandomFigure() => figure[3] = new Point(-1, 1);
            Assert.ThrowsException<ArgumentException>(SetIndexOfRandomFigure);
        }

        [TestMethod()]
        public void RandomFigureTest_BlockSucceeds()
        {
            var list = new List<Point>() {
                new Point(2, 1),
                new Point(2, 3),
                new Point(5, 3),
                new Point(5, 1),
            };

            RandomFigure figure = new RandomFigure(list);

            double S = figure.GetSquare();            

            // проверка на валидность точек происходит после разблокировки фигуры
            figure.Block();
            figure[3] = new Point(4, 4);
            figure[2] = new Point(4, 6);
            figure.Unblock();

            Assert.IsTrue(Math.Abs(S - 6) < GeometryHelper.e);
        }

        [TestMethod()]
        public void RandomFigureTest_RandomFigureFails()
        {
            var list = new List<Point>() {
                new Point(2, 1),
                new Point(2, 3),
                new Point(5, 3),
                new Point(5, 1),
            };

            RandomFigure figure = new RandomFigure(list);            

            // создаёт исключение после присвоения точке figure[3], ведь не было блокировки
            // и вызовется CheckLegal на самопересеченной фигуре
            void SetParams()
            {
                figure[3] = new Point(4, 4);
                figure[2] = new Point(4, 6);
            }

            Assert.ThrowsException<ArgumentException>(SetParams);            
        }

        [TestMethod()]
        public void RandomFigureTest_BlockFails()
        {
            var list = new List<Point>() {
                new Point(2, 1),
                new Point(2, 3),
                new Point(5, 3),
                new Point(5, 1),
            };

            RandomFigure figure = new RandomFigure(list);

            // создаёт исключение при получении площади фигуре в реиме Block
            void SetParams()
            {
                figure.Block();
                figure[3] = new Point(4, 4);                
                var S = figure.GetSquare();
                figure.Unblock();
            }

            Assert.ThrowsException<BlockException>(SetParams);
        }
    }
}