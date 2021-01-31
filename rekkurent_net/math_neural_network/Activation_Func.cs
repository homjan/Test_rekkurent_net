using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    public static class Activation_Func
    {      
        public static double Sigmoid(double x)
        {
            double y = 1 / (1 + Math.Exp((-1) * x));
            return y;
        }


        public static double Tanh(double x)
        {
            double y = (Math.Exp(x) - Math.Exp((-1) * x)) / (Math.Exp(x) + Math.Exp((-1) * x));
            return y;
        }

        public static double RELU(double x)
        {
            double y;
            if (x < 0) { y = 0; }
            else { y = x; }

            return y;
        }

        public static double[] Softmax(double[] x)
        {
            double[] y = new double[x.Length];
            double maxi = x.Max();

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Math.Exp(x[i] - maxi);
            }
            double sum = y.Sum();

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] / sum;

            }
            return y;
        }

        public static double[] Obezpaz(double[] x)
        {
            double[] y = new double[x.Length];
            double sum = x.Sum();

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[i] / sum;
            }
            return y;
        }




    }
}
