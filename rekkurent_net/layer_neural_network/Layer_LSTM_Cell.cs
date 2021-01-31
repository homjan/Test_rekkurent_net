using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    class Layer_LSTM_Cell : Layer_abstract, IRekkurent_Cell_Element
    {
        public double[] state_LSTM;
        public double[] state_LSTM_Memory;

        public double[,] state_Matrix_LSTM;

        public int number_Steps_RNN;

        private int step = 0;

        private double[] output_Result;

        private int input_Length;
        private int output_Length;
        private int LSTM_Row_Length;

        /// <summary>
        /// //////////////////////////////////////////////////////
        /// </summary>

        public double[,] matrix_LSTM_1_Candidate_Cell;
        public double[,] matrix_LSTM_2_Input;
        public double[,] matrix_LSTM_3_Forget;
        public double[,] matrix_LSTM_4_Output;

        public double[] bias_1_Candidate_Cell;
        public double[] bias_2_Input;
        public double[] bias_3_Forget;
        public double[] bias_4_Output;

        public double[,] weight_1_Candidate_Cell;
        public double[,] weight_2_Input;
        public double[,] weight_3_Forget;
        public double[,] weight_4_Output;

        public double[,] weight_1_Small_Candidate_Cell;
        public double[,] weight_2_Small_Input;
        public double[,] weight_3_Small_Forget;
        public double[,] weight_4_Small_Output;

        public Layer_LSTM_Cell(int input_length, int output_length, int RNN_row_length) : base(input_length, output_length)
        {
            input_Length = input_length;
            output_Length = output_length;
            LSTM_Row_Length = RNN_row_length;

            number_Steps_RNN = RNN_row_length;

            state_LSTM = new double[output_length];
            state_LSTM_Memory = new double[output_length];

            for (int i = 0; i < output_length; i++)
            {
                state_LSTM[i] = 0;
                state_LSTM_Memory[i] = 0;
            }

            weight_1_Small_Candidate_Cell = new double[output_length, input_length];
            weight_2_Small_Input = new double[output_length, input_length];
            weight_3_Small_Forget = new double[output_length, input_length];
            weight_4_Small_Output = new double[output_length, input_length];

        state_Matrix_LSTM = new double[output_length, 4*output_length];
            weight_1 = new double[output_length, 4 * input_length];
            bias0 = new double[4 * output_length];

            matrix_LSTM_1_Candidate_Cell = new double[output_length, output_length];
            matrix_LSTM_2_Input = new double[output_length, output_length];
            matrix_LSTM_3_Forget = new double[output_length, output_length];
            matrix_LSTM_4_Output = new double[output_length, output_length];

            bias_1_Candidate_Cell = new double[output_length];
            bias_2_Input = new double[output_length];
            bias_3_Forget = new double[output_length];
            bias_4_Output = new double[output_length];

            weight_1_Candidate_Cell = new double[output_length, input_length];
            weight_2_Input = new double[output_length, input_length]; 
            weight_3_Forget = new double[output_length, input_length];
            weight_4_Output = new double[output_length, input_length];

        }

        public double[] Get_output_Result()
        {
            return output_Result;
        }

        public void Calculate_Full_Layer(double[] x)
        {            
            double[] x_small = new double[input_Length];                       

            while (step < LSTM_Row_Length)
            {
                x_small = Initialize_Input_Object(x);//Вырезаем и инициализируем следующий входной массив последовательности
                Initialize_Weights_Small();// Вырезаем и инициализируем веса для следующего массива

                output_Result = Perzertron_forward(x_small);//Считаем шаг

                step = step + input_Length;//переходим на следующий элемент
            }

            step = 0;

        }

        public void Divide_All()
        {
            Divide_LSTM_Matrix();
            Divide_LSTM_Weight();
            Divide_LSTM_Bias();
        }

        public void Read_from_file_state_Matrix(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            state_Matrix_LSTM = reader_Weight.Read_in_file_weight_1(state_Matrix_LSTM);
        }

        public void Set_state_LSTM(double[] w2)
        {
            this.state_LSTM = w2;
        }
        public void Set_state_LSTM_Memory(double[] w2)
        {
            this.state_LSTM_Memory = w2;
        }

        private void Divide_LSTM_Matrix()
        {
            for (int i = 0; i < weight_1.GetLength(0); i++)
            {
                for (int j = 0; j < weight_1.GetLength(1); j++)
                {
                    if (j < output_Length)
                    {
                        matrix_LSTM_1_Candidate_Cell[i, j] = state_Matrix_LSTM[i, j];
                    }
                    if (j >= output_Length && j < 2 * output_Length)
                    {
                        matrix_LSTM_2_Input[i, j - output_Length] = state_Matrix_LSTM[i, j];
                    }
                    if (j >= 2 * output_Length && j < 3 * output_Length)
                    {
                        matrix_LSTM_3_Forget[i, j - 2 * output_Length] = state_Matrix_LSTM[i, j];
                    }
                    if (j >= 3 * output_Length && j < 4 * output_Length)
                    {
                        matrix_LSTM_4_Output[i, j - 3 * output_Length] = state_Matrix_LSTM[i, j];
                    }
                }
            }
        }

        private void Divide_LSTM_Weight()
        {
            for (int i = 0; i < weight_1.GetLength(0); i++)
            {
                for (int j = 0; j < weight_1.GetLength(1); j++)
                {
                    if (j < output_Length)
                    {
                        weight_1_Candidate_Cell[i, j] = weight_1[i, j];
                    }
                    if (j >= output_Length && j < 2 * output_Length)
                    {
                        weight_2_Input[i, j - output_Length] = weight_1[i, j];
                    }
                    if (j >= 2 * output_Length && j < 3 * output_Length)
                    {
                        weight_3_Forget[i, j - 2 * output_Length] = weight_1[i, j];
                    }
                    if (j >= 3 * output_Length && j < 4 * output_Length)
                    {
                        weight_4_Output[i, j - 3 * output_Length] = weight_1[i, j];
                    }
                }
            }

        }
        private void Divide_LSTM_Bias()
        {
            for (int i = 0; i < bias0.Length; i++)
            {
                if (i < output_Length)
                {
                    bias_1_Candidate_Cell[i] = bias0[i];
                }
                if (i >= output_Length && i < 2 * output_Length)
                {
                    bias_2_Input[i - output_Length] = bias0[i];
                }
                if (i >= 2 * output_Length && i < 3 * output_Length)
                {
                    bias_3_Forget[i - 2 * output_Length] = bias0[i];
                }
                if (i >= 3 * output_Length && i < 4 * output_Length)
                {
                    bias_4_Output[i - 3 * output_Length] = bias0[i];
                }

            }
        }

        private double[] Initialize_Input_Object(double[] x)
        {
            double[] y = new double[input_Length];
            int y1 = 0;

            for (int i = step; i < step + input_Length; i++)
            {
                y[y1] = x[i-step];
                y1++;
            }
            return y;
        }

        private double[] Initialize_Output_Object(double[] x)
        {
            double[] y = new double[output_Length];
            int y1 = 0;

            for (int i = step; i < step + output_Length; i++)
            {
                y[i] = x[y1-step];
                y1++;
            }
            return y;
        }

        private void Initialize_Weights_Small()
        {
            for (int i = step; i < input_Length; i++)
            {
                
                    for (int j = 0; j < output_Length; j++)
                    {
                        weight_1_Small_Candidate_Cell[j, i] = weight_1_Candidate_Cell[j, i];
                        weight_2_Small_Input[j, i] = weight_2_Input[j, i];
                        weight_3_Small_Forget[j, i] = weight_3_Forget[j, i];
                        weight_4_Small_Output[j, i] = weight_4_Output[j, i];
                    }                
            }
        }

        public override double[] Perzertron_forward(double[] x)
        {
            double[] y = new double[weight_1_Small_Candidate_Cell.GetLength(0)];

            double[] candidate_Cell = Candidate_Cell_State_1_Forward(x);
            double[] input_Cell = Input_Gate_2_Forward(x);
            double[] forget_Cell = Forget_Gate_3_Forward(x);
            double[] output_cell = Output_Gate_4_Forward(x);

            double[] cell = Matrix_work.Vector_Sum(Matrix_work.Vector_Multiplication_Adamar_Shur(forget_Cell, state_LSTM_Memory),
                Matrix_work.Vector_Multiplication_Adamar_Shur(input_Cell, candidate_Cell));
            
            Set_state_LSTM_Memory(cell);

            y = Matrix_work.Vector_Multiplication_Adamar_Shur(output_cell, Activation_Cell_Tanh(cell));

            Set_state_LSTM(y);
          
            return y;
        }
              

        private double[] Candidate_Cell_State_1_Forward(double[] x) 
        {
            double[] y = new double[weight_1_Small_Candidate_Cell.GetLength(1)];

            for (int i = 0; i < weight_1_Small_Candidate_Cell.GetLength(1); i++)
            {
                for (int j = 0; j < weight_1_Small_Candidate_Cell.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_1_Small_Candidate_Cell[j, i]) + (state_LSTM[i] * matrix_LSTM_1_Candidate_Cell[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_1_Candidate_Cell[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Tanh(y[i]);
            }
            return y;
        }

        private double[] Input_Gate_2_Forward(double[] x) 
        {
            double[] y = new double[weight_2_Small_Input.GetLength(1)];

            for (int i = 0; i < weight_2_Small_Input.GetLength(1); i++)
            {
                for (int j = 0; j < weight_2_Small_Input.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_2_Small_Input[j, i]) + (state_LSTM[i] * matrix_LSTM_2_Input[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_2_Input[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }
            return y;
        }

        private double[] Forget_Gate_3_Forward(double[] x)
        {
            double[] y = new double[weight_3_Small_Forget.GetLength(1)];

            for (int i = 0; i < weight_3_Small_Forget.GetLength(1); i++)
            {
                for (int j = 0; j < weight_3_Small_Forget.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_3_Small_Forget[j, i]) + (state_LSTM[i] * matrix_LSTM_3_Forget[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_3_Forget[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }
            return y;
        }

        private double[] Output_Gate_4_Forward(double[] x)
        {
            double[] y = new double[weight_4_Small_Output.GetLength(1)];

            for (int i = 0; i < weight_4_Small_Output.GetLength(1); i++)
            {
                for (int j = 0; j < weight_4_Small_Output.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_4_Small_Output[j, i]) + (state_LSTM[i] * matrix_LSTM_4_Output[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_4_Output[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }
                      
            return y;
        }

        private double[] Activation_Cell_Tanh(double[] x) 
        {
            double[] y = new double[x.Length];
          
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Tanh(x[i]);
            }

            return y;

        }


    }
}
