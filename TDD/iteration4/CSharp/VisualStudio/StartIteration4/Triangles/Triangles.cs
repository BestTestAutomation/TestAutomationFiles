using System;
using System.Collections.Generic;

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
                return true;
            }
            catch 
            {
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
                    result += "isosceles";
                }
                else
                {
                    result += "scalene";
                }
            }
            return result;
        }


        public string OutputTriangleForm(double side1, double side2, double side3)
        {
            string result = $"{side1}, {side2}, {side3} forms a triangle of form: ";
            // If this is not a triangle, bail out
            if (!IsTriangle(side1, side2, side3))
            {
                throw new Exception($"{side1}, {side2}, {side3} does not describe a valid triangle");
            }
            List<decimal> angles = InteriorAngles(side1, side2, side3);
            string triangleForm = string.Empty;
            foreach (decimal angle in angles)
            {
                if (Math.Round(angle) > 90)
                {
                    triangleForm = "obtuse triangle";
                    break;
                }
                else if (Math.Round(angle) == 90)
                {
                    triangleForm = "right triangle";
                    break;
                }
                else
                {
                    triangleForm = "acute triangle";
                }
            }
            return result + triangleForm;
        }


        public List<decimal> InteriorAngles(double side1, double side2, double side3)
        {
            if (!IsTriangle(side1, side2, side3))
            {
                throw new Exception($"{side1}, {side2}, {side3} does not describe a valid triangle");
            }
            // Specifying decimal as the number type helps us get around rounding errors
            // and when doing comparisons
            List<decimal> angles = new List<decimal>();
            /*
             * The Law of cosines to get the angle:
             * cos(C) = (a^2 + b^2 - c^2) / (2ab)
             * For each angle, shuffle around sides in the formula as needed
             * We will treat things as:
             *  a = side1, b = side2, c = side3
             * To get C (or B, or A), we need to take the inverse cosine (Math.Acos in C#)
             * of the term on the right hand of the equation.
             * The resultant formula components:
             * Math.Acos() - the inverse cosine
             * Math.Pow(side#, 2)
             * To try and be clear, we will construct each part of the equation as its own variable,
             * then solve for th angle using Math.Acos();
             */
            double side1Squared = Math.Pow(side1, 2);
            double side2Squared = Math.Pow(side2, 2);
            double side3Squared = Math.Pow(side3, 2);
            double angleForSide1 = Math.Acos((side2Squared + side3Squared - side1Squared) / (2 * side2 * side3));
            double angleForSide2 = Math.Acos((side3Squared + side1Squared - side2Squared) / (2 * side3 * side1));
            double angleForSide3 = Math.Acos((side1Squared + side2Squared - side3Squared) / (2 * side1 * side2));
            angles.Add((decimal)angleForSide1 * (decimal)(180 / Math.PI));
            angles.Add((decimal)angleForSide2 * (decimal)(180 / Math.PI));
            angles.Add((decimal)angleForSide3 * (decimal)(180 / Math.PI));
            angles.Sort();
            return angles;
        }
    }
}
