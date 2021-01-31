using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Education_net
    {

        protected double[,] weight_1;
        protected double[,] weight_2;

        protected double[] bias0;
        protected double[] bias1;

        protected double[,] weight_1_delta;
        protected double[,] weight_2_delta;

        protected double[,] weight_1_old;
        protected double[,] weight_2_old;

        protected double[,] bias_0_delta;
        protected double[,] bias_1_delta;

        protected double[] bias_0_old;
        protected double[] bias_1_old;


        int razmer_data_in;
        int razmer_layer_1_in;
        int razmer_layer_2_in;

        public double[] mps_number;

      
     //   public generation_math generator_random;

        public Education_net(int razmer1, int razmer2, int razmer3)
        {

            this.razmer_data_in = razmer1;
            this.razmer_layer_1_in = razmer2;
            this.razmer_layer_2_in = razmer3;

            weight_1 = new double[razmer_layer_1_in, razmer_data_in];
            weight_2 = new double[razmer_layer_2_in, razmer_layer_1_in];
            mps_number = new double[razmer_data_in];

            weight_1_delta = new double[razmer_layer_1_in, razmer_data_in];
            weight_2_delta = new double[razmer_layer_2_in, razmer_layer_1_in];

            weight_1_old = new double[razmer_layer_1_in, razmer_data_in];
            weight_2_old = new double[razmer_layer_2_in, razmer_layer_1_in];

            bias0 = new double[razmer_layer_1_in];
            bias1 = new double[razmer_layer_2_in];

            bias_0_delta = new double[razmer_layer_1_in, razmer_data_in];
            bias_1_delta = new double[razmer_layer_2_in, razmer_layer_1_in];

            bias_0_old = new double[razmer_layer_1_in];
            bias_1_old = new double[razmer_layer_2_in];        

        }

        public virtual void Shift_weights()
        {
            weight_1_old = weight_1;
            weight_2_old = weight_2;

            bias_0_old = bias0;
            bias_1_old = bias1;
        }

        public double[] Delta_H(double[] sigma, double[] sloj2)
        {
            double[] y = new double[sigma.Length];
            double[] s = Activation_Func_Diff.Sigmoid_diff(sloj2);

            for (int i = 0; i < sigma.Length; i++)
            {
                for (int j = 0; j < sigma.Length; j++)
                {
                    y[i] = (sigma[j] * weight_2[i, j]);
                }
                y[i] = s[i] * y[i];
            }

            return y;
        }

      

        public virtual void Gradient_delta_2(double[] sigma, double[] sloj2)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    weight_2_delta[i, j] = sloj2[i] * sigma[j];
                    bias_1_delta[i, j] = sigma[j];
                }

            }

        }

        public virtual void Weight_2_update(double E1, double a1)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    weight_2[i, j] = weight_2[i, j] + (E1 * weight_2_delta[i, j] + a1 * weight_2_old[i, j]);
                    bias1[i] = bias1[i] + (E1 * bias_1_delta[i, j]);
                }

                bias1[i] = bias1[i] + (a1 * bias_1_old[i]);
            }
        }


        public virtual void Gradient_delta_1(double[] sigma, double[] sloj1)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    weight_1_delta[i, j] = sloj1[i] * sigma[j];
                    bias_0_delta[i, j] = sigma[j];
                }
            }
        }

        public virtual void Weight_1_update(double E1, double a1)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    weight_1[i, j] = weight_1[i, j] + (E1 * weight_1_delta[i, j] + a1 * weight_1_old[i, j]);
                    bias0[i] = bias0[i] + (E1 * bias_0_delta[i, j]);
                }

                bias0[i] = bias0[i] + (a1 * bias_0_old[i]);
            }
        }


        public virtual void Get_weight_1(double[,] w1)
        {
            this.weight_1 = w1;
        }

        public virtual void Get_weight_2(double[,] w2)
        {
            this.weight_2 = w2;
        }

        public virtual void Write_in_file_weight_1(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_weight_1(razmer_layer_1_in, razmer_data_in, weight_1);
        }

        public virtual void Write_in_file_weight_2(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_weight_1(razmer_layer_2_in, razmer_layer_1_in, weight_2);
        }

        public virtual void Write_in_file_bias_0(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_bias_1(razmer_layer_1_in, bias0);
        }

        public virtual void Write_in_file_bias_1(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_bias_1(razmer_layer_2_in, bias1);
        }

        public virtual void Read_in_file_bias_1(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            bias0 = reader_Weight.Read_in_file_bias_1();
        }

        public virtual void Read_in_file_bias_2(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            bias1 = reader_Weight.Read_in_file_bias_1();                     
        }


        public virtual void Read_in_file_weight_1(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            weight_1 = reader_Weight.Read_in_file_weight_1(weight_1);
        }


        public void Read_in_file_weight_2(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            weight_2 = reader_Weight.Read_in_file_weight_1(weight_2);
        }

        public double[] Obezpaz(double[] x)
        {
            double[] y = new double[x.Length];
            double sum = x.Sum();

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[i] / sum;
            }
            return y;
        }

      
        public virtual double[] Sigma_output(double[] out_ideal, double[] out_actual, double[] diff)
        {
            double[] y = new double[out_ideal.Length];
            for (int i = 0; i < out_ideal.Length; i++)
            {
                y[i] = (out_ideal[i] - out_actual[i]) * diff[i];
            }
            return y;
        }

        public double Grad_ab(double sigma, double out_a)
        {
            return sigma * out_a;
        }

        public double Delta_weight(double E, double Grad_ab, double A, double delta_weight_old)
        {
            double y = E * Grad_ab + A * delta_weight_old;
            return y;
        }

        public void Add_weight_1()
        {
            for (int i = 0; i < razmer_layer_1_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    weight_1[i, j] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
                }
            }
        }

        public void Add_weight_2()
        {           
            for (int i = 0; i < razmer_layer_2_in; i++)
            {
                for (int j = 0; j < razmer_layer_1_in; j++)
                {
                    weight_2[i, j] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;                 
                }
            }
        }

        public void Add_bias_0() {

            for (int j = 0; j < razmer_layer_1_in; j++)
            {
                bias0[j]= System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
            }
        }

        public void Add_bias_1()
        {
            for (int j = 0; j < razmer_layer_2_in; j++)
            {
                bias1[j] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
            }
        }

        public virtual double[] Perzertron_forward(double[] x)
        {
            double[] y = new double[x.Length];

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

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }
            return y;
        }

        public virtual double[] Perzertron_forward_softmax(double[] x)
        {
            double[] y = new double[x.Length];
            double[] z;

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    y[i] = y[i] + (x[i] * weight_2[j, i]);
                }
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias1[i];
            }

            z = Activation_Func.Softmax(y);
            /*     for (int i = 0; i < x.Length; i++)
                 {

                     y[i] = sigmoid(y[i]);
                 }*/

            return z;

        }

        public double MSE(double[] true_result, double[] numerical_result)
        {

            double d = 0;

            for (int i = 0; i < true_result.Length; i++)
            {
                d += Math.Pow((true_result[i] - numerical_result[i]), 2);
            }
            d /= System.Convert.ToDouble(true_result.Length);

            return d;

        }

        public void MSE_add(double[] true_result, double[] numerical_result)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                mps_number[i] = mps_number[i] + Math.Pow((true_result[i] - numerical_result[i]), 2);
            }
        }

        public double[] MSE_return(double aa)
        {
            double[] mps_number1 = new double[mps_number.Length];

            for (int i = 0; i < razmer_data_in; i++)
            {
                mps_number1[i] = mps_number[i] / aa;
            }

            return mps_number1;
        }

        public void MSE_zero()
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                mps_number[i] = 0;
            }
        }

    }
}
