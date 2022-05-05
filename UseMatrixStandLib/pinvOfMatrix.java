package com.test;

import org.ujmp.core.DenseMatrix;
import org.ujmp.core.Matrix;

public class pinvOfMatrix 
{
	public static void pinvMatrix(double[][] a)
	{
		Matrix A = DenseMatrix.Factory.importFromArray(a);
		System.out.print("A:");
		System.out.println(A);		
		Matrix pinvA = A.pinv();
		System.out.print("A的广义逆矩阵为:");
		System.out.println(pinvA);
	}
	public static void main(String[] args)
	{
		double[][] a1 = {{1, 2, 3, 4, 5, 6, 7},{8, 9, 10, 11, 12, 13, 14},{15, 16, 17, 18, 19, 20, 21},{22, 23, 24, 25, 26, 27, 28},{29, 30, 31, 32, 33, 34, 35}};
		pinvMatrix(a1);
		double[][] a2={{11, 12, 13, 14},{15, 16, 17, 18},{19, 20, 21, 22},{23, 24, 25, 26},{27, 28, 29, 30},{31, 32, 33, 34}};
		pinvMatrix(a2);
	}
}
