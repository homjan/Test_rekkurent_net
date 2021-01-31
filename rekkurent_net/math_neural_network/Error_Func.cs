using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    public static class Error_Func
    {      
        /// <summary>
        /// Рассчитать и вернуть квадратичную функцию ошибок
        /// </summary>
        /// <param name="true_result"></param>
        /// <param name="numerical_result"></param>
        /// <returns></returns>
        public static double[] MSE_calculate_and_return(double[] true_result, double[] numerical_result)
        {
            double[] mps_number = new double[true_result.Length];

            for (int i = 0; i < true_result.Length; i++)
            {
                mps_number[i] = mps_number[i] + Math.Pow((true_result[i] - numerical_result[i]), 2);
            }   

            return mps_number;
        }

        /// <summary>
        /// Обезразмерить квадратичную функцию ошибок на число элементов
        /// </summary>
        /// <param name="MSE"></param>
        /// <returns></returns>
        public static double MSE_ob(double[] MSE)
        {
            double M=0;

            for (int i = 0; i < MSE.Length; i++)
            {
                M= M+ MSE[i] / MSE.Length;
            }

            return M;

        }
        /// <summary>
        /// Обезразмерить квадратичную функцию ошибок на число обработанных строк
        /// </summary>
        /// <param name="MSE"></param>
        /// <param name="N_valid"></param>
        /// <returns></returns>
        public static double[] MSE_obez(double[] MSE, double N_valid) {

            for (int i = 0; i < MSE.Length; i++)
            {
                MSE[i] = MSE[i] / N_valid;
            }

            return MSE;

        }



    }
}
