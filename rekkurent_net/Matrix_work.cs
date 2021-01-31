using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Matrix_work
    {
       private double[,] weight;
        private double[][] weight_divided;

        private int rows;
        private int columns;

        public Matrix_work(double[,] weight2) {

            this.weight = weight2;
            rows = weight.GetUpperBound(0) + 1;// Количество строк
            columns = weight.Length / rows;// Количество столбцов

            weight_divided = new double[rows][];
            for( int i = 0; i < columns; i++){
                
                weight_divided[i] = new double[columns];
            }
        }

        public double[][] Get_weight_divided() {
            return weight_divided;
        }

        public double[,] Get_weight() {
            return weight;
        }


        public void Weight_Convert_in_Weight_divided() {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    weight_divided[i][j] = weight[i, j];
                }
            }
        }

        public void Weight_divided_Convert_in_Weight() {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    weight[i, j] = weight_divided[i][j];
                }
            }
        }

        /// <summary>
        /// Найти вектор х, который при умножении на weight_divided, дает b
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public double[] HelperSolve( double[] b)
        {
            // Решаем weight_divided * x = b
            int n = weight_divided.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                    sum -= weight_divided[i][j] * x[j];
                x[i] = sum;
            }
            x[n - 1] /= weight_divided[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                    sum -= weight_divided[i][j] * x[j];
                x[i] = sum / weight_divided[i][i];
            }
            return x;
        }
        /// <summary>
        /// Произведение двух матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[,] Matrix_Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
        /// <summary>
        /// Сложение двух матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[,] Matrix_Sum(double[,] a, double[,] b)
        {
            if ((a.GetLength(1) != b.GetLength(1)) && (a.GetLength(0) != b.GetLength(0))) throw new Exception("Матрицы нельзя сложить");
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {                   
                    r[i, j] = a[i,j] + b[i, j];
                    
                }
            }
            return r;
        }

        /// <summary>
        /// Сложение двух векторов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[] Vector_Sum(double[] a, double[] b)
        {
            if (a.GetLength(0) != b.GetLength(0)) throw new Exception("Векторы нельзя сложить");
            double[] r = new double[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {                
                    r[i] = a[i] + b[i];               
            }
            return r;
        }

        /// <summary>
        /// Вычитание двух векторов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[] Vector_Substraction(double[] a, double[] b)
        {
            if (a.GetLength(0) != b.GetLength(0)) throw new Exception("Векторы нельзя вычесть");
            double[] r = new double[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                r[i] = a[i] - b[i];
            }
            return r;
        }
        /// <summary>
        /// Покомпонентное умножение двух матриц
        /// (Алгоритм Адамара_Шура)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[,] Matrix_Multiplication_Adamar_Shur(double[,] a, double[,] b)
        {
            if ((a.GetLength(1) != b.GetLength(1)) && (a.GetLength(0) != b.GetLength(0))) throw new Exception("Матрицы нельзя сложить");
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    r[i, j] = a[i, j] * b[i, j];

                }
            }
            return r;
        }

        /// <summary>
        /// /// Покомпонентное умножение двух векторов
        /// (Алгоритм Адамара_Шура)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[] Vector_Multiplication_Adamar_Shur(double[] a, double[] b)
        {
            if (a.GetLength(0) != b.GetLength(0)) throw new Exception("Весторы нельзя сложить");
            double[] r = new double[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {               
                    r[i] = a[i] * b[i];
            }
            return r;
        }




    }
}
