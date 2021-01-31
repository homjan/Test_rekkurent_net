using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    class Layer_RNN_Cell : Layer_abstract, IRekkurent_Cell_Element
    {
        public double[] state_RNN;        

        public double[,] state_Matrix_RNN;

        public int number_Steps_RNN;

        private int step = 0;

        public double[,] weight_small;

        private int input_Length;
        private int output_Length;
        private int RNN_Row_Length;

        private double[] output_Result;

        public Layer_RNN_Cell(int input_length, int output_length, int RNN_row_length) : base(input_length, output_length)
        {
            input_Length = input_length;//Длина входного вектора
            output_Length = output_length;//Длина выходного вектора
            RNN_Row_Length = RNN_row_length;//Длина подаваемой последовательности

            output_Result = new double[output_length];//Результат

            state_RNN = new double[output_length];//Ячейка памяти
            weight_small = new double[output_length, input_length];
           
            for (int i = 0; i < output_length; i++)
            {
                state_RNN[i] = 0;
            }

            state_Matrix_RNN = new double[output_length, output_length];//Размерность матрицы памяти

            number_Steps_RNN = RNN_row_length;

        }

        public double[] Get_output_Result() {

            return output_Result;
        }

        private void Initialize_Weight_Small() 
        {
            for (int i = 0; i < input_Length; i++)
            {                
                    for (int j = 0; j < output_Length; j++)
                    {
                        weight_small[j, i] = weight_1[j, i];
                    }                  
                
            }
        }

        public void Calculate_Full_Layer(double[] x) {
                       
            double[] x_small = new double[input_Length];

            x_small = Initialize_Input_Object(x);//Вырезаем и инициализируем первый входной массив последовательности
            Initialize_Weight_Small();// Вырезаем и инициализируем веса для первого массива
            output_Result = Perzertron_Forward_First_Step(x_small);//Считаем первый шаг

            step = step + input_Length;//переходим на следующий элемент
           
            while (step< RNN_Row_Length)
            {
                x_small = Initialize_Input_Object(x);//Вырезаем и инициализируем следующий входной массив последовательности
                Initialize_Weight_Small();// Вырезаем и инициализируем веса для следующего массива

                output_Result = Perzertron_forward(x_small);//Считаем шаг

                step = step + input_Length;//переходим на следующий элемент
            }

            step = 0;
      
        }

        public void Divide_All() { 
        
        }

        private double[] Initialize_Input_Object(double[] x) {

            double[] y = new double[input_Length];
            int y1 = 0;

            for (int i = step; i < step + input_Length; i++)
            {
                y[y1] = x[i-step];
                y1++;
            }
            return y;

        }


        public void Set_state_RNN(double[] w2)
        {
            this.state_RNN = w2;
        }

        public void Read_from_file_state_Matrix(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            state_Matrix_RNN = reader_Weight.Read_in_file_weight_1(state_Matrix_RNN);
        }


        public override double[] Perzertron_forward(double[] x)
        {
            double[] y = new double[weight_small.GetLength(1)];

            for (int i = 0; i < weight_small.GetLength(1); i++)
            {
                for (int j = 0; j < weight_small.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_small[j, i]) + (state_RNN[i] * state_Matrix_RNN[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias0[i];
            }

            for (int i = 0; i < x.Length; i++)
            {

                y[i] = Activation_Func.Tanh(y[i]);
            }

            Set_state_RNN(y);

            return y;
        }

        public double[] Perzertron_Forward_First_Step(double[] x)
        {
            double[] y = new double[weight_small.GetLength(1)];

            for (int i = 0; i < weight_small.GetLength(1); i++)
            {
                for (int j = 0; j < weight_small.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_small[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias0[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Tanh(y[i]);
            }

            Set_state_RNN(y);
            return y;

        }


    }
}
