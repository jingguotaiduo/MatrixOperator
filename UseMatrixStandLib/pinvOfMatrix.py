import numpy as np

def Shucu(X,row,col): #打印二维矩阵函数
    for i in range(0, row):
        for j in range(0, col):
            print(" %f " % (X[i][j]), end='')
        print()

def pinvofMatrix(X,row,col): #计算广义逆并输出
    pinv_X = np.linalg.pinv(X)
    print('原矩阵A：')
    Shucu(X, row, col)
    print('A的广义逆矩阵：')
    Shucu(pinv_X, col, row)

if __name__ == '__main__':
    m ,n = 5, 7
    A1 = np.array([[1, 2, 3, 4, 5, 6, 7],[8, 9, 10, 11, 12, 13, 14],[15, 16, 17, 18, 19, 20, 21],[22, 23, 24, 25, 26, 27, 28],[29, 30, 31, 32, 33, 34, 35]])
    pinvofMatrix(A1,m,n)
    A2 = np.array([[11, 12, 13, 14],[15, 16, 17, 18],[19, 20, 21, 22],[23, 24, 25, 26],[27, 28, 29, 30],[31, 32, 33, 34]])
    m, n = 6, 4
    pinvofMatrix(A2, m, n)
