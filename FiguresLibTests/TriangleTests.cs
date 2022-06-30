using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib.Tests
{
    [TestClass()]
    public class TriangleTests
    {
        [TestMethod()]
        public void TriangleTest_ValidValues()
        {
            double side1 = 3;
            double side2 = 4;
            double side3 = 5;
            double p = (side1 + side2 + side3) / 2;
            double expectedSquere = Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));

            var CurTriangle = new Triangle(side1, side2, side3);
            double triangleSquere = CurTriangle.GetSquare();

            Assert.AreEqual(triangleSquere, expectedSquere);
        }

        [TestMethod()]
        public void TriangleTest_WrongValues()
        {
            double side1 = 10;
            double side2 = 15;
            double side3 = -2;

            void CurTriangleAction() => new Triangle(side1, side2, side3);

            Assert.ThrowsException<ArgumentException>(CurTriangleAction);
        }

        [TestMethod()]
        public void TriangleTest_NotATriangleValues()
        {
            double side1 = 10;
            double side2 = 20;
            double side3 = side1 + side2 + 5;

            void CurTriangleAction() => new Triangle(side1, side2, side3);

            Assert.ThrowsException<ArgumentException>(CurTriangleAction);
        }

        [TestMethod()]
        public void TriangleTest_IsRightAngledTriangle()
        {
            double side1 = 3;
            double side2 = 4;
            double side3 = 5;

            var CurTriangle = new Triangle(side1, side2, side3);

            Assert.IsTrue(CurTriangle.IsRightAngled());
        }

        [TestMethod()]
        public void TriangleTest_IsNotRightAngledTriangle()
        {
            double side1 = 12;
            double side2 = 13;
            double side3 = 14;

            var CurTriangle = new Triangle(side1, side2, side3);

            Assert.IsFalse(CurTriangle.IsRightAngled());
        }

        [TestMethod()]
        public void TriangleTest_BlockSucceed()
        {
            double side1 = 1;
            double side2 = 2;
            double side3 = 3;

            var CurTriangle = new Triangle(side1, side2, side3);

            // проверка на валидность точек происходит после разблокировки фигуры
            CurTriangle.Block();
            CurTriangle.Side1 = 6;
            CurTriangle.Side3 = 4;
            CurTriangle.Unblock();            
        }

        [TestMethod()]
        public void TriangleTest_BlockFails()
        {
            double side1 = 1;
            double side2 = 2;
            double side3 = 3;

            var CurTriangle = new Triangle(side1, side2, side3);

            // создаёт исключение после присвоения Side1 ведь не было блокировки
            // и вызовется CheckLegal на фигуре, не являющейся треугольником
            void SetParams()
            {
                CurTriangle.Side1 = 6;
                CurTriangle.Side3 = 4;
            }

            Assert.ThrowsException<ArgumentException>(SetParams);
        }
    }
}
