import numpy as np

def Trans(A):# 矩阵转置
    m, n = len(A),len(A[0])
    At = [[0 for j in range(m)] for i in range(n)]
    for i in range(0, n):
        for j in range(0, m):
            At[i][j] = A[j][i]
    return np.array(At)

def Multiply(A, B):# 矩阵相乘
    m1, n1 = len(A),len(A[0])
    m2, n2 = len(B), len(B[0])
    C = []
    if n1 != m2:
        print('两个矩阵无法相乘！')
    else:
        C = [[0 for j in range(n2)] for i in range(m1)]
        for i in range(0,m1):
            for j in range(0,n2):
                C[i][j] = 0
                for k in range(0,m2):
                    C[i][j] += A[i][k] * B[k][j]
    return C

def Rank(a0):# 求矩阵的秩
    r = 0
    m1 = 0
    m, n = len(a0), len(a0[0])
    a = [[0 for j in range(n)] for i in range(m)]
    for i in range(0,m):
        for j in range(0,n):
            a[i][j] = a0[i][j]
    if m > n:
        m1 = n
    else:
        m1 = m
    for i in range(0,m1):# 从上到下，将主对角线元素化为1，左下角矩阵化为0
        aii = a[i][i]; # 记录每行主对角线元素
        if abs(aii) <= 1e-6:# 如果为0
            for k in range(i+1,m):
                aki = a[k][i]; # 记录每行对应第i行主对角线元素的元素
                if abs(aii) > 1e-6:  # 如果不为0
                    for j in range(0,n):
                        a[i][j] -= a[k][j]
                    ai = a[i][i]
                    for j in range(0,n):
                        a[i][j] /= ai
                    break
        else:# 主元素不为0
            for j in range(0,n):
                a[i][j] /= aii
        for l in range(i+1, m):
            ali = a[l][i]
            if abs(ali) > 1e-6:
                for j in range(0,n):
                    a[l][j] -= ali * a[i][j]
    i = m1 -1
    while i >=0:
        k = i -1
        while k >= 0:
            aki = a[k][i]
            if abs(aki) > 1e-6:
                j = m1 -1
                while j >= 0:
                    a[k][j] -= aki * a[i][j]
                    j = j-1
            k = k -1
        i = i-1

    for i in range(0,m):
        for j in range(0,n):
            if abs(a[i][j]) > 1e-6:
                r = r+1
                break
    return r

def inv(a0):
    c = []
    m,n = len(a0), len(a0[0])
    if m != n:
        print('非方阵，不可逆！')
    else:
        if Rank(a0) != m:
            print('该方阵为奇异矩阵，不可逆！')
        else:# 满秩
            a = [[0 for j in range(n)] for i in range(m)]
            c = [[0 for j in range(n)] for i in range(m)]
            for i in range(0, m):
                for j in range(0, n):
                    a[i][j] = a0[i][j]
                    if i == j:
                        c[i][j] = 1
                    else:
                        c[i][j] = 0
            for i in range(0,m):
                aii = a[i][i]; # 记录每行主对角线元素
                if abs(aii) <= 1e-6:# 如果为0
                    for k in range(i+1,m):
                        aki = a[k][i]# 记录每行主对角线元素
                        if abs(aki) > 1e-6:# 如果不为0
                            for j in range(0,n):
                                a[i][j] -= a[k][j]
                                c[i][j] -= c[k][j]
                            aii = a[i][i]
                            for j in range(0,n):
                                a[i][j] /= aii
                                c[i][j] /= aii
                            break
                else:# 如果不为0
                    for j in range(0,n):
                        a[i][j] /= aii
                        c[i][j] /= aii
                for l in range(i+1,m):
                    ali = a[l][i]
                    if abs(ali) > 1e-6:
                        for j in range(0,n):
                            a[l][j] -= ali * a[i][j]
                            c[l][j] -= ali * c[i][j]

            i = m -1
            while i >= 0:
                k = i -1
                while k >= 0:
                    aki = a[k][i]
                    if abs(aki) > 1e-6:
                        j = n -1
                        while j >= 0:
                            a[k][j] -= aki * a[i][j]
                            c[k][j] -= aki * c[i][j]
                            j = j-1
                    k = k-1
                i = i - 1
    return c

def pinv(a0):
    m, n = len(a0), len(a0[0])
    A = [[0 for j in range(n)] for i in range(m)]
    for i in range(0, m):
        for j in range(0, n):
            A[i][j] = a0[i][j]
    M = 0
    if m > n:
        M = n
    else:
        M = m
    E = [[0 for j in range(m)] for i in range(m)]
    for i in range(0, m):
        for j in range(0, m):
            if i == j:
                E[i][j] = 1;
            else:
                E[i][j] = 0
    for i in range(0,M):
        aii = A[i][i]# 记录每行主对角线元素
        if abs(aii) <= 1e-6:# 如果为0
            for k in range(i+1,m):
                aki = A[k][i]# 记录每行对应第i行主对角线元素的元素
                if abs(aki) > 1e-6:# 如果不为0
                    for j in range(0,n):
                        A[i][j] -= A[k][j]
                    for j in range(0,m):
                        E[i][j] -= E[k][j]
                    ai = A[i][i]
                    for j in range(0,n):
                        A[i][j] /= ai;
                    for j in range(0,m):
                        E[i][j] /= ai
                    break
        else:# 主元素不为0
            for j in range(0,n):
                A[i][j] /= aii
            for j in range(0,m):
                E[i][j] /= aii

        for l in range(i+1,m):
            ali = A[l][i]
            if abs(ali) > 1e-6:
                for j in range(0,n):
                    A[l][j] -= ali * A[i][j]
                for j in range(0,m):
                    E[l][j] -= ali * E[i][j]
            else:
                continue# 为零，则下一行
    i = M -1
    while i >= 0:
        k = i-1
        while k >= 0:
            aki = A[k][i]
            if abs(aki) > 1e-6:
                j = n - 1
                while j >= 0:
                    A[k][j] -= aki * A[i][j]
                    j = j-1
                j = m-1
                while j >= 0:
                    E[k][j] -= aki * E[i][j]
                    j = j-1
            k = k-1
        i = i -1
    rA = Rank(A)
    for q in range(0,rA):
        if abs(A[q][q]) <= 1e-6:# 主对角线元素若为0，需要交换行或交换列
            for i in range(q+1,M):
                biaoji = -1
                if A[i][i] == 1:
                    biaoji = 1
                elif abs(A[i][i]) <= 1e-6:# 主对角线元素若为0
                    for j in range(0,n):
                        if A[i][j] == 1:
                            biaoji = i
                            break
                if biaoji > 0:# 该行有1，已经找到其行号和列号
                    # 将i行和q行交换
                    for w in range(0,n):
                        temp = A[q][w]
                        A[q][w] = A[i][w]
                        A[i][w] = temp
                    for w in range(0,m):
                        temp1 = E[q][w]
                        E[q][w] = E[i][w]
                        E[i][w] = temp1
    z = rA
    G = [[0 for j in range(n)] for i in range(m)]
    P = [[0 for j in range(m)] for i in range(m)]
    P1 = [[0 for j in range(m)] for i in range(m)]
    B = [[0 for j in range(z)] for i in range(m)]
    C = [[0 for j in range(n)] for i in range(z)]
    for i in range(0,m):
        for j in range(0,n):
            G[i][j] = A[i][j]
    for i in range(0,m):
        for j in range(0,m):
            P[i][j] = E[i][j]
    P1 = inv(P)
    for i in range(0,m):
        for j in range(0,z):
            B[i][j] = P1[i][j]
    for i in range(0,z):
        for j in range(0,n):
            C[i][j] = G[i][j]
    Bt = Trans(B)
    Ct = Trans(C)
    A1 = Multiply(Multiply(Ct, inv(Multiply(C, Ct))), Multiply(inv(Multiply(Bt, B)), Bt))
    return A1

def Shucu(X): #打印二维矩阵函数
    row,col = len(X), len(X[0])
    for i in range(0, row):
        for j in range(0, col):
            print(" %f " % (X[i][j]), end='')
        print()
def pinvofMatrix(A): #计算广义逆并输出
    pinv_A = pinv(A)
    print('原矩阵A：')
    Shucu(A)
    print('A的广义逆矩阵：')
    Shucu(pinv_A)
if __name__ == '__main__':
    A1 = np.array([[1, 2, 3, 4, 5, 6, 7],[8, 9, 10, 11, 12, 13, 14],[15, 16, 17, 18, 19, 20, 21],[22, 23, 24, 25, 26, 27, 28],[29, 30, 31, 32, 33, 34, 35]])
    A2 = np.array([[11, 12, 13, 14],[15, 16, 17, 18],[19, 20, 21, 22],[23, 24, 25, 26],[27, 28, 29, 30],[31, 32, 33, 34]])
    pinvofMatrix(A1)
    pinvofMatrix(A2)
