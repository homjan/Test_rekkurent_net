using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    public class Layer_RNN
    {
        private int input_Length;
        private int output_Length;
        private int rekkurent_Length;

        private int input_Length_Cell;
        private int output_Length_Cell;

        private double[] x;
        private double[] x_element; 
        private double[] y;
        private double[] y_element;

        IRekkurent_Cell_Element cell_Element;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">входной вектор</param>
        /// <param name="output_length">Длина выходного вектора</param>
        /// <param name="rek_length">Длина реккурентной последовательности</param>
        /// <param name="input_length_cell"> Длина входного элемента ячейки</param>
        /// <param name="output_length_cell">Длина выходного элемента ячейки</param>
        public Layer_RNN(double[] x1, int output_length, int rek_length, int input_length_cell, int output_length_cell) {

            x = x1;
            input_Length = x1.Length;
            output_Length = output_length;

            y = new double[output_length];

            rekkurent_Length = rek_length;
            x_element = new double[rek_length];

            input_Length_Cell = input_length_cell;
            output_Length_Cell = output_length_cell;

           
        }

        public void Set_X(double[] x1) 
        {
            x = x1;        
        }



        public void Inizialize_RNN_Cell() { 
         
            cell_Element = new Layer_RNN_Cell(input_Length_Cell, output_Length_Cell, rekkurent_Length);                    
        }

        public void Inizialize_LSTM_Cell() { 
        
            cell_Element = new Layer_LSTM_Cell(input_Length_Cell, output_Length_Cell, rekkurent_Length);

        }

        public void Inizialize_GRU_Cell() {

            cell_Element = new Layer_GRU_Cell(input_Length_Cell, output_Length_Cell, rekkurent_Length);

        }
        /// <summary>
        /// Формируем и рассчитываем вперед реккурентный слой
        /// </summary>
        /// <param name="bias_adres"></param>
        /// <param name="weight_adres"></param>
        /// <param name="rec_weight_adres"></param>
        public void Build_RNN_Layer(string bias_adres, string weight_adres, string rec_weight_adres) {

            Made_Y_using_Y_element(0.0);

            cell_Element.Read_from_file_bias_1(bias_adres);
            cell_Element.Read_from_file_weight_1(weight_adres);
            cell_Element.Read_from_file_state_Matrix(rec_weight_adres);
            cell_Element.Divide_All();
        }
        public void Calculate_RNN_Layer() 
        { 
            for (int i = rekkurent_Length; i < input_Length; i=i+y_element.Length)//Инкремент должен регулироваться и задавать величину шага
            {
                Cut_Rekkurent_element(i);
                cell_Element.Calculate_Full_Layer(x_element);
                y_element = cell_Element.Get_output_Result();
                Add_Y_Element(y_element, i);
            }

           

        }

        /// <summary>
        /// Вырезаем из всей входящей последовательности обрабатываемый участок - вектор
        /// </summary>
        /// <param name="left_Border">Левая граница последовательности</param>
        private void Cut_Rekkurent_element(int right_Border) {

            for (int i = right_Border - rekkurent_Length; i < right_Border; i++)
            {
                x_element[i- right_Border+rekkurent_Length] = x[i];
            }
        
        }

        /// <summary>
        /// Вставляем в выходящую последовательность обработанный участок - вектор
        /// </summary>
        /// <param name="element"></param>
        /// <param name="right_Border">Правая граница</param>
        private void Add_Y_Element(double[] element, int right_Border) {

            for (int i = right_Border; i < right_Border+element.Length; i++)
            {
                y[i] = element[i-right_Border];
            }
        }

        /// <summary>
        /// Устанавливаем первые элементы последовательности, которые немогут быть посчитаны
        /// </summary>
        private void Made_Y_using_Y_element(double q) {

            for (int i = 0; i < rekkurent_Length; i++)
            {
                y[i] = q;
            }           
        }

        public double[] Get_Y() 
        {
            return y;
        }

    }
}
