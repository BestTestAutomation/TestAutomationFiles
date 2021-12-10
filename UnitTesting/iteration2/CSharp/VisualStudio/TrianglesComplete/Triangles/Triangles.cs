using System;

namespace Triangles
{
    public class Triangles
    {
        public bool IsTriangle(double side1, double side2, double side3)
        {
            try
            {
                // If any one of the sides is 0 or less, is not a triangle
                if (side1 <= 0 || side2 <= 0 || side3 <= 0)
                {
                    return false;
                }
                // If all sides are positive, and are all of maximum value, 
                // this is a valid triangle
                else if (side1 == double.MaxValue &&
                    side1 == side2 && side1 == side3)
                {
                    return true;
                }
                // Overflowing during addition will give us invalid results,
                // so we return a false per the specifications
                // This is not a complete implementation of this case! 
                // Can you see why, and how would you fix it?
                else if (
                    side1 == double.MaxValue ||
                    side2 == double.MaxValue ||
                    side3 == double.MaxValue
                    )
                {
                    return false;
                }
                // The length of the sides don't allow for a triangle
                else if (
                    side1 + side2 <= side3 ||
                    side1 + side3 <= side2 ||
                    side2 + side3 <= side1
                    )
                {
                    return false;
                }
                // All remaining cases are triangles (Is this true?)
                return true;
            }
            catch
            {
                // If we encounter ANY exception, we return false per the specifications
                return false;
            }
        }

        public string OutputTriangleType(double side1, double side2, double side3)
        {
            string result = $"{side1}, {side2}, {side3} forms a triangle of type: ";
            // If this is not a triangle, bail out
            if (!IsTriangle(side1, side2, side3))
            {
                result = "Not a triangle";
            }
            else
            {
                // Now to decide which kind of triangle this is
                if (side1 == side2 && side1 == side3)
                {
                    result += "equilateral";
                }
                else if (side1 == side2 || side1 == side3 || side2 == side3)
                {
                    result += "isoceles";
                }
                else
                {
                    result += "scalene";
                }
            }
            return result;
        }
    }
}
