package com.test;

public class MatrixOperator 
{
	private static double[][] Trans(double[][] a)//矩阵转置
	{
		double[][] mat = null;
		if (a.length > 0 && a[0].length > 0)
		{
			mat = new double[a[0].length][a.length];
			for (int i = 0; i < a[0].length; i++)
			{
				for (int j = 0; j < a.length; j++)
				{
					mat[i][j] = a[j][i];
				}
			}
		}
		return mat;
	}
	private static double[][] Multiply(double[][] a, double[][] b)//两个矩阵相乘
	{
		double[][] c = null;
		if (a[0].length != b.length)
			System.out.println("两个矩阵无法相乘！");
		else
		{
			c = new double[a.length][b[0].length];
			for (int i = 0; i < c.length; i++)
			{
				for (int j = 0; j < c[0].length; j++)
				{
					c[i][j] = 0;
					for (int k = 0; k < b.length; k++)
						c[i][j] += a[i][k] * b[k][j];
				}
			}
		}
		return c;
	}
	private static int Rank(double[][] a0)//求矩阵的秩
	{
		int r = 0, m1 = 0,m=a0.length,n=a0[0].length;
		double[][] a = new double[m][n];
		for (int i = 0; i < m; i++)
			for (int j = 0; j < n; j++)
				a[i][j] = a0[i][j];
		if (m > n)
			m1 = n;
		else
			m1 = m;
		for (int i = 0; i < m1; i++)//从上到下，将主对角线元素化为1，左下角矩阵化为0
		{
			double aii = a[i][i];//记录每行主对角线元素
			if (Math.abs(aii) <= (1e-6))//如果为0
			{
				int k;
				for (k = i + 1; k < m; k++)
				{
					double aki = a[k][i];//记录每行对应第i行主对角线元素的元素
					if (Math.abs(aki) >(1e-6))//如果不为0
					{
						for (int j = 0; j < n; j++)
						{
							a[i][j] -= a[k][j];
						}
						double ai = a[i][i];
						for (int j = 0; j < n; j++)
							a[i][j] /= ai;
						break;
					}
				}
			}
			else  //主元素不为0
			{
				for (int j = 0; j < n; j++)
					a[i][j] /= aii;
			}
			for (int l = i + 1; l < m; l++)
			{
				double ali = a[l][i];
				if (Math.abs(ali) >(1e-6))
				{
					for (int j = 0; j < n; j++)
					{
						a[l][j] -= ali * a[i][j];
					}
				}
			}
		}
		for (int i = m1 - 1; i >= 0; i--)//从下到上，将右上角矩阵化为0
		{
			for (int k = i - 1; k >= 0; k--)
			{
				double aki = a[k][i];
				if (Math.abs(aki) > (1e-6))
				{
					for (int j = m1-1; j >= 0; j--)
					{
						a[k][j] -= aki * a[i][j];
					}
				}
			}
		}
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (Math.abs(a[i][j]) >(1e-6))
				{
					r++; break;
				}
			}
		}
		return r;
	}
	private static double[][] inv(double[][] a0)//方阵求逆
	{
		double[][] c = null;
		int m = a0.length, n = a0[0].length;
		if (m != n)
		{
			System.out.println("非方阵，不可逆！");
		}
		else
		{
			if (Rank(a0) != m)
				System.out.println("该方阵为奇异矩阵，不可逆！");
			else
			{
				double[][] a = new double[m][n];
				for (int i = 0; i < m; i++)
					for (int j = 0; j < n; j++)
						a[i][j] = a0[i][j];
				c = new double[m][n];
				for (int i = 0; i < m; i++)
				{
					for (int j = 0; j < n; j++)
					{
						if (i == j) c[i][j] = 1;
						else c[i][j] = 0;
					}
				}
				for (int i = 0; i < m; i++)
				{
					double aii = a[i][i];//记录每行主对角线元素
					if (Math.abs(aii) <= (1e-6))//如果为0
					{
						int k;
						for (k = i + 1; k < m; k++)
						{
							double aki = a[k][i];//记录每行主对角线元素
							if (Math.abs(aki) >(1e-6))//如果不为0
							{
								for (int j = 0; j < n; j++)
								{
									a[i][j] -= a[k][j];
									c[i][j] -= c[k][j];
								}
								aii = a[i][i];
								for (int j = 0; j < n; j++)
								{
									a[i][j] /= aii;
									c[i][j] /= aii;
								}
								break;
							}
						}
					}
					else//如果不为0
					{
						for (int j = 0; j < n; j++)
						{
							a[i][j] /= aii;
							c[i][j] /= aii;
						}
					}
					for (int l = i + 1; l < m; l++)
					{
						double ali = a[l][i];
						if (Math.abs(ali) >(1e-6))
						{
							for (int j = 0; j < n; j++)
							{
								a[l][j] -= ali * a[i][j];
								c[l][j] -= ali * c[i][j];
							}
						}
					}
				}
				for (int i = m - 1; i >= 0; i--)
				{
					for (int k = i - 1; k >= 0; k--)
					{
						double aki = a[k][i];
						if (Math.abs(aki) > (1e-6))
						{
							for (int j = n - 1; j >= 0; j--)
							{
								a[k][j] -= aki * a[i][j];
								c[k][j] -= aki * c[i][j];
							}
						}
					}
				}
			}
		}
		return c;
	}
	private static double[][] pinv(double[][] a0)//矩阵求广义逆
	{
		int m = a0.length,n = a0[0].length;
		double[][] A = new double[m][n];
		for (int i = 0; i < m; i++)
			for (int j = 0; j < n; j++)
				A[i][j] = a0[i][j];
		int M = 0;
		if (m > n) 
			M = n;
		else 
			M = m;
		double[][] E = new double[m][m];
		for(int i=0;i<m;i++)
			for(int j=0;j<m;j++)
				if(i==j)
					E[i][j] = 1;
				else
					E[i][j] = 0;
		for (int i = 0; i < M; i++)
		{
			double aii = A[i][i];//记录每行主对角线元素
			if (Math.abs(aii) <= (1e-6))//如果为0
			{
				int k;
				for (k = i + 1; k < m; k++)
				{
					double aki = A[k][i];//记录每行对应第i行主对角线元素的元素
					if (Math.abs(aki) >(1e-6))//如果不为0
					{
						for (int j = 0; j < n; j++)
						{
							A[i][j] -= A[k][j];
						}
						for (int j = 0; j < m; j++)
						{
							E[i][j] -= E[k][j];
						}
					    double ai = A[i][i];
						for (int j = 0; j < n; j++)
						{
							A[i][j] /= ai;
						}
						for (int j = 0; j < m; j++)
						{
							E[i][j] /= ai;
						}
						break;
					}
				}
			}
			else  //主元素不为0
			{
				for (int j = 0; j < n; j++)
				{
					A[i][j] /= aii;
				}
				for (int j = 0; j < m; j++)
				{
					E[i][j] /= aii;
				}
			}
			for (int l = i + 1; l < m; l++)
			{
				double ali = A[l][i];
				if (Math.abs(ali) >(1e-6))
				{
					for (int j = 0; j < n; j++)
					{
						A[l][j] -= ali * A[i][j];
					}
					for (int j = 0; j < m; j++)
					{
						E[l][j] -= ali * E[i][j];
					}
				}
				else 
					continue;//为零，则下一行
			}
		}
		for (int i = M - 1; i >= 0; i--)
		{
			for (int k = i - 1; k >= 0; k--)
			{
				double aki = A[k][i];
				if (Math.abs(aki) > (1e-6))
				{
					for (int j = n - 1; j >= 0; j--)
					{
						A[k][j] -= aki * A[i][j];
					}
					for (int j = m - 1; j >= 0; j--)
					{
						E[k][j] -= aki * E[i][j];
					}
				}
			}
		}
		int rA = Rank(A);
		for (int q = 0; q < rA; q++)
		{
			if (Math.abs(A[q][q]) <= (1e-6))//主对角线元素若为0，需要交换行或交换列
			{
				for (int i = q + 1; i < M; i++)
				{
					int biaoji = -1;
					if (A[i][i] == 1)
					{
						biaoji = i;
					}
					else if (Math.abs(A[i][i]) <= (1e-6))//主对角线元素若为0
					{
						for (int j = 0; j < n; j++)
						{
							if (A[i][j] == 1)
							{
								biaoji = i;
								break;
							}
						}
					}
					if (biaoji > 0)//该行有1，已经找到其行号和列号
					{
						//将i行和q行交换
						for (int w = 0; w < n; w++)
						{
							double temp = A[q][w];
							A[q][w] = A[i][w];
							A[i][w] = temp;
						}
						for (int w = 0; w < m; w++)
						{
							double temp1 = E[q][w];
							E[q][w] = E[i][w];
							E[i][w] = temp1;
						}
					}
				}
			}
		}
		int z = rA;
		double[][] G = new double[m][n], P=new double[m][m], P1=new double[m][m], B=new double[m][z], C=new double[z][n];
		for(int i=0;i<m;i++)
			for (int j = 0; j < n; j++)
			{
				G[i][j] = A[i][j];
			}
		for (int i = 0; i<m; i++)
			for (int j = 0; j < m; j++)
			{
				P[i][j] = E[i][j];
			}
		P1 = inv(P);
		for (int i = 0; i < m; i++)
			for (int j = 0; j < z; j++)
				B[i][j] = P1[i][j];
//		int rB = Rank(B);
		for (int i = 0; i < z; i++)
			for (int j = 0; j < n; j++)
				C[i][j] = G[i][j];
//		int rC = Rank(C);
		double[][] Bt = Trans(B), Ct = Trans(C),A1 = Multiply(Multiply(Ct, inv(Multiply(C, Ct))),Multiply(inv(Multiply(Bt, B)), Bt));
		return A1;
	}
	static void Print(double[][] a)//打印矩阵函数
	{
		for(int i=0;i<a.length;i++)
		{
			for(int j=0;j<a[i].length;j++)
				System.out.print(a[i][j]+" ");
			System.out.println();
		}
	}
	public static void main(String[] args)
	{
		double[][] A1= new double[][]{{1,2,3,4,5,6,7},{8,9,10,11,12,13,14}, {15,16,17,18,19,20,21},{22,23,24,25,26,27,28},{29,30,31,32,33,34,35}};
		double[][] pinv_A1 = pinv(A1);
		double[][] A2= new double[][]{{11,12,13,14},{15,16,17,18},{19,20,21,22},{23,24,25,26},{27,28,29,30},{31,32,33,34}};
		double[][] pinv_A2 = pinv(A2);
		System.out.println("原矩阵A：");
		Print(A1);
		System.out.println("矩阵A对应的广义逆矩阵A+：");
		Print(pinv_A1);
		System.out.println("原矩阵A：");
		Print(A2);
		System.out.println("矩阵A对应的广义逆矩阵A+：");
		Print(pinv_A2);
	}
}
