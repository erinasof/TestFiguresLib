using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib.Tests
{
    [TestClass()]
    public class GeometryHelperTests
    {
        [TestMethod()]
        public void GetTriangleSquereByCoordinates_RightAngledTriangeSIs6()
        {
            double s = GeometryHelper.GetTriangleSquareByCoordinates(new Point(1, 1), new Point(1, 5), new Point(4, 1));
            Assert.AreEqual(s, 6);
        }

        [TestMethod()]
        public void GetLengthByPoints_10()
        {
            Assert.AreEqual(10, GeometryHelper.GetLengthByPoints(new Point(1, 1), new Point(7, 9)));
        }

        [TestMethod()]
        public void DirectionIsClockwise_IsFalse()
        {
            Assert.IsFalse(GeometryHelper.DirectionIsClockwise(new List<Point>()
                {
                    new Point(2, 3),
                    new Point(2, 1),
                    new Point(4, 1),
                }
            ));
        }

        [TestMethod()]
        public void DirectionIsClockwise_IsTrue()
        {
            Assert.IsTrue(GeometryHelper.DirectionIsClockwise(new List<Point>()
                {
                    new Point(2, 3),
                    new Point(4, 1),
                    new Point(2, 1),
                }
            ));
        }

        [TestMethod()]
        public void PointIsOnSection_True()
        {
            Assert.IsTrue(GeometryHelper.PointIsOnSection(new Point(-1, -4), new Point(-4, -1), new Point(-3, -2)));
        }

        [TestMethod()]
        public void PointIsOnSection_Near()
        {
            Assert.IsTrue(GeometryHelper.PointIsOnSection(new Point(0, 0), new Point(10, 10), new Point(5, 5.00000001)));
        }

        [TestMethod()]
        public void PointIsOnSection_OnLine()
        {
            Assert.IsFalse(GeometryHelper.PointIsOnSection(new Point(-1, -4), new Point(-4, -1), new Point(5, 0)));
        }

        [TestMethod()]
        public void PointIsOnSection_False()
        {
            Assert.IsFalse(GeometryHelper.PointIsOnSection(new Point(-1, -4), new Point(-4, -1), new Point(-3, 2)));
        }

        [TestMethod()]
        public void GetSidesInterception_False()
        {
            Assert.IsNull(GeometryHelper.GetSidesIntersection(
                new Point(-1, 3), new Point(-2, 2),
                new Point(1, 3), new Point(0, 2))
                );
        }

        [TestMethod()]
        public void GetSidesInterception_2x3()
        {
            Point IntersectP = GeometryHelper.GetSidesIntersection(
                new Point(1, 1), new Point(3, 5),
                new Point(1, 4), new Point(4, 1));
            Point ExpectedP = new Point(2, 3);
            
            Assert.IsTrue(Math.Abs(IntersectP.X - ExpectedP.X) < GeometryHelper.e);
            Assert.IsTrue(Math.Abs(IntersectP.Y - ExpectedP.Y) < GeometryHelper.e);
        }
    }
}
