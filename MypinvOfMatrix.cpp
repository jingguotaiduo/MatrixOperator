#include <iostream>
#include <vector>
using namespace std;

struct Matrix 
{
	public:
		int m = 0;
		int n = 0;
		double **value;
		void Init(double *a, int M, int N)
		{
			if (M > 0 && N > 0)
			{
				m = M;
				n = N;
				value = (double **)malloc(m * sizeof(double *));
				for (int i = 0; i < m; i++)
				{
					value[i] = (double *)malloc(n * sizeof(double));
				}
				for (int i = 0; i < m; i++)
				{
					for (int j = 0; j < n; j++)
					{
						value[i][j] = *(a + i * n + j);
					}
				}
			}
		}
		void Init(int N)
		{
			if ( N > 0)
			{
				m = N;
				n = N;
				value = (double **)malloc(m * sizeof(double *));
				for (int i = 0; i < m; i++)
				{
					value[i] = (double *)malloc(n * sizeof(double));
				}
				for (int i = 0; i < m; i++)
				{
					for (int j = 0; j < n; j++)
					{
						if (i == j)
							value[i][j] = 1;
						else
							value[i][j] = 0;
					}
				}
			}
		}
		void Init(int M,int N)
		{
			if (M > 0 && N > 0)
			{
				m = M;
				n = N;
				value = (double **)malloc(m * sizeof(double *));
				for (int i = 0; i < m; i++)
				{
					value[i] = (double *)malloc(n * sizeof(double));
				}
				for (int i = 0; i < m; i++)
				{
					for (int j = 0; j < n; j++)
					{
						value[i][j] = 0;
					}
				}
			}
		}
		void print()
		{
			if (m > 0 && n > 0)
			{
				for (int i = 0; i < m; i++)
				{
					for (int j = 0; j < n; j++)
					{
						cout << value[i][j] << " ";
					}
					cout << endl;
				}
			}
		}
};
class MatrixOperator
{
	public:
		static Matrix Trans(Matrix a)//矩阵转置
		{
			Matrix mat;
			if (a.m > 0 && a.n > 0)
			{
				mat.m = a.n;
				mat.n = a.m;
				mat.value = (double **)malloc(mat.m * sizeof(double *));
				for (int i = 0; i < mat.m; i++)
				{
					mat.value[i] = (double *)malloc(mat.n * sizeof(double));
				}
				for (int i = 0; i < mat.m; i++)
				{
					for (int j = 0; j < mat.n; j++)
					{
						mat.value[i][j] = a.value[j][i];
					}
				}
			}
			return mat;
		}
		static Matrix Multiply(Matrix a, Matrix b)//两个矩阵相乘
		{
			Matrix c;
			if (a.n != b.m)
			{
				cout << "两个矩阵无法相乘！" << endl;
			}
			else
			{
				c.m = a.m;
				c.n = b.n;
				c.value = (double **)malloc(c.m * sizeof(double *));
				for (int i = 0; i < c.m; i++)
				{
					c.value[i] = (double *)malloc(c.n * sizeof(double));
				}
				for (int i = 0; i < c.m; i++)
				{
					for (int j = 0; j < c.n; j++)
					{
						c.value[i][j] = 0;
						for (int k = 0; k < a.n; k++)
							c.value[i][j] += a.value[i][k] * b.value[k][j];
					}
				}
			}
			return c;
		}
		static int Rank(Matrix a0)//求矩阵的秩
		{
			int r = 0, m1 = 0;
			Matrix a;
			a.Init(a0.m, a0.n);
			for (int i = 0; i < a.m; i++)
				for (int j = 0; j < a.n; j++)
					a.value[i][j] = a0.value[i][j];
			if (a.m > a.n)
				m1 = a.n;
			else
				m1 = a.m;
			for (int i = 0; i < m1; i++)//从上到下，将主对角线元素化为1，左下角矩阵化为0
			{
				double aii = a.value[i][i];//记录每行主对角线元素
				if (abs(aii) <= (1e-6))//如果为0
				{
					int k;
					for (k = i + 1; k < a.m; k++)
					{
						double aki = a.value[k][i];//记录每行对应第i行主对角线元素的元素
						if (abs(aki) >(1e-6))//如果不为0
						{
							for (int j = 0; j < a.n; j++)
							{
								a.value[i][j] -= a.value[k][j];
							}
							double ai = a.value[i][i];
							for (int j = 0; j < a.n; j++)
								a.value[i][j] /= ai;
							break;
						}
					}
				}
				else  //主元素不为0
				{
					for (int j = 0; j < a.n; j++)
						a.value[i][j] /= aii;
				}
				for (int l = i + 1; l < a.m; l++)
				{
					double ali = a.value[l][i];
					if (abs(ali) >(1e-6))
					{
						for (int j = 0; j < a.n; j++)
						{
							a.value[l][j] -= ali * a.value[i][j];
						}
					}
				}
			}
			for (int i = m1 - 1; i >= 0; i--)//从下到上，将右上角矩阵化为0
			{
				for (int k = i - 1; k >= 0; k--)
				{
					double aki = a.value[k][i];
					if (abs(aki) > (1e-6))
					{
						for (int j = a.n-1; j >= 0; j--)
						{
							a.value[k][j] -= aki * a.value[i][j];
						}
					}
				}
			}
			for (int i = 0; i < a.m; i++)
			{
				for (int j = 0; j < a.n; j++)
				{
					if (a.value[i][i] >(1e-6))
					{
						r++; break;
					}
				}
			}
			return r;
		}
		static Matrix inv(Matrix a0)//方阵求逆
		{
			Matrix c;
			if (a0.m != a0.n)
			{
				cout << "非方阵，不可逆！" << endl;
			}
			else
			{
				if (Rank(a0) != a0.m)
					cout << "该方阵为奇异矩阵，不可逆！" << endl;
				else
				{
					Matrix a;
					a.Init(a0.m, a0.n);
					for (int i = 0; i < a.m; i++)
						for (int j = 0; j < a.n; j++)
							a.value[i][j] = a0.value[i][j];
					c.m = a.m;
					c.n = a.n;
					c.value = (double **)malloc(c.m * sizeof(double *));
					for (int i = 0; i < c.m; i++)
					{
						c.value[i] = (double *)malloc(c.n * sizeof(double));
					}
					for (int i = 0; i < c.m; i++)
					{
						for (int j = 0; j < c.n; j++)
						{
							if (i == j) c.value[i][j] = 1;
							else c.value[i][j] = 0;
						}
					}
					for (int i = 0; i < a.m; i++)
					{
						double aii = a.value[i][i];//记录每行主对角线元素
						if (abs(aii) <= (1e-6))//如果为0
						{
							int k;
							for (k = i + 1; k < a.m; k++)
							{
								double aki = a.value[k][i];//记录每行主对角线元素
								if (abs(aki) >(1e-6))//如果不为0
								{
									for (int j = 0; j < a.n; j++)
									{
										a.value[i][j] -= a.value[k][j];
										c.value[i][j] -= c.value[k][j];
									}
									aii = a.value[i][i];
									for (int j = 0; j < a.n; j++)
									{
										a.value[i][j] /= aii;
										c.value[i][j] /= aii;
									}
									break;
								}
							}
						}
						else//如果不为0
						{
							for (int j = 0; j < a.n; j++)
							{
								a.value[i][j] /= aii;
								c.value[i][j] /= aii;
							}
						}
						for (int l = i + 1; l < a.m; l++)
						{
							double ali = a.value[l][i];
							if (abs(ali) >(1e-6))
							{
								for (int j = 0; j < a.n; j++)
								{
									a.value[l][j] -= ali * a.value[i][j];
									c.value[l][j] -= ali * c.value[i][j];
								}
							}
						}
					}
					for (int i = a.m - 1; i >= 0; i--)
					{
						for (int k = i - 1; k >= 0; k--)
						{
							double aki = a.value[k][i];
							if (abs(aki) > (1e-6))
							{
								for (int j = a.n - 1; j >= 0; j--)
								{
									a.value[k][j] -= aki * a.value[i][j];
									c.value[k][j] -= aki * c.value[i][j];
								}
							}
						}
					}
				}
			}
			return c;
		}
		static Matrix pinv(Matrix a0)//矩阵求广义逆
		{
			Matrix A;
			A.Init(a0.m, a0.n);
			for (int i = 0; i < A.m; i++)
				for (int j = 0; j < A.n; j++)
					A.value[i][j] = a0.value[i][j];
			int M = 0;
			if (A.m > A.n) 
				M = A.n;
			else 
				M = A.m;
			Matrix E;
			E.Init(A.m);
			for (int i = 0; i < M; i++)
			{
				double aii = A.value[i][i];//记录每行主对角线元素
				if (abs(aii) <= (1e-6))//如果为0
				{
					int k;
					for (k = i + 1; k < A.m; k++)
					{
						double aki = A.value[k][i];//记录每行对应第i行主对角线元素的元素
						if (abs(aki) >(1e-6))//如果不为0
						{
							for (int j = 0; j < A.n; j++)
							{
								A.value[i][j] -= A.value[k][j];
							}
							for (int j = 0; j < A.m; j++)
							{
								E.value[i][j] -= E.value[k][j];
							}
						    double ai = A.value[i][i];
							for (int j = 0; j < A.n; j++)
							{
								A.value[i][j] /= ai;
							}
							for (int j = 0; j < A.m; j++)
							{
								E.value[i][j] /= ai;
							}
							break;
						}
					}
				}
				else  //主元素不为0
				{
					for (int j = 0; j < A.n; j++)
					{
						A.value[i][j] /= aii;
					}
					for (int j = 0; j < A.m; j++)
					{
						E.value[i][j] /= aii;
					}
				}
				for (int l = i + 1; l < A.m; l++)
				{
					double ali = A.value[l][i];
					if (abs(ali) >(1e-6))
					{
						for (int j = 0; j < A.n; j++)
						{
							A.value[l][j] -= ali * A.value[i][j];
						}
						for (int j = 0; j < A.m; j++)
						{
							E.value[l][j] -= ali * E.value[i][j];
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
					double aki = A.value[k][i];
					if (abs(aki) > (1e-6))
					{
						for (int j = A.n - 1; j >= 0; j--)
						{
							A.value[k][j] -= aki * A.value[i][j];
						}
						for (int j = A.m - 1; j >= 0; j--)
						{
							E.value[k][j] -= aki * E.value[i][j];
						}
					}
				}
			}
			int rA = MatrixOperator::Rank(A);
			for (int q = 0; q < rA; q++)
			{
				if (abs(A.value[q][q]) <= (1e-6))//主对角线元素若为0，需要交换行或交换列
				{
					for (int i = q + 1; i < M; i++)
					{
						int biaoji = -1;
						if (A.value[i][i] == 1)
						{
							biaoji = i;
						}
						else if (abs(A.value[i][i]) <= (1e-6))//主对角线元素若为0
						{
							for (int j = 0; j < A.n; j++)
							{
								if (A.value[i][j] == 1)
								{
									biaoji = i;
									break;
								}
							}
						}
						if (biaoji > 0)//该行有1，已经找到其行号和列号
						{
							//将i行和q行交换
							for (int w = 0; w < A.n; w++)
							{
								double temp = A.value[q][w];
								A.value[q][w] = A.value[i][w];
								A.value[i][w] = temp;
							}
							for (int w = 0; w < A.m; w++)
							{
								double temp1 = E.value[q][w];
								E.value[q][w] = E.value[i][w];
								E.value[i][w] = temp1;
							}
						}
					}
				}
			}
			int z = rA, rB = 0, rC = 0;
			Matrix G, P, P1, B, C;
			G.Init(A.m, A.n);
			P.Init(E.m, E.n);
			for(int i=0;i<A.m;i++)
				for (int j = 0; j < A.n; j++)
				{
					G.value[i][j] = A.value[i][j];
				}
			for (int i = 0; i<E.m; i++)
				for (int j = 0; j < E.n; j++)
				{
					P.value[i][j] = E.value[i][j];
				}
			P1 = inv(P);
			B.Init(A.m, z);
			for (int i = 0; i < A.m; i++)
				for (int j = 0; j < z; j++)
					B.value[i][j] = P1.value[i][j];
			rB = Rank(B);
			C.Init(z, A.n);
			for (int i = 0; i < z; i++)
				for (int j = 0; j < A.n; j++)
					C.value[i][j] = G.value[i][j];
			rC = Rank(C);
			Matrix Bt = Trans(B), Ct = Trans(C),A1 = Multiply(Multiply(Ct, inv(Multiply(C, Ct))),Multiply(inv(Multiply(Bt, B)), Bt));
			return A1;
		}
};

int main()
{
	cout << "*********************" << endl;
	//double mb[5][7] ={ {1, 2, 3, 4, 5, 6, 7}, { 8, 9, 10, 11, 12, 13, 14 }, {15, 16, 17, 18, 19, 20, 21}, { 22, 23, 24, 25, 26, 27, 28 }, { 29, 30, 31, 32, 33, 34, 35 } };
	//Matrix mB;
	//mB.Init((double *)mb, 5, 7);
	//cout << "原矩阵A1:" << endl;
	//mB.print();
	//Matrix pinv_mB = MatrixOperator::pinv(mB);
	//cout << "矩阵A1的广义逆矩阵:" << endl;
	//pinv_mB.print();
	cout << "*********************" << endl;
	double ma[6][4] = { { 11,12,13,14 },{ 15,16,17,18 },{ 19,20,21,22 },{ 23,24,25,26 },{ 27,28,29,30 },{ 31,32,33,34 } };
	Matrix mA;
	mA.Init((double *)ma, 6, 4);
	cout << "原矩阵A2:" << endl;
	mA.print();
	Matrix pinv_mA = MatrixOperator::pinv(mA);
	cout << "矩阵A2的广义逆矩阵:" << endl;
	pinv_mA.print();
	//也可考虑使用vector作为二维数组传参，更加稳定且较为方便
	//vector< vector<double> > arry = { { 1,2,3 },{4,5,6 } };
	//for (int i = 0; i < arry.size(); i++)
	//{
	//	for (int j = 0; j < arry[0].size(); j++)
	//	{
	//		cout << arry[i][j] << " ";
	//	}
	//	cout << endl;
	//}
	return 0;
}
