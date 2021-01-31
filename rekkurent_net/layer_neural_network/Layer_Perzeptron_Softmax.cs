using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    class Layer_Perzeptron_Softmax : Layer_abstract
    {
        public Layer_Perzeptron_Softmax(int input_length, int output_length) : base(input_length, output_length)
        {

        }

        public double[] Delta_H(double[] sigma, double[] sloj2)
        {
            double[] y = new double[sigma.Length];
            double[] s = Activation_Func_Diff.Sigmoid_diff(sloj2);

            for (int i = 0; i < sigma.Length; i++)
            {
                for (int j = 0; j < sigma.Length; j++)
                {
                    y[i] = (sigma[j] * weight_1[i, j]);
                }
                y[i] = s[i] * y[i];
            }

            return y;
        }

        public override double[] Perzertron_forward(double[] x)
        {
            double[] y = new double[x.Length];
            double[] z;

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    y[i] = y[i] + (x[i] * weight_1[j, i]);
                }
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias0[i];
            }

            z = Activation_Func.Softmax(y);          

            return z;

        }



    }
}
