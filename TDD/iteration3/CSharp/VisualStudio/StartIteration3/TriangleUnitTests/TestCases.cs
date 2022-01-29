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


        [TestCase(0, 0, 0, false, TestName = "All zeroes")]
        [TestCase(0, 0, 1, false, TestName = "Zero combination 1")]
        [TestCase(0, 1, 0, false, TestName = "Zero combination 2")]
        [TestCase(1, 0, 0, false, TestName = "Zero combination 3")]
        [TestCase(0, 1, 1, false, TestName = "Zero combination 4")]
        [TestCase(1, 0, 1, false, TestName = "Zero combination 5")]
        [TestCase(1, 1, 0, false, TestName = "Zero combination 6")]
        [TestCase(-1, -1, -1, false, TestName = "All negatives")]
        [TestCase(1, 1, -1, false, TestName = "Negatives combination 1")]
        [TestCase(1, -1, 1, false, TestName = "Negatives combination 2")]
        [TestCase(-1, 1, 1, false, TestName = "Negatives combination 3")]
        [TestCase(1, -1, -1, false, TestName = "Negatives combination 4")]
        [TestCase(-1, 1, -1, false, TestName = "Negatives combination 5")]
        [TestCase(-1, -1, 1, false, TestName = "Negatives combination 6")]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, true, TestName = "All max values")]
        [TestCase(1, double.MaxValue, double.MaxValue, false, TestName = "MaxValue case 1")]
        [TestCase(double.MaxValue, 1, double.MaxValue, false, TestName = "MaxValue case 2")]
        [TestCase(double.MaxValue, double.MaxValue, 1, false, TestName = "MaxValue case 3")]
        [TestCase(1, 1, 2, false, TestName = "Not a triangle case 1")]
        [TestCase(2, 1, 1, false, TestName = "Not a triangle case 2")]
        [TestCase(1, 2, 1, false, TestName = "Not a triangle case 3")]
        [TestCase(0.5, 0.5, 2, false, TestName = "Not a triangle case 4")]
        [TestCase(0.5, 2, 0.5, false, TestName = "Not a triangle case 5")]
        [TestCase(2, 0.5, 0.5, false, TestName = "Not a triangle case 6")]
        [TestCase(1, 1, 1, true, TestName = "Is a triangle 1, 1, 1")]
        [TestCase(1, 2, 2, true, TestName = "Is a triangle 1, 2, 2")]
        [TestCase(3, 4, 5, true, TestName = "Is a triangle 3, 4, 5")]
        public void TriangleInputs(double side1, double side2, double side3, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, triangles.IsTriangle(side1, side2, side3));
        }



        [TestCase(1, 1, 1, "equilateral")]
        [TestCase(1, 2, 2, "isosceles")]
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
