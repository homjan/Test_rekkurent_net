using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    public static class Activation_Func_Diff
    {
        public static double[] Sigmoid_diff(double[] solution_num_out)
        {
            double[] y = new double[solution_num_out.Length];
            for (int i = 0; i < solution_num_out.Length; i++)
            {
                y[i] = (1 - solution_num_out[i]) * solution_num_out[i];
            }
            return y;
        }
        public static double Tanh_diff(double solution_num_out)
        {
            double y = 1 - (solution_num_out * solution_num_out);
            return y;
        }

        public static double RELU_diff(double solution_num_out)
        {
            double y;
            if (solution_num_out < 0) { y = 0; }
            else { y = 1; }

            return y;
        }

        public static double Softmax_diff(double x)
        {
            double y = x * (1 - x);
            return y;
        }



    }
}
