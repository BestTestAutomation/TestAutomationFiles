import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.CsvSource;
import org.junit.jupiter.params.provider.MethodSource;
import org.junit.jupiter.params.provider.ValueSource;

import java.util.stream.Stream;


class trianglesTest {
	triangles triangle;
	
	@BeforeEach
	void Setup() {
		triangle = new triangles();
	}
	
// @CsvSource({"1,1,1", "1,2,2", "3,4,5", "Double.MAX_VALUE,Double.MAX_VALUE,Double.MAX_VALUE"})
	@Test
	void AreValidTrianglesSimple() {
		assertTrue(triangle.IsTriangle(1, 1, 1));
		assertTrue(triangle.IsTriangle(1, 2, 2));
		assertTrue(triangle.IsTriangle(3, 4, 5));
		assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE, Double.MAX_VALUE));
	}
	
	@ParameterizedTest
	// @ValueSource(doubles = {1,1,1,1,1,2,3,4,5,Double.MAX_VALUE,Double.MAX_VALUE,Double.MAX_VALUE})
	@CsvSource({"1,1,1", "1,2,2", "3,4,5"})
	void AreValidTrianglesCSV(double side1, double side2, double side3) {
		assertTrue(triangle.IsTriangle(side1, side2, side3));
	}

	@ParameterizedTest(name = "{index} => array   = {0} )") 
	@MethodSource("validSides")
	void AreValidTrianglesParameterized(double[] sides) {
		assertTrue(triangle.IsTriangle(sides[0], sides[1], sides[2]));
	}

	@ParameterizedTest(name = "{index} => array   = {0} ): sides{0} is {2}") 
	@MethodSource("validSides")
	void AreValidTrianglesParameterizedWithNames(double[] sides, boolean isValid, String result) {
		if(isValid) {
			assertTrue(triangle.IsTriangle(sides[0], sides[1], sides[2]));
		}
		else {
			assertFalse(triangle.IsTriangle(sides[0], sides[1], sides[2]));
		}
	}
	

	private static Stream<Arguments> validSides() {
		return Stream.of(
				Arguments.of((new double[] {1, 1, 1}), true, "valid"),
				Arguments.of((new double[] {1, 2, 2}), true, "valid"),
				Arguments.of((new double[] {3, 4, 5}), true, "valid"),
				Arguments.of((new double[] {Double.MAX_VALUE, Double.MAX_VALUE, Double.MAX_VALUE}), true, "valid"),
				Arguments.of((new double[] {0, 0, 0}), false, "not valid"),
				Arguments.of((new double[] {0, 0, 1}), false, "not valid"),
				Arguments.of((new double[] {0, 1, 1}), false, "not valid"),
				Arguments.of((new double[] {1, 0, 0}), false, "not valid"),
				Arguments.of((new double[] {1, 0, 1}), false, "not valid"),
				Arguments.of((new double[] {1, 1, 0}), false, "not valid"),
				Arguments.of((new double[] {0, 1, 0}), false, "not valid"),
				Arguments.of((new double[] {Double.MAX_VALUE, Double.MAX_VALUE/2 + 1, Double.MAX_VALUE/2 + 1}), false, "not valid")
				);
		}
	

}
