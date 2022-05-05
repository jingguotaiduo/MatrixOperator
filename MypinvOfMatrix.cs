using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MypinvOfMatrix
{
    class MatrixOperator
    {
        public static double[,] Trans(double[,] a0, int m, int n)//矩阵转置函数
        {
            double[,] c = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    c[i, j] = a0[j, i];
            return c;
        }

        public static double[,] Multi(double[,] a0, int m, int n, double[,] b0, int m2, int n2)//矩阵相乘函数
        {
            if (n != m2)
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }
            double[,] c = new double[m, n2];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n2; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < n; k++)
                        c[i, j] += a0[i, k] * b0[k, j];
                }
            return c;
        }

        public static double[,] ni(double[,] a0, int m, int n)//矩阵求逆（方阵)
        {
            double[,] a = new double[m, n],c = new double[m, n];
            if (m != n)
            {
                Console.WriteLine("非方阵，不可逆！");
            }
            else
            {
                if (rank(a0, m, n) != m)
                    Console.WriteLine("该方阵为奇异矩阵，不可逆！");
                else
                {
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            a[i, j] = a0[i, j];
                        }
                    }
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (i == j) c[i, j] = 1;
                            else c[i, j] = 0;
                        }
                    }
                    for (int i = 0; i < m; i++)
                    {
                        double aii = a[i, i];//记录每行主对角线元素
                        if (Math.Abs(aii) <= (1e-6))//如果为0
                        {
                            int k;
                            for (k = i + 1; k < m; k++)
                            {
                                double aki = a[k, i];//记录每行主对角线元素
                                if (Math.Abs(aki) > (1e-6))//如果不为0
                                {
                                    for (int j = 0; j < n; j++)
                                    {
                                        a[i, j] = a[i, j] - a[k, j];
                                        c[i, j] = c[i, j] - c[k, j];
                                    }
                                    aii = a[i, i];
                                    for (int j = 0; j < n; j++)
                                    {
                                        a[i, j] = a[i, j] / aii;
                                        c[i, j] = c[i, j] / aii;
                                    }
                                    break;
                                }
                            }
                            /*if (k == m)
                            {
                                Exception myException = new Exception("没有逆矩阵");
                                throw myException;
                            }
                             * */
                        }
                        else//如果不为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                a[i, j] = a[i, j] / aii;
                                c[i, j] = c[i, j] / aii;
                            }
                        }
                        for (int l = i + 1; l < m; l++)
                        {
                            double ali = a[l, i];
                            if (Math.Abs(ali) > (1e-6))
                            {
                                for (int j = 0; j < n; j++)
                                {
                                    a[l, j] -= ali * a[i, j];
                                    c[l, j] -= ali * c[i, j];
                                }
                            }
                        }
                    }
                    for (int i = m - 1; i >= 0; i--)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            double aki = a[k, i];
                            if (Math.Abs(aki) > (1e-6))
                            {
                                for (int j = n - 1; j >= 0; j--)
                                {
                                    a[k, j] -= aki * a[i, j];
                                    c[k, j] -= aki * c[i, j];
                                }
                            }
                        }
                    }
                }
            }
            return c;
        }

        public static int rank(double[,] a0, int m, int n)//求矩阵的秩的函数
        {
            int r = 0, m1 = 0;
            double[,] a = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = a0[i, j];
                }
            if (m > n) m1 = n;
            else m1 = m;
            for (int i = 0; i < m1; i++)
            {
                double aii = a[i, i];//记录每行主对角线元素
                if (Math.Abs(aii) <= (1e-6))//如果为0
                {
                    int k;
                    for (k = i + 1; k < m; k++)
                    {
                        double aki = a[k, i];//记录每行对应第i行主对角线元素的元素
                        if (Math.Abs(aki) > (1e-6))//如果不为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                a[i, j] = a[i, j] - a[k, j];
                            }
                            double ai = a[i, i];
                            for (int j = 0; j < n; j++)
                                a[i, j] /= ai;
                            break;
                        }
                    }
                }
                else  //主元素不为0
                {
                    for (int j = 0; j < n; j++)
                        a[i, j] /= aii;
                }
                for (int l = i + 1; l < m; l++)
                {
                    double ali = a[l, i];
                    if (Math.Abs(ali) > (1e-6))
                    {
                        for (int j = 0; j < n; j++)
                        {
                            a[l, j] -= ali * a[i, j];
                        }
                    }
                }
            }
            for (int i = m1 - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double aki = a[k, i];
                    if (Math.Abs(aki) > (1e-6))
                    {
                        for (int j = m1 - 1; j >= 0; j--)
                        {
                            a[k, j] -= aki * a[i, j];
                        }
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Math.Abs(a[i, j]) > (1e-6))
                    {
                        r++;
                        break;
                    }
                }
            }
            return r;
        }

        public static double[,] pinv(double[,] A, int m, int n)//求矩阵广义逆函数
        {
            int M;
            if (m > n) M = n;
            else M = m;
            double[,] E = new double[m, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j == i) E[i, j] = 1;
                    else E[i, j] = 0;
                }
            }
            for (int i = 0; i < M; i++)
            {
                double aii = A[i, i];//记录每行主对角线元素
                if (Math.Abs(aii) <= (1e-6))//如果为0
                {
                    int k;
                    for (k = i + 1; k < m; k++)
                    {
                        double aki = A[k, i];//记录每行对应第i行主对角线元素的元素
                        if (Math.Abs(aki) > (1e-6))//如果不为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                A[i, j] = A[i, j] - A[k, j];
                            }
                            for (int j = 0; j < m; j++)
                            {
                                E[i, j] = E[i, j] - E[k, j];
                            }
                            double ai = A[i, i];
                            for (int j = 0; j < n; j++)
                            {
                                A[i, j] /= ai;
                            }
                            for (int j = 0; j < m; j++)
                            {
                                E[i, j] /= ai;
                            }
                            break;
                        }
                    }
                }
                else  //主元素不为0
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] /= aii;
                    }
                    for (int j = 0; j < m; j++)
                    {
                        E[i, j] /= aii;
                    }
                }
                for (int l = i + 1; l < m; l++)
                {
                    double ali = A[l, i];
                    if (Math.Abs(ali) > (1e-6))
                    {
                        for (int j = 0; j < n; j++)
                        {
                            A[l, j] -= ali * A[i, j];
                        }
                        for (int j = 0; j < m; j++)
                        {
                            E[l, j] -= ali * E[i, j];
                        }
                    }
                    else continue;//为零，则下一行
                }
            }
            for (int i = M - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double aki = A[k, i];
                    if (Math.Abs(aki) > (1e-6))
                    {
                        for (int j = n - 1; j >= 0; j--)
                        {
                            A[k, j] -= aki * A[i, j];
                        }
                        for (int j = m - 1; j >= 0; j--)
                        {
                            E[k, j] -= aki * E[i, j];
                        }
                    }
                }
            }
            int rA = MatrixOperator.rank(A, m, n);
            for (int q = 0; q < rA; q++)
            {
                if (Math.Abs(A[q, q]) <= (1e-6))//主对角线元素若为0，需要交换行或交换列
                {
                    for (int i = q + 1; i < M; i++)
                    {
                        int biaoji = -1;
                        if (A[i, i] == 1)
                        {
                            biaoji = i;
                        }
                        else if (Math.Abs(A[i, i]) <= (1e-6))//主对角线元素若为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                if (A[i, j] == 1)
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
                                double temp = A[q, w];
                                A[q, w] = A[i, w];
                                A[i, w] = temp;
                            }
                            for (int w = 0; w < m; w++)
                            {
                                double temp1 = E[q, w];
                                E[q, w] = E[i, w];
                                E[i, w] = temp1;
                            }
                        }
                    }
                }
            }
            int z = rA;
            double[,] G = new double[m, n], P = new double[m, m], P1 = new double[m, m], B = new double[m, z], C = new double[z, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    G[i, j] = A[i, j];
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    P[i, j] = E[i, j];
                }
            }
            P1 = MatrixOperator.ni(P, m, m);
            int rB = 0, rC = 0;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < z; j++)
                    B[i, j] = P1[i, j];
            rB = MatrixOperator.rank(B, m, z);
            for (int i = 0; i < z; i++)
                for (int j = 0; j < n; j++)
                    C[i, j] = G[i, j];
            rC = MatrixOperator.rank(C, z, n);
            double[,] BT = new double[z, m], CT = new double[n, z];
            BT = MatrixOperator.Trans(B, m, z);
            CT = MatrixOperator.Trans(C, z, n);
            double[,] A1 = new double[n, m];
            A1 = MatrixOperator.Multi(MatrixOperator.Multi(CT, n, z, MatrixOperator.ni(MatrixOperator.Multi(C, z, n, CT, n, z), z, z), z, z), n, z,
                                     MatrixOperator.Multi(MatrixOperator.ni(MatrixOperator.Multi(BT, z, m, B, m, z), z, z), z, z, BT, z, m), z, m);
            return A1;
        }

        public static int chudengtransform(double[,] a, int m, int n)//对矩阵进行初等变换后再进行初等列变换化为行阶梯型矩阵
        {
            int zhi = 0;
            for (int i = 0; i < m; i++)
            {
                double aii = a[i, i];//记录每行主对角线元素
                if (Math.Abs(aii) <= (1e-6))//如果为0
                {
                    int k;
                    for (k = i + 1; k < m; k++)
                    {
                        double aki = a[k, i];//记录每行对应第i行主对角线元素的元素
                        if (Math.Abs(aki) > (1e-6))//如果不为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                a[i, j] = a[i, j] - a[k, j];
                            }
                            double ai = a[i, i];
                            for (int j = 0; j < n; j++)
                                a[i, j] /= ai;
                            break;
                        }
                    }
                }
                else  //主元素不为0
                {
                    for (int j = 0; j < n; j++)
                        a[i, j] /= aii;
                }
                for (int l = i + 1; l < m; l++)
                {
                    double ali = a[l, i];
                    if (Math.Abs(ali) > (1e-6))
                    {
                        for (int j = 0; j < n; j++)
                        {
                            a[l, j] -= ali * a[i, j];
                        }
                    }
                    else continue;//为零，则下一行
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int k = i + 1; k < n; k++)
                {
                    double aik = a[i, k];
                    if (Math.Abs(aik) > (1e-6))
                    {
                        for (int j = 0; j < m; j++)
                        {
                            a[j, k] -= a[j, i] * aik;
                        }
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Math.Abs(a[i, j]) > (1e-6))
                    {
                        zhi++;
                        break;
                    }
                }
            }
            for (int q = 0; q < zhi; q++)
            {
                if (Math.Abs(a[q, q]) <= (1e-6))//主对角线元素若为0，需要交换行或交换列
                {
                    for (int i = q + 1; i < m; i++)
                    {
                        int biaoji = -1;
                        if (a[i, i] == 1)
                        {
                            biaoji = i;
                        }
                        if (biaoji > 0)//该行有1，已经找到其行号和列号
                        {
                            //将i行和q行交换
                            for (int w = 0; w < n; w++)
                            {
                                double temp = a[q, w];
                                a[q, w] = a[i, w];
                                a[i, w] = temp;
                            }
                            //将j列和q列交换
                            for (int w = 0; w < m; w++)
                            {
                                double temp = a[w, q];
                                a[w, q] = a[w, biaoji];
                                a[w, biaoji] = temp;
                            }
                        }
                    }
                }
            }
            return zhi;
        }

        public static void chudenghangbianhuan(double[,] A, double[,] E, int a, int b)//初等行变换化为标准阶梯型
        {
            int m = a, m1 = 0;
            int n = b;
            if (m > n) m1 = n;
            else m1 = m;
            for (int i = 0; i < m1; i++)
            {
                double aii = A[i, i];//记录每行主对角线元素
                if (Math.Abs(aii) <= (1e-6))//如果为0
                {
                    int k;
                    for (k = i + 1; k < m; k++)
                    {
                        double aki = A[k, i];//记录每行对应第i行主对角线元素的元素
                        if (Math.Abs(aki) > (1e-6))//如果不为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                A[i, j] = A[i, j] - A[k, j];
                            }
                            for (int j = 0; j < m; j++)
                            {
                                E[i, j] = E[i, j] - E[k, j];
                            }
                            double ai = A[i, i];
                            for (int j = 0; j < n; j++)
                            {
                                A[i, j] /= ai;
                            }
                            for (int j = 0; j < m; j++)
                            {
                                E[i, j] /= ai;
                            }
                            break;
                        }
                    }
                }
                else  //主元素不为0
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] /= aii;
                    }
                    for (int j = 0; j < m; j++)
                    {
                        E[i, j] /= aii;
                    }
                }
                for (int l = i + 1; l < m; l++)
                {
                    double ali = A[l, i];
                    if (Math.Abs(ali) > (1e-6))
                    {
                        for (int j = 0; j < n; j++)
                        {
                            A[l, j] -= ali * A[i, j];
                        }
                        for (int j = 0; j < m; j++)
                        {
                            E[l, j] -= ali * E[i, j];
                        }
                    }
                    else continue;//为零，则下一行
                }
            }
            for (int i = m1 - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double aki = A[k, i];
                    if (Math.Abs(aki) > (1e-6))
                    {
                        for (int j = n - 1; j >= 0; j--)
                        {
                            A[k, j] -= aki * A[i, j];
                        }
                        for (int j = m - 1; j >= 0; j--)
                        {
                            E[k, j] -= aki * E[i, j];
                        }
                    }
                }
            }
            int rA = MatrixOperator.rank(A, m, n);
            for (int q = 0; q < rA; q++)
            {
                if (Math.Abs(A[q, q]) <= (1e-6))//主对角线元素若为0，需要交换行或交换列
                {
                    for (int i = q + 1; i < m1; i++)
                    {
                        int biaoji = -1;
                        if (A[i, i] == 1)
                        {
                            biaoji = i;
                        }
                        else if (Math.Abs(A[i, i]) <= (1e-6))//主对角线元素若为0
                        {
                            for (int j = 0; j < n; j++)
                            {
                                if (A[i, j] == 1)
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
                                double temp = A[q, w];
                                A[q, w] = A[i, w];
                                A[i, w] = temp;
                            }
                            for (int w = 0; w < m; w++)
                            {
                                double temp1 = E[q, w];
                                E[q, w] = E[i, w];
                                E[i, w] = temp1;
                            }
                        }
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int m, n;
            Console.Write("矩阵的行数:");
            m = Convert.ToInt16(Console.ReadLine());
            Console.Write("矩阵的列数:");
            n = Convert.ToInt16(Console.ReadLine());
            double[,] a = new double[m, n], A = new double[m, n];
            Console.WriteLine("请输入矩阵元素");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    a[i, j] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine("原矩阵A:");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = a[i, j];
                    Console.Write(a[i, j] + " ");
                    if (j == (n - 1))
                        Console.WriteLine();
                }
            }

            double[,] A11 = MatrixOperator.pinv(A, m, n);
            Console.WriteLine("广义逆矩阵pinv_A：");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(A11[i, j] + "  ");
                    if (j == m - 1) Console.WriteLine();
                }
            }

            //接下来计算矩阵的满秩分解 求a=B*C
            Console.WriteLine("满秩分解法求解广义逆矩阵pinv_A过程如下：");
            int z = MatrixOperator.rank(a, m, n);
            Console.WriteLine("A矩阵的秩为" + z);
            double[,] E = new double[m, m], G = new double[m, n], P = new double[m, m], P1 = new double[m, m], B = new double[m, z], C = new double[z, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j == i) E[i, j] = 1;
                    else E[i, j] = 0;
                }
            }
            MatrixOperator.chudenghangbianhuan(a, E, m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    G[i, j] = a[i, j];
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    P[i, j] = E[i, j];
                }
            }
            P1 = MatrixOperator.ni(P, m, m);
            Console.WriteLine("初等行变换后的矩阵如下：");
            Console.WriteLine("A----G：");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(G[i, j] + " ");
                    if (j == (n - 1))
                        Console.WriteLine();
                }
            }
            Console.WriteLine("E----P：");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(P[i, j] + " ");
                    if (j == (m - 1))
                        Console.WriteLine();
                }
            }
            Console.WriteLine("P逆:");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(P1[i, j] + " ");
                    if (j == (m - 1))
                        Console.WriteLine();
                }
            }
            Console.WriteLine("满秩分解（A=BC)后的矩阵如下：");
            int rB = 0, rC = 0;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < z; j++)
                    B[i, j] = P1[i, j];
            rB = MatrixOperator.rank(B, m, z);
            Console.WriteLine("B矩阵的秩为" + rB);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    Console.Write(B[i, j] + "  ");
                    if (j == z - 1) Console.WriteLine();
                }
            }
            for (int i = 0; i < z; i++)
                for (int j = 0; j < n; j++)
                    C[i, j] = G[i, j];
            rC = MatrixOperator.rank(C, z, n);
            Console.WriteLine("C矩阵的秩为" + rC);
            for (int i = 0; i < z; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(C[i, j] + "  ");
                    if (j == n - 1) Console.WriteLine();
                }
            }
            double[,] BT = new double[z, m], CT = new double[n, z];
            BT = MatrixOperator.Trans(B, m, z);
            CT = MatrixOperator.Trans(C, z, n);
            double[,] A1 = new double[n, m];
            A1 = MatrixOperator.Multi(MatrixOperator.Multi(CT, n, z, MatrixOperator.ni(MatrixOperator.Multi(C, z, n, CT, n, z), z, z), z, z), n, z,
                                     MatrixOperator.Multi(MatrixOperator.ni(MatrixOperator.Multi(BT, z, m, B, m, z), z, z), z, z, BT, z, m), z, m);
            Console.WriteLine("矩阵A的广义逆矩阵如下：");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(A1[i, j] + "  ");
                    if (j == m - 1) Console.WriteLine();
                }
            }
        }
    }
}
