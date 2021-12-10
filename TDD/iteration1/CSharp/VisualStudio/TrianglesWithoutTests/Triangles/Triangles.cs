using System;

namespace Triangles
{
    public class Triangles
    {
        public bool IsTriangle(double side1, double side2, double side3)
        {
            // If we encounter ANY exception, we return false per the specifications
            try
            {
                // If any one of the sides is 0 or less, this is not a triangle
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
                else if (
                    side1 == double.MaxValue ||
                    side2 == double.MaxValue ||
                    side3 == double.MaxValue
                    )
                {
                    return false;
                }
                // The lengths of the sides don't allow for a triangle
                else if (
                    side1 + side2 <= side3 ||
                    side1 + side3 <= side2 ||
                    side2 + side3 <= side1
                    )
                {
                    return false;
                }
                // All other cases are triangles (is this true?)
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
