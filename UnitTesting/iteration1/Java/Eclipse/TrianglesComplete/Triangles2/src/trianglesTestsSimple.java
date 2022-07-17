//import static org.junit.jupiter.api.Assertions.*;

import org.junit.Assert;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.*;
import org.junit.jupiter.params.provider.CsvSource;
import org.junit.jupiter.params.provider.ValueSource;

import java.util.Arrays;
import java.util.Collection;

import org.junit.runners.Parameterized;
import org.junit.runners.Parameterized.Parameters;
import org.junit.runner.RunWith;


public class trianglesTestsSimple {

	private triangles triangle;	
	private int side1;
	private int side2;
	private int side3;
	   
	
	@BeforeEach
	void Setup() {
		triangle = new triangles();
	}	
	
	@ParameterizedTest
	@CsvSource(value = {"1, 1, 1", "1, 2, 2", "3, 4, 5", "Double.MAX_VALUE, Double.MAX_VALUE, Double.MAX_VALUE"})
	void areValidTrianglesParameterized(int side1, int side2, int side3) 
	{
		Assert.assertTrue(triangle.IsTriangle(side1, side2, side3));
	}
	
	@Test
	void AreValidTrianglesSimple() {
		Assert.assertTrue(triangle.IsTriangle(1, 1, 1));
		Assert.assertTrue(triangle.IsTriangle(1, 2, 2));
		Assert.assertTrue(triangle.IsTriangle(3, 4, 5));
		Assert.assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE, Double.MAX_VALUE));
	}
	
	@Test
	void AreNotValidTrianglesSimple() {
		Assert.assertFalse(triangle.IsTriangle(0, 0, 0));
		Assert.assertFalse(triangle.IsTriangle(0, 0, 1));
		Assert.assertFalse(triangle.IsTriangle(0, 1, 0));
		Assert.assertFalse(triangle.IsTriangle(1, 0, 0));
		Assert.assertFalse(triangle.IsTriangle(0, 1, 1));
		Assert.assertFalse(triangle.IsTriangle(1, 1, 0));
		Assert.assertFalse(triangle.IsTriangle(1, 0, 1));
		Assert.assertFalse(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE/2 + 1, Double.MAX_VALUE/2 + 1));
		Assert.assertFalse(triangle.IsTriangle(-1, -1, -1));
		Assert.assertFalse(triangle.IsTriangle(1, 1, -1));
		Assert.assertFalse(triangle.IsTriangle(1, -1, 1));
		Assert.assertFalse(triangle.IsTriangle(-1, 1, 1));
		Assert.assertFalse(triangle.IsTriangle(1, -1, -1));
		Assert.assertFalse(triangle.IsTriangle(-1, 1, -1));
		Assert.assertFalse(triangle.IsTriangle(-1, -1, 1));
	}
	
	@Test
	void InterestingCases()
    {
        // What *should* happen here, versus what *does* happen here? Why?
        // Assert.assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE - 1, Double.MAX_VALUE - 1));
        // Assert.assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE + 1, Double.MAX_VALUE + 1));
        // Assert.assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE + Double.MAX_VALUE, Double.MAX_VALUE + Double.MAX_VALUE));
        // Assert.assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE * Double.MAX_VALUE, Double.MAX_VALUE * Double.MAX_VALUE));
    }
	
    @Test
    void TriangleTypes() 
    {
        Assert.assertEquals("1.0, 1.0, 1.0 forms a triangle of type: equilateral", triangle.OutputTriangleType(1, 1, 1));
        Assert.assertEquals("2.0, 2.0, 1.0 forms a triangle of type: isosceles", triangle.OutputTriangleType(2, 2, 1));
        Assert.assertEquals("3.0, 4.0, 5.0 forms a triangle of type: scalene", triangle.OutputTriangleType(3, 4, 5));
        Assert.assertEquals("Not a triangle", triangle.OutputTriangleType(3, 4, 20));
    }

}
