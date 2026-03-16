using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tema2_Variant15;

namespace Tema2_UnitTests
{
    [TestClass]
    public class GeometryHelperTests
    {
        private const double DELTA = 0.001;

        // Тесты для пересечения прямой и окружности

        [TestMethod]
        public void TestLineCircle_TwoIntersections()
        {
            // Прямая по оси X, окружность с центром в начале координат
            var result = GeometryHelper.FindLineCircleIntersection(
                -5, 0, 5, 0,   // Прямая от (-5,0) до (5,0)
                0, 0, 3        // Окружность с центром (0,0) и радиусом 3
            );

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(-3.0, result[0].X, DELTA);
            Assert.AreEqual(0.0, result[0].Y, DELTA);
            Assert.AreEqual(3.0, result[1].X, DELTA);
            Assert.AreEqual(0.0, result[1].Y, DELTA);
        }

        [TestMethod]
        public void TestLineCircle_OneIntersection_Tangent()
        {
            // Прямая касается окружности
            var result = GeometryHelper.FindLineCircleIntersection(
                -5, 3, 5, 3,   // Прямая y = 3
                0, 0, 3        // Окружность с центром (0,0) и радиусом 3
            );

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0.0, result[0].X, DELTA);
            Assert.AreEqual(3.0, result[0].Y, DELTA);
        }

        [TestMethod]
        public void TestLineCircle_NoIntersections()
        {
            // Прямая не пересекает окружность
            var result = GeometryHelper.FindLineCircleIntersection(
                -5, 5, 5, 5,   // Прямая y = 5
                0, 0, 3        // Окружность с центром (0,0) и радиусом 3
            );

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLineCircle_NegativeRadius()
        {
            GeometryHelper.FindLineCircleIntersection(
                0, 0, 1, 1,
                0, 0, -3
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLineCircle_SamePoints()
        {
            GeometryHelper.FindLineCircleIntersection(
                1, 1, 1, 1,    // Одинаковые точки
                0, 0, 3
            );
        }

        // Тесты для пересечения двух окружностей

        [TestMethod]
        public void TestCircleCircle_TwoIntersections()
        {
            // Две окружности пересекаются в двух точках
            var result = GeometryHelper.FindCircleCircleIntersection(
                0, 0, 3,       // Окружность 1: центр (0,0), радиус 3
                4, 0, 3        // Окружность 2: центр (4,0), радиус 3
            );

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2.0, result[0].X, DELTA);
            Assert.AreEqual(2.236, result[0].Y, DELTA);
            Assert.AreEqual(2.0, result[1].X, DELTA);
            Assert.AreEqual(-2.236, result[1].Y, DELTA);
        }

        [TestMethod]
        public void TestCircleCircle_OneIntersection_External()
        {
            // Окружности касаются снаружи
            var result = GeometryHelper.FindCircleCircleIntersection(
                0, 0, 3,       // Окружность 1
                6, 0, 3        // Окружность 2
            );

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3.0, result[0].X, DELTA);
            Assert.AreEqual(0.0, result[0].Y, DELTA);
        }

        [TestMethod]
        public void TestCircleCircle_OneIntersection_Internal()
        {
            // Окружности касаются изнутри
            var result = GeometryHelper.FindCircleCircleIntersection(
                0, 0, 5,       // Большая окружность
                3, 0, 2        // Меньшая окружность
            );

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(5.0, result[0].X, DELTA);
            Assert.AreEqual(0.0, result[0].Y, DELTA);
        }

        [TestMethod]
        public void TestCircleCircle_NoIntersections_TooFar()
        {
            // Окружности слишком далеко друг от друга
            var result = GeometryHelper.FindCircleCircleIntersection(
                0, 0, 2,
                10, 0, 2
            );

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCircleCircle_NoIntersections_OneInside()
        {
            // Одна окружность внутри другой
            var result = GeometryHelper.FindCircleCircleIntersection(
                0, 0, 10,
                0, 0, 2
            );

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCircleCircle_Coincident()
        {
            // Окружности совпадают
            GeometryHelper.FindCircleCircleIntersection(
                0, 0, 5,
                0, 0, 5
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCircleCircle_NegativeRadius1()
        {
            GeometryHelper.FindCircleCircleIntersection(
                0, 0, -3,
                4, 0, 3
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCircleCircle_NegativeRadius2()
        {
            GeometryHelper.FindCircleCircleIntersection(
                0, 0, 3,
                4, 0, -3
            );
        }

        [TestMethod]
        public void TestCircleCircle_DifferentQuadrants()
        {
            // Окружности в разных квадрантах
            var result = GeometryHelper.FindCircleCircleIntersection(
                1, 1, 2,
                4, 1, 2
            );

            Assert.AreEqual(2, result.Count);
        }
    }
}
