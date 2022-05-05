#include<iostream> 
#include<Eigen/Core> 
#include<Eigen/SVD>   

template<typename _Matrix_Type_> _Matrix_Type_ pseudoInverse(const _Matrix_Type_ &a, double epsilon = std::numeric_limits<double>::epsilon())
{
	Eigen::JacobiSVD< _Matrix_Type_ > svd(a, Eigen::ComputeThinU | Eigen::ComputeThinV);
	double tolerance = epsilon * std::max(a.cols(), a.rows()) *svd.singularValues().array().abs()(0);
	return svd.matrixV() *  (svd.singularValues().array().abs() > tolerance).select(svd.singularValues().array().inverse(), 0).matrix().asDiagonal() * svd.matrixU().adjoint();
}

void pinvofMatrix(Eigen::MatrixXd A)
{
	std::cout <<"原矩阵：" << std::endl << A << std::endl;
	std::cout <<"广义逆矩阵：" << std::endl << pseudoInverse(A) << std::endl;
}
int main()
{
	Eigen::MatrixXd A1(5, 7);
    A1 << 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35;
	pinvofMatrix(A1);
	Eigen::MatrixXd A2(6, 4);
	A2 << 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34;
	pinvofMatrix(A2);
	return 0;
}
