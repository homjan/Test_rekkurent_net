using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    abstract class Layer_abstract
    {
        protected double[,] weight_1;  
        protected double[] bias0;       

        protected double[,] weight_1_delta;     
        protected double[,] weight_1_old;
       
        protected double[,] bias_0_delta;       
        protected double[] bias_0_old;
        

        int length_data_input;
        int length_data_output;
      
        public double[] mps_number;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input_length">Входной слой</param>
        /// <param name="output_length">Выходной слой</param>
        public Layer_abstract(int input_length, int output_length)
        {

            this.length_data_input = input_length;
            this.length_data_output = output_length;
           

            weight_1 = new double[length_data_output, length_data_input];          
            mps_number = new double[length_data_input];

            weight_1_delta = new double[length_data_output, length_data_input];          
            weight_1_old = new double[length_data_output, length_data_input];
            
            bias0 = new double[length_data_output];            
            bias_0_delta = new double[length_data_output, length_data_input];            
            bias_0_old = new double[length_data_output];
           
        }

        public virtual void Shift_Weights()
        {
            weight_1_old = weight_1;           

            bias_0_old = bias0;           
        }
        /// <summary>
        /// Рассчитать изменеия весов
        /// </summary>
        /// <param name="sigma"></param>
        /// <param name="sloj1"></param>
        public virtual void Gradient_delta_1(double[] sigma, double[] sloj1)
        {
            for (int i = 0; i < length_data_input; i++)
            {
                for (int j = 0; j < length_data_input; j++)
                {
                    weight_1_delta[i, j] = sloj1[i] * sigma[j];
                    bias_0_delta[i, j] = sigma[j];
                }
            }
        }

        /// <summary>
        /// Обновить веса на рассчитанную дельту
        /// </summary>
        /// <param name="E1"></param>
        /// <param name="a1"></param>
        public virtual void Weight_1_update(double E1, double a1)
        {
            for (int i = 0; i < length_data_input; i++)
            {
                for (int j = 0; j < length_data_input; j++)
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

        /// <summary>
        /// Записать в файл веса
        /// </summary>
        /// <param name="name_file"></param>
        public virtual void Write_to_file_weight_1(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_weight_1(length_data_output, length_data_input, weight_1);
        }
      /// <summary>
      /// Записать в файл смещение
      /// </summary>
      /// <param name="name_file"></param>
        public virtual void Write_to_file_bias_1(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_bias_1(length_data_output, bias0);
        }

      /// <summary>
      /// Прочитать из файла смещения
      /// </summary>
      /// <param name="name_file"></param>
        public virtual void Read_from_file_bias_1(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            bias0 = reader_Weight.Read_in_file_bias_1();
        }

      /// <summary>
      /// Прочитать из файла веса
      /// </summary>
      /// <param name="name_file"></param>
        public virtual void Read_from_file_weight_1(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            weight_1 = reader_Weight.Read_in_file_weight_1(weight_1);
        }

   
        /// <summary>
        /// Рассчитать частную производную от активации
        /// </summary>
        /// <param name="out_ideal"></param>
        /// <param name="out_actual"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public virtual double[] Sigma_output(double[] out_ideal, double[] out_actual, double[] diff)
        {
            double[] y = new double[out_ideal.Length];
            for (int i = 0; i < out_ideal.Length; i++)
            {
                y[i] = (out_ideal[i] - out_actual[i]) * diff[i];
            }
            return y;
        }

        public virtual double Grad_ab(double sigma, double out_a)
        {
            return sigma * out_a;
        }
        /// <summary>
        /// Рассчитать изменение весов
        /// </summary>
        /// <param name="E"></param>
        /// <param name="Grad_ab"></param>
        /// <param name="A"></param>
        /// <param name="delta_weight_old"></param>
        /// <returns></returns>
        public virtual double Delta_weight(double E, double Grad_ab, double A, double delta_weight_old)
        {
            double y = E * Grad_ab + A * delta_weight_old;
            return y;
        }
        /// <summary>
        /// Сгенерировать случайные веса
        /// </summary>
        public virtual void Add_weight_1()
        {
            for (int i = 0; i < length_data_output; i++)
            {
                for (int j = 0; j < length_data_input; j++)
                {
                    weight_1[i, j] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
                }
            }
        }

        /// <summary>
        /// Сгенерировать случаное смещение
        /// </summary>
        public virtual void Add_bias_0()
        {

            for (int j = 0; j < length_data_output; j++)
            {
                bias0[j] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
            }
        }
        /// <summary>
        /// Считать перцептрон вперед
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
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



    }
}
