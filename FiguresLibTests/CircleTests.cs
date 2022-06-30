using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib.Tests
{
    [TestClass()]
    public class CircleTests
    {
        [TestMethod()]
        public void CircleTest_ValidValues()
        {
            double radius = 4;
            double expectedSquere = Math.PI * radius * radius;

            var CurCircle = new Circle(radius);
            double circleSquere = CurCircle.GetSquare();
            
            Assert.AreEqual(circleSquere, expectedSquere);
        }

        [TestMethod()]
        public void CircleTest_WrongValues()
        {
            double radius = -4;
            void CurCircleAction() => new Circle(radius);

            Assert.ThrowsException<ArgumentException>(CurCircleAction);
        }
    }
}
