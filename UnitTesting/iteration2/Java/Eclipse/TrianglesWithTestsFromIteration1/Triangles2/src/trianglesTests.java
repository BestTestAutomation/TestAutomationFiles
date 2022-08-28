import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

class trianglesTests {
	
	private triangles triangle;

	@BeforeEach
	void setUp() throws Exception {
		triangle = new triangles();
	}

	@Test
	void AreNotValidTrianglesZeros() {
		assertFalse(triangle.IsTriangle(0, 0, 0));
		assertFalse(triangle.IsTriangle(0, 0, 1));
		assertFalse(triangle.IsTriangle(0, 1, 0));
		assertFalse(triangle.IsTriangle(1, 0, 0));
		assertFalse(triangle.IsTriangle(0, 1, 1));
		assertFalse(triangle.IsTriangle(1, 1, 0));
		assertFalse(triangle.IsTriangle(1, 0, 1));
	}

	
	@Test
	void AreNotValidTrianglesNegatives() {
		assertFalse(triangle.IsTriangle(-1, -1, -1));
		assertFalse(triangle.IsTriangle(1, 1, -1));
		assertFalse(triangle.IsTriangle(1, -1, 1));
		assertFalse(triangle.IsTriangle(-1, 1, 1));
		assertFalse(triangle.IsTriangle(1, -1, -1));
		assertFalse(triangle.IsTriangle(-1, 1, -1));
		assertFalse(triangle.IsTriangle(-1, -1, 1));
	}
	
	@Test
	void AreValidTrianglesMaxValues() {
		assertTrue(triangle.IsTriangle(Double.MAX_VALUE, Double.MAX_VALUE, Double.MAX_VALUE));
	}
	
	@Test
	void AreNotValidTriangles() {
		assertFalse(triangle.IsTriangle(1, 1, 2));
		assertFalse(triangle.IsTriangle(1, 1, 3));
		assertFalse(triangle.IsTriangle(1, 1, 2));
		assertFalse(triangle.IsTriangle(1, 2, 1));
		assertFalse(triangle.IsTriangle(2, 1, 1));
		assertFalse(triangle.IsTriangle(1, 1, 3));
		assertFalse(triangle.IsTriangle(1, 3, 1));
		assertFalse(triangle.IsTriangle(3, 1, 1));
	}	
	

	@Test
	void AreValidTrianglesSimple() {
		assertTrue(triangle.IsTriangle(1, 1, 1));
		assertTrue(triangle.IsTriangle(1, 2, 2));
		assertTrue(triangle.IsTriangle(3, 4, 5));
	}

}
