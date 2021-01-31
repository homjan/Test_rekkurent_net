using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    class Layer_GRU_Cell : Layer_abstract, IRekkurent_Cell_Element
    {
        public double[] state_GRU;      

        public double[,] state_Matrix_GRU;

        private double[] output_Result;

        public int number_Steps_GRU;

        private int step = 0;

        private int input_Length;
        private int output_Length;
        private int GRU_Row_Length;

        /// <summary>
        /// //////////////////////////////////////////////////////
        /// </summary>

        public double[,] matrix_GRU_1_Update_Gate;
        public double[,] matrix_GRU_2_Reset_Gate;
        public double[,] matrix_GRU_3_Candidate_Cell;
       

        public double[] bias_1_Update_Gate;
        public double[] bias_2_Reset_Gate;
        public double[] bias_3_Candidate_Cell;
       

        public double[,] weight_1_Update_Gate;
        public double[,] weight_2_Reset_Gate;
        public double[,] weight_3_Candidate_Cell;    

        public double[,] weight_1_Small_Update_Gate;
        public double[,] weight_2_Small_Reset_Gate;
        public double[,] weight_3_Small_Candidate_Cell;
        /// <summary>
        /// Конструктор GRU ячейки
        /// </summary>
        /// <param name="input_length">Длина входного векторв</param>
        /// <param name="output_length">Длина выходного вектора</param>
        /// <param name="RNN_row_length">Длина последовательности</param>
        public Layer_GRU_Cell(int input_length, int output_length, int RNN_row_length) : base(input_length, output_length)
        {
            input_Length = input_length;
            output_Length = output_length;
            GRU_Row_Length = RNN_row_length;

            number_Steps_GRU = RNN_row_length;

            state_GRU = new double[output_length];            

            for (int i = 0; i < output_length; i++)
            {
                state_GRU[i] = 0;               
            }

            weight_1_Small_Update_Gate = new double[output_length, input_Length];
            weight_2_Small_Reset_Gate = new double[output_length, input_Length];
            weight_3_Small_Candidate_Cell = new double[output_length, input_Length];          

            state_Matrix_GRU = new double[output_length, 3 * output_length];
            weight_1 = new double[output_length, 3 * input_length];
            bias0 = new double[3 * output_length];

            matrix_GRU_1_Update_Gate = new double[output_length, output_length];
            matrix_GRU_2_Reset_Gate = new double[output_length, output_length];
            matrix_GRU_3_Candidate_Cell = new double[output_length, output_length];
            

            bias_1_Update_Gate = new double[output_length];
            bias_2_Reset_Gate = new double[output_length];
            bias_3_Candidate_Cell = new double[output_length];
           

            weight_1_Update_Gate = new double[output_length, input_length];
            weight_2_Reset_Gate = new double[output_length, input_length];
            weight_3_Candidate_Cell = new double[output_length, input_length];
            

        }

        public double[] Get_output_Result()
        {
            return output_Result;
        }
        /// <summary>
        /// Считать из файла матрицу с памятью
        /// </summary>
        /// <param name="name_file"></param>
        public void Read_from_file_state_Matrix(String name_file)
        {
            Reader_weight reader_Weight = new Reader_weight(name_file);
            state_Matrix_GRU = reader_Weight.Read_in_file_weight_1(state_Matrix_GRU);
        }
        /// <summary>
        /// Рассчитать слой вперед
        /// </summary>
        /// <param name="x"></param>
        public void Calculate_Full_Layer(double[] x)
        {
           
            double[] x_small = new double[input_Length];

            while (step < input_Length)
            {
                x_small = Initialize_Input_Object(x);//Вырезаем и инициализируем следующий входной массив последовательности
                Initialize_Weights_Small();// Вырезаем и инициализируем веса для следующего массива

                output_Result = Perzertron_forward(x_small);//Считаем шаг

                step = step + GRU_Row_Length;//переходим на следующий элемент
            }

            step = 0;

        }

        public void Divide_All()
        {
            Divide_GRU_Matrix();
            Divide_GRU_Weight();
            Divide_GRU_Bias();
        }

        public void Set_state_GRU(double[] w2)
        {
            this.state_GRU = w2;
        }
      
        private void Divide_GRU_Matrix()
        {
            for (int i = 0; i < weight_1.GetLength(0); i++)
            {
                for (int j = 0; j < weight_1.GetLength(1); j++)
                {
                    if (j < output_Length)
                    {
                        matrix_GRU_1_Update_Gate[i, j] = state_Matrix_GRU[i, j];
                    }
                    if (j >= output_Length && j < 2 * output_Length)
                    {
                        matrix_GRU_2_Reset_Gate[i, j - output_Length] = state_Matrix_GRU[i, j];
                    }
                    if (j >= 2 * output_Length && j < 3 * output_Length)
                    {
                        matrix_GRU_3_Candidate_Cell[i, j - 2 * output_Length] = state_Matrix_GRU[i, j];
                    }
                   
                }
            }
        }

        private void Divide_GRU_Weight()
        {
            for (int i = 0; i < weight_1.GetLength(0); i++)
            {
                for (int j = 0; j < weight_1.GetLength(1); j++)
                {
                    if (j < output_Length)
                    {
                        weight_1_Update_Gate[i, j] = weight_1[i, j];
                    }
                    if (j >= output_Length && j < 2 * output_Length)
                    {
                        weight_2_Reset_Gate[i, j - output_Length] = weight_1[i, j];
                    }
                    if (j >= 2 * output_Length && j < 3 * output_Length)
                    {
                        weight_3_Candidate_Cell[i, j - 2 * output_Length] = weight_1[i, j];
                    }                    
                }
            }

        }
        private void Divide_GRU_Bias()
        {
            for (int i = 0; i < bias0.Length; i++)
            {
                if (i < output_Length)
                {
                    bias_1_Update_Gate[i] = bias0[i];
                }
                if (i >= output_Length && i < 2 * output_Length)
                {
                    bias_2_Reset_Gate[i - output_Length] = bias0[i];
                }
                if (i >= 2 * output_Length && i < 3 * output_Length)
                {
                    bias_3_Candidate_Cell[i - 2 * output_Length] = bias0[i];
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

            for (int i = 0; i < input_Length; i++)
            {                
                    for (int j = 0; j < output_Length; j++)
                    {
                        weight_1_Small_Update_Gate[j, i] = weight_1_Update_Gate[j, i];
                        weight_2_Small_Reset_Gate[j, i] = weight_2_Reset_Gate[j, i];
                        weight_3_Small_Candidate_Cell[j, i] = weight_3_Candidate_Cell[j, i];
                    }
                
            }
        }

        public override double[] Perzertron_forward(double[] x)
        {
            double[] y = new double[weight_1_Small_Update_Gate.GetLength(0)];
            double[] y_Left = new double[weight_1_Small_Update_Gate.GetLength(0)];
            double[] y_Right = new double[weight_1_Small_Update_Gate.GetLength(0)];



            double[] one = new double[weight_1_Small_Update_Gate.GetLength(0)];

            for (int i = 0; i < one.Length; i++)
            {
                one[i] = 1;
            }

            double[] update_Cell = Update_Gate_1_Forward(x);
            double[] reset_Cell = Reset_Gate_2_Forward(x);
            double[] candidate_Cell = Candidate_Cell_Gate_3_Forward(x, reset_Cell);



            y_Left = Matrix_work.Vector_Multiplication_Adamar_Shur(Matrix_work.Vector_Substraction(one, update_Cell), candidate_Cell);
            y_Right = Matrix_work.Vector_Multiplication_Adamar_Shur(update_Cell, state_GRU);
            y = Matrix_work.Vector_Sum(y_Left, y_Right);

           Set_state_GRU(y);

            return y;
        }


        private double[] Update_Gate_1_Forward(double[] x)
        {
            double[] y = new double[weight_1_Small_Update_Gate.GetLength(1)];

            for (int i = 0; i < weight_1_Small_Update_Gate.GetLength(1); i++)
            {
                for (int j = 0; j < weight_1_Small_Update_Gate.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_1_Small_Update_Gate[j, i]) + (state_GRU[i] * matrix_GRU_1_Update_Gate[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_1_Update_Gate[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }
            return y;
        }

        private double[] Reset_Gate_2_Forward(double[] x)
        {
            double[] y = new double[weight_2_Small_Reset_Gate.GetLength(1)];

            for (int i = 0; i < weight_2_Small_Reset_Gate.GetLength(1); i++)
            {
                for (int j = 0; j < weight_2_Small_Reset_Gate.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_2_Small_Reset_Gate[j, i]) + (state_GRU[i] * matrix_GRU_2_Reset_Gate[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_2_Reset_Gate[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Sigmoid(y[i]);
            }
            return y;
        }

        private double[] Candidate_Cell_Gate_3_Forward(double[] x, double[] reset_Gate )
        {
            double[] y = new double[weight_3_Small_Candidate_Cell.GetLength(1)];

            double[] z = Matrix_work.Vector_Multiplication_Adamar_Shur(reset_Gate, state_GRU);

            for (int i = 0; i < weight_3_Small_Candidate_Cell.GetLength(1); i++)
            {
                for (int j = 0; j < weight_3_Small_Candidate_Cell.GetLength(0); j++)
                {
                    y[i] = y[i] + (x[i] * weight_3_Small_Candidate_Cell[j, i]) + (z[i] * matrix_GRU_3_Candidate_Cell[j, i]);
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = y[i] + bias_3_Candidate_Cell[i];
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = Activation_Func.Tanh(y[i]);
            }
            return y;
        }

       
    }
}
