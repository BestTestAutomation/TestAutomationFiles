using System;
using NUnit.Framework;
using Triangles;

namespace TriangleUnitTests
{
    [TestFixture]
    public class TestCases
    {
        Triangles.Triangles triangles;

        [SetUp]
        public void Setup()
        {
            triangles = new Triangles.Triangles();
        }

        [Test]
        public void ZeroInputs()
        {
            Assert.IsFalse(triangles.IsTriangle(0, 0, 0));
            Assert.IsFalse(triangles.IsTriangle(0, 0, 1));
            Assert.IsFalse(triangles.IsTriangle(0, 1, 0));
            Assert.IsFalse(triangles.IsTriangle(1, 0, 0));
            Assert.IsFalse(triangles.IsTriangle(0, 1, 1));
            Assert.IsFalse(triangles.IsTriangle(1, 0, 1));
            Assert.IsFalse(triangles.IsTriangle(1, 1, 0));
        }

        [Test]
        public void NegativeInputs()
        {
            Assert.IsFalse(triangles.IsTriangle(-1, -1, -1));
            Assert.IsFalse(triangles.IsTriangle(1, 1, -1));
            Assert.IsFalse(triangles.IsTriangle(1, -1, 1));
            Assert.IsFalse(triangles.IsTriangle(-1, 1, 1));
            Assert.IsFalse(triangles.IsTriangle(1, -1, -1));
            Assert.IsFalse(triangles.IsTriangle(-1, 1, -1));
            Assert.IsFalse(triangles.IsTriangle(-1, -1, 1));
        }

        [TestCase(1, 1, 2)]
        [TestCase(2, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(0.5, 0.5, 2)]
        [TestCase(0.5, 2, 0.5)]
        [TestCase(2, 0.5, 0.5)]
        public void NotTriangles(double side1, double side2, double side3)
        {
            Assert.IsFalse(triangles.IsTriangle(side1, side2, side3));
        }

        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(3, 4, 5)]
        public void AreTriangles(double side1, double side2, double side3)
        {
            Assert.IsTrue(triangles.IsTriangle(side1, side2, side3));
        }

        [Test]
        public void InterestingCases()
        {
            // What *should* happen here, versus what *does* happen here? Why?
            // Assert.IsTrue(triangles.IsTriangle(double.MaxValue, double.MaxValue - 1, double.MaxValue - 1));
            // Assert.IsTrue(triangles.IsTriangle(double.MaxValue, double.MaxValue + 1, double.MaxValue + 1));
            // Assert.IsTrue(triangles.IsTriangle(double.MaxValue, double.MaxValue + double.MaxValue, double.MaxValue + double.MaxValue));
            // Assert.IsTrue(triangles.IsTriangle(double.MaxValue, double.MaxValue * double.MaxValue, double.MaxValue * double.MaxValue));
        }
    }
}
