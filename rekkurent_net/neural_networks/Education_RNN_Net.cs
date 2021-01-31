using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Education_RNN_Net : Education_net
    {
        private int razmer_data_in;
        private int razmer_layer_1_in;
        private int razmer_layer_2_in;

        public double[] state_RNN;
        public double[] state_RNN_old;

        public double[,] state_Matrix_RNN;
        public double[,] state_Matrix_RNN_delta;

        int length_input_data;

        /// <summary>
        /// Конструктор двуслойной RNN-сети
        /// </summary>
        /// <param name="razmer1">Число элементов массива входного слоя</param>
        /// <param name="razmer2">Число элементов массива внутреннего слоя</param>
        /// <param name="razmer3">Число элементов массива выходного слоя</param>
        /// <param name="length_inputdata">Число элементов общей последовательности</param>
        public Education_RNN_Net(int razmer1, int razmer2, int razmer3, int length_inputdata) : base(razmer1, razmer2, razmer3) {

            state_RNN = new double[razmer1];
            state_RNN_old = new double[razmer1];

            razmer_data_in = razmer1;
            razmer_layer_1_in = razmer2;
            razmer_layer_2_in = razmer3;

            state_Matrix_RNN = new double[razmer_layer_1_in, razmer_data_in];
            state_Matrix_RNN_delta = new double[razmer_layer_1_in, razmer_data_in];

            length_input_data = length_inputdata;

        }

        public void Shift_state_RNN() {

            for (int i = 0; i < state_RNN.Length; i++) {
                state_RNN_old[i] = state_RNN[i];
            }        
        }

        public void Shift_state_back_RNN()
        {

            for (int i = 0; i < state_RNN.Length; i++)
            {
               state_RNN[i] = state_RNN_old[i];
            }
        }

        public void Set_state_RNN(double[] w2)
        {
            this.state_RNN = w2;
        }
        /// <summary>
        /// Записывает в файл массив с данными о внутреннем состаянии сети
        /// </summary>
        /// <param name="name_file">Имя файла</param>
        public void Write_in_file_state_RNN(String name_file)
        {
            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_bias_1(razmer_data_in, state_RNN);          
        }

        /// <summary>
        /// Записывает в файл двумерный массив с данными о внутреннем состаянии сети
        /// </summary>
        /// <param name="name_file"></param>
        public void Write_in_file_state_Matrix_RNN(String name_file) {

            Writer_weight writer_Weight = new Writer_weight(name_file);
            writer_Weight.Set_in_file_weight_1(razmer_data_in, razmer_layer_1_in, state_Matrix_RNN);
        }

        /// <summary>
        /// Считывает из файла массив с данными о внутреннем состаянии сети
        /// </summary>
        /// <param name="name_file">Имя файла</param>
        public void Read_out_file_state_RNN(String name_file)
        {
             Reader_weight reader_Weight = new Reader_weight(name_file);
             state_RNN = reader_Weight.Read_in_file_bias_1();   
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name_file"></param>
        public void Read_out_file_state_Matrix_RNN(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            state_Matrix_RNN = reader_Weight.Read_in_file_weight_1(state_Matrix_RNN);
        }




        /// <summary>
        /// Шаг вперед по первому слою
        /// </summary>
        /// <param name="x">Входные данные</param>
        /// <returns></returns>
        public override double[] Perzertron_forward(double[] x)
        {
            double[] y = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    y[i] = y[i] + (x[i] * weight_1[j, i])+state_Matrix_RNN[j, i];
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


        /// <summary>
        /// Шаг вперед по памяти
        /// </summary>
        /// <param name="x"></param>
        public void RNN_state_forward(double[] x)
        {
            Shift_state_RNN();//Заполняем предыдущее состояние ?

            for (int i = 0; i < state_RNN.Length; i++)
            {
                state_RNN[i] = x[i];
            }
        }

        /// <summary>
        /// Шаг вперен по второму слою
        /// </summary>
        /// <param name="x"> массив внутреннего слоя</param>
        /// <returns></returns>
        public override double[] Perzertron_forward_softmax(double[] x)
        {
            double[] y = new double[x.Length];
          

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

            // z = activation_Func.Softmax(y);

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }

            // return z;
            return y;
        }

        /// <summary>
        /// Шаг назад по памяти
        /// </summary>
        public void RNN_state_Recovery() {

            Matrix_work matrix_Work = new Matrix_work(state_Matrix_RNN);
            matrix_Work.Weight_Convert_in_Weight_divided();

            for (int i = 0; i < length_input_data; i++)
            {
                state_RNN_old = matrix_Work.HelperSolve(state_RNN);
                state_RNN = state_RNN_old;
            }
        }




        public double[] Recovery_State_RNN(double[] old_rnn_state) {

            double[] new_rnn_state = new double[old_rnn_state.Length];
            return new_rnn_state;
        }

        /// <summary>
        /// Заполняет массив внутреннего состояния сети случайными данными (от 0 до 1 с шагом 0,01)
        /// </summary>
        public void Add_RNN()
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                state_RNN[i] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
            }
        }

        public void Add_state_Matrix_RNN()
        {
            for (int i = 0; i < razmer_layer_1_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    state_Matrix_RNN[i, j] = System.Convert.ToDouble(Generation_Math.GenerateDigit_100()) / 100;
                }
            }
        }

        public double[] Delta_RNN(double[] sigma, double[] sloj2)
        {
            double[] y = new double[sigma.Length];
            double[] s = Activation_Func_Diff.Sigmoid_diff(sloj2);

            for (int i = 0; i < sigma.Length; i++)
            {
                for (int j = 0; j < sigma.Length; j++)
                {
                    y[i] = (sigma[j] * state_Matrix_RNN[i, j]);
                }
                y[i] = s[i] * y[i];
            }

            return y;
        }

        public void Gradient_Delta_RNN(double[] sigma, double[] sloj1)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    state_Matrix_RNN_delta[i, j] = sloj1[i] * sigma[j];                  
                }
            }
        }

        public virtual void Delta_RNN_1_Update(double E1, double a1)
        {
            for (int i = 0; i < razmer_data_in; i++)
            {
                for (int j = 0; j < razmer_data_in; j++)
                {
                    state_Matrix_RNN[i, j] = state_Matrix_RNN[i, j] + (E1 * state_Matrix_RNN_delta[i, j]);                   
                }                
            }
        }



    }
}
