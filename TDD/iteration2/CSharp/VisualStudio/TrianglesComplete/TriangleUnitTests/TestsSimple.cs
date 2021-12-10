using System;
using NUnit.Framework;
using Triangles;

namespace TriangleUnitTests
{
    [TestFixture]
    public class TestsSimple
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

        [Test]
        public void AllMaxValue()
        {
            Assert.IsTrue(triangles.IsTriangle(double.MaxValue, double.MaxValue, double.MaxValue));
        }


        [Test]
        public void NotTriangles()
        {
            // two short sides equal the long side
            Assert.IsFalse(triangles.IsTriangle(1, 1, 2));
            Assert.IsFalse(triangles.IsTriangle(2, 1, 1));
            Assert.IsFalse(triangles.IsTriangle(1, 2, 1));
            // Two short sides are less than the long side
            Assert.IsFalse(triangles.IsTriangle(0.5, 0.5, 2));
            Assert.IsFalse(triangles.IsTriangle(0.5, 2, 0.5));
            Assert.IsFalse(triangles.IsTriangle(2, 0.5, 0.5));
        }

        [Test]
        public void AreTriangles()
        {
            Assert.IsTrue(triangles.IsTriangle(1, 1, 1));
            Assert.IsTrue(triangles.IsTriangle(1, 2, 2));
            Assert.IsTrue(triangles.IsTriangle(3, 4, 5));
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

        [Test]
        public void TriangleTypes() 
        {
            Assert.AreEqual("1, 1, 1 forms a triangle of type: equilateral", triangles.OutputTriangleType(1, 1, 1));
            Assert.AreEqual("2, 2, 1 forms a triangle of type: isosceles", triangles.OutputTriangleType(2, 2, 1));
            Assert.AreEqual("3, 4, 5 forms a triangle of type: scalene", triangles.OutputTriangleType(3, 4, 5));
            Assert.AreEqual("Not a triangle", triangles.OutputTriangleType(3, 4, 20));
        }

        [TestCase(1, 1, 1, "equilateral")]
        [TestCase(1, 2, 2, "isoceles")]
        [TestCase(3, 4, 5, "scalene")]
        [TestCase(1, 1, 2, "Not a triangle")]
        public void TriangleTypes(double side1, double side2, double side3, string expectedResult)
        {
            string fullString = expectedResult;
            if (expectedResult != "Not a triangle")
            {
                fullString = $"{side1}, {side2}, {side3} forms a triangle of type: " + expectedResult;
            }
            Assert.AreEqual(fullString, triangles.OutputTriangleType(side1, side2, side3));
        }
    }
}
