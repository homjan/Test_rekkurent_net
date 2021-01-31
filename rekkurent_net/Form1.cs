using rekkurent_net.reader_and_writer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rekkurent_net
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Сгенерировать выражение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int N_elem = Convert.ToInt32(this.textBox3.Text);

            Generation_string generator1 = new Generation_string(N_elem);
            generator1.Generation_Message();
            string text1 = generator1.Get_message();
            textBox2.Text = text1;

            Generation_Test generator2 = new Generation_Test(text1);
            generator2.Made_Test_String_Out();
            string text2 = generator2.Get_Test_String_Out();

            textBox1.Text = text2;

        }


        /// <summary>
        /// Создать обучающие примеры
        /// и поместить их в папку samples_data
        /// Затем конвертировать символы в соответсвующие числа и поместить результат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            double full_N_data = Convert.ToDouble(this.textBox7.Text);

            int learn_N_data = Convert.ToInt32(full_N_data*0.7);
            int test_N_data = Convert.ToInt32(full_N_data * 0.2);
            int valid_N_data = Convert.ToInt32(full_N_data * 0.1);

            int N_elem = Convert.ToInt32(this.textBox3.Text);

            /*
               string text1 = valid_data[k];// исходные входные данные
                    string text2 = valid_data_result[k];// исходные результаты

                    char[] text2_char = text2.ToCharArray();

                    for (int i = 0; i < text2_char.Length; i++)
                    {
                        text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                        text2_double[i] = Convert.ToDouble(text2_int[i]);
                    }

                    Converter_string_double converter_String_Double = new Converter_string_double(text1);
                    converter_String_Double.Convert_string_double();
                    double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

             */
            double[,] learn_data_double = new double[learn_N_data, N_elem];
            double[,] learn_data_result_double = new double[learn_N_data, N_elem];

            double[,] test_data_double = new double[test_N_data, N_elem];
            double[,] test_data_result_double = new double[test_N_data, N_elem];

            double[,] valid_data_double = new double[valid_N_data, N_elem];
            double[,] valid_data_result_double = new double[valid_N_data, N_elem];

            Generation_string generator1 = new Generation_string(N_elem);
            Generation_Test generator2 = new Generation_Test();

            StreamWriter rw1 = new StreamWriter("Samples_from_education\\samples_data\\Learn_data.txt");
            StreamWriter r2w1 = new StreamWriter("Samples_from_education\\samples_data\\Learn_data_result.txt");


            for (int i = 0; i < learn_N_data; i++)
            {
                generator1.Generation_Message();
                string text1 = generator1.Get_message();
                rw1.WriteLine(text1);

                generator2.Set_Test_String_In(text1);
                generator2.Made_Test_String_Out();
                string text2 = generator2.Get_Test_String_Out();
                r2w1.WriteLine(text2);
               
            }
            rw1.Close();
            r2w1.Close();

            ///////////////////////////////////////////////////////

            File_Reader reader = new File_Reader();
            string[] learn_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Learn_data.txt", learn_N_data + 1);
            string[] learn_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Learn_data_result.txt", learn_N_data + 1);

            for (int k = 0; k < learn_N_data; k++)
            {
                string text11 = learn_data[k];// исходные входные данные
                string text22 = learn_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    learn_data_double[k,i] = data_in[i];
                    learn_data_result_double[k,i] = text2_double[i];
                }              

            }

                
            Writer_weight writer_Weight1 = new Writer_weight("Samples_from_education\\samples_data_double\\Learn_data_double.txt");
            writer_Weight1.Set_in_file_weight_1(learn_N_data, N_elem, learn_data_double);

            Writer_weight writer_Weight2 = new Writer_weight("Samples_from_education\\samples_data_double\\Learn_data_result_double.txt");
            writer_Weight2.Set_in_file_weight_1(learn_N_data, N_elem, learn_data_result_double);

            ///////////////////

            StreamWriter rw2 = new StreamWriter("Samples_from_education\\samples_data\\Test_data.txt");
            StreamWriter r2w2 = new StreamWriter("Samples_from_education\\samples_data\\Test_data_result.txt");

            for (int i = 0; i < test_N_data; i++)
            {
                generator1.Generation_Message();
                string text1 = generator1.Get_message();
                rw2.WriteLine(text1);

                generator2.Set_Test_String_In(text1);
                generator2.Made_Test_String_Out();
                string text2 = generator2.Get_Test_String_Out();
                r2w2.WriteLine(text2);
            }
            rw2.Close();
            r2w2.Close();


            ///////////////////////////////////////////////////////
                       
            string[] test_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Test_data.txt", test_N_data + 1);
            string[] test_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Test_data_result.txt", test_N_data + 1);

            for (int k = 0; k < test_N_data; k++)
            {
                string text11 = test_data[k];// исходные входные данные
                string text22 = test_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    test_data_double[k, i] = data_in[i];
                    test_data_result_double[k, i] = text2_double[i];
                }

            }


            Writer_weight writer_Weight12 = new Writer_weight("Samples_from_education\\samples_data_double\\Test_data_double.txt");
            writer_Weight12.Set_in_file_weight_1(test_N_data, N_elem, test_data_double);

            Writer_weight writer_Weight22 = new Writer_weight("Samples_from_education\\samples_data_double\\Test_data_result_double.txt");
            writer_Weight22.Set_in_file_weight_1(test_N_data, N_elem, test_data_result_double);

            ///////////////////

            StreamWriter rw3 = new StreamWriter("Samples_from_education\\samples_data\\Valid_data.txt");
            StreamWriter r3w2 = new StreamWriter("Samples_from_education\\samples_data\\Valid_data_result.txt");

            for (int i = 0; i < valid_N_data; i++)
            {
                generator1.Generation_Message();
                string text1 = generator1.Get_message();
                rw3.WriteLine(text1);

                generator2.Set_Test_String_In(text1);
                generator2.Made_Test_String_Out();
                string text2 = generator2.Get_Test_String_Out();
                r3w2.WriteLine(text2);
            }
            rw3.Close();
            r3w2.Close();

            ///////////////////////////
           
            string[] valid_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data.txt", valid_N_data + 1);
            string[] valid_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data_result.txt", valid_N_data + 1);

            for (int k = 0; k < valid_N_data; k++)
            {
                string text11 = valid_data[k];// исходные входные данные
                string text22 = valid_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];

                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    valid_data_double[k, i] = data_in[i];
                    valid_data_result_double[k, i] = text2_double[i];
                }

            }


            Writer_weight writer_Weight13 = new Writer_weight("Samples_from_education\\samples_data_double\\Valid_data_double.txt");
            writer_Weight13.Set_in_file_weight_1(valid_N_data, N_elem, valid_data_double);

            Writer_weight writer_Weight23 = new Writer_weight("Samples_from_education\\samples_data_double\\Valid_data_result_double.txt");
            writer_Weight23.Set_in_file_weight_1(valid_N_data, N_elem, valid_data_result_double);

            label12.Text = "Готово";
        }

              

        public char[] converter_int_char(int[] data) {
            char[] new_data = new char[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 1)
                {
                    new_data[i] = '1';
                }

                if (data[i] == 0)
                {
                    new_data[i] = '0';
                }

            }

            return new_data;
        
        }

      
        /// <summary>
        /// Считать LSTM вперед
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_elem = Convert.ToInt32(this.textBox3.Text);

            Generation_string generator1 = new Generation_string(N_elem);
            generator1.Generation_Message();
            string text1 = generator1.Get_message();
            textBox2.Text = text1;

            Generation_Test generator2 = new Generation_Test(text1);
            generator2.Made_Test_String_Out();
            string text2 = generator2.Get_Test_String_Out();

            textBox1.Text = text2;

            ////////////////////////////////////////////

            char[] text2_char = text2.ToCharArray();
            int[] text2_int = new int[text2_char.Length];
            double[] text2_double = new double[text2_char.Length];

            for (int i = 0; i < text2_char.Length; i++)
            {
                text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                text2_double[i] = Convert.ToDouble(text2_int[i]);
            }

            ////////////
            int length_full_array = Convert.ToInt32(textBox3.Text);// - Полная длина последовательности
            int RNN_length = Convert.ToInt32(textBox8.Text);// Длина RNN последовательности - 5
            int output_Length = Convert.ToInt32(textBox11.Text);//Длина выходного вектора - 1
            int input_Length = Convert.ToInt32(textBox12.Text);//Длина выходного вектора - 1

            double[] sloj1 = new double[length_full_array];
            //   double[] sloj2;
            //   double[] sloj3;

            Converter_string_double converter_String_Double = new Converter_string_double(text1);
            converter_String_Double.Convert_string_double();
            double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

            //Обезразмериваем входные данные
            for (int i = 0; i < data_in.Length; i++)
            {
                data_in[i] = data_in[i] / 16.0;
            }

            double[] data_out = new double[length_full_array];

            //////////////////////////////////////////
            ///Создаем слои и считываем веса

            layer_neural_network.Layer_RNN Layer = new layer_neural_network.Layer_RNN(data_in,
                length_full_array, RNN_length, input_Length, output_Length);
            Layer.Inizialize_LSTM_Cell();
            Layer.Build_RNN_Layer("Rekkurent_net\\lstm5-1\\bias0.txt",
                "Rekkurent_net\\lstm5-1\\kernel0.txt",
                "Rekkurent_net\\lstm5-1\\recurrent_kernel0.txt");
            Layer.Calculate_RNN_Layer();

            //Получаем второй слой
            data_out = Layer.Get_Y();

            //Пересчитываем результат для вывода на экран и выводим

            Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
            converter_Double_Result.Convertation();
            int[] result_2 = converter_Double_Result.Get_result();
            char[] result_2_char = converter_int_char(result_2);

            string text3 = new string(result_2_char);

            textBox4.Text = text3;

            for (int i = 0; i < result_2.Length; i++)
            {
                richTextBox1.AppendText(i + "\t" + Math.Round(data_out[i], 3) + "\t" + result_2[i] + "\n");
            }

            //Считаем ошибку          
            double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);

            for (int i = 0; i < Mse1.Length; i++)
            {
                richTextBox2.AppendText(i + "\t" + Math.Round(Mse1[i], 5) + "\n");
            }

            richTextBox2.AppendText("sum" + "\t" + Math.Round(Mse1.Sum(), 5) + "\n");

            richTextBox2.AppendText("\t" + Math.Round(Error_Func.MSE_ob(Mse1), 5) + "\n");



            label12.Text = "Готово";
        }

        /// <summary>
        /// Считать валидационный файл LSTM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_valid = Convert.ToInt32(Convert.ToDouble(textBox7.Text) * 0.1);
            int length_full_array = Convert.ToInt32(textBox3.Text);// - Полная длина последовательности
            int RNN_length = Convert.ToInt32(textBox8.Text);// Длина RNN последовательности - 5
            int output_Length = Convert.ToInt32(textBox11.Text);//Длина выходного вектора - 1
            int input_Length = Convert.ToInt32(textBox12.Text);//Длина выходного вектора - 1


            File_Reader reader = new File_Reader();
            string[] valid_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data.txt", N_valid + 1);
            string[] valid_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data_result.txt", N_valid + 1);

            double[] data_in = new double[length_full_array];


            //////////////////////////////////////////
            ///Создаем слои и считываем веса
            layer_neural_network.Layer_RNN Layer = new layer_neural_network.Layer_RNN(data_in,
                 length_full_array, RNN_length, input_Length, output_Length);
            Layer.Inizialize_LSTM_Cell();
            Layer.Build_RNN_Layer("Rekkurent_net\\lstm5-1\\bias0.txt",
                "Rekkurent_net\\lstm5-1\\kernel0.txt",
                "Rekkurent_net\\lstm5-1\\recurrent_kernel0.txt");
            ////////////////////////////////////////////////////////////////

            double sum_MSE = 0;

            for (int k = 0; k < N_valid; k++)
            {
                string text1 = valid_data[k];// исходные входные данные
                string text2 = valid_data_result[k];// исходные результаты

                char[] text2_char = text2.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];

                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text1);
                converter_String_Double.Convert_string_double();
                data_in = converter_String_Double.Get_cript_message();//Входные данные

                Layer.Set_X(data_in);

                //Получем первый слой
                Layer.Calculate_RNN_Layer();

                double[] data_out = data_out = Layer.Get_Y();


                //Пересчитываем результат для вывода на экран и выводим

                Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
                converter_Double_Result.Convertation();
                int[] result_2 = converter_Double_Result.Get_result();
                char[] result_2_char = converter_int_char(result_2);

                string text3 = new string(result_2_char);
              
                //Считаем ошибку

                double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);
                double s = Error_Func.MSE_ob(Mse1);

                sum_MSE = sum_MSE + s;

                richTextBox2.AppendText("\t" + Math.Round(s, 5) + "\n");
            }


            richTextBox2.AppendText("\n" + "Ошибка на число примеров" + "\t" + Math.Round((sum_MSE / N_valid), 5) + "\n");

            label12.Text = "Готово";
        }

       
        /// <summary>
        /// Считать GPU вперед 1 строка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_elem = Convert.ToInt32(this.textBox3.Text);

            Generation_string generator1 = new Generation_string(N_elem);
            generator1.Generation_Message();
            string text1 = generator1.Get_message();
            textBox2.Text = text1;

            Generation_Test generator2 = new Generation_Test(text1);
            generator2.Made_Test_String_Out();
            string text2 = generator2.Get_Test_String_Out();

            textBox1.Text = text2;

            ////////////////////////////////////////////

            char[] text2_char = text2.ToCharArray();
            int[] text2_int = new int[text2_char.Length];
            double[] text2_double = new double[text2_char.Length];

            for (int i = 0; i < text2_char.Length; i++)
            {
                text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                text2_double[i] = Convert.ToDouble(text2_int[i]);
            }

            ////////////
            int length_full_array = Convert.ToInt32(textBox3.Text);// - Полная длина последовательности
            int RNN_length = Convert.ToInt32(textBox8.Text);// Длина RNN последовательности - 5
            int output_Length = Convert.ToInt32(textBox11.Text);//Длина выходного вектора - 1
            int input_Length = Convert.ToInt32(textBox12.Text);//Длина выходного вектора - 1

            double[] sloj1 = new double[length_full_array];
            //   double[] sloj2;
            //   double[] sloj3;

            Converter_string_double converter_String_Double = new Converter_string_double(text1);
            converter_String_Double.Convert_string_double();
            double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

            //Обезразмериваем входные данные
            for (int i = 0; i < data_in.Length; i++)
            {
                data_in[i] = data_in[i] / 16.0;
            }

            double[] data_out = new double[length_full_array];

            //////////////////////////////////////////
            ///Создаем слои и считываем веса

            layer_neural_network.Layer_RNN Layer = new layer_neural_network.Layer_RNN(data_in,
                length_full_array, RNN_length, input_Length, output_Length);
            Layer.Inizialize_GRU_Cell();
            Layer.Build_RNN_Layer("Rekkurent_net\\gru5_1_reset_after_false\\bias0.txt",
                "Rekkurent_net\\gru5_1_reset_after_false\\kernel0.txt",
                "Rekkurent_net\\gru5_1_reset_after_false\\recurrent_kernel0.txt");
            Layer.Calculate_RNN_Layer();

            //Получаем второй слой
            data_out = Layer.Get_Y();

            //Пересчитываем результат для вывода на экран и выводим

            Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
            converter_Double_Result.Convertation();
            int[] result_2 = converter_Double_Result.Get_result();
            char[] result_2_char = converter_int_char(result_2);

            string text3 = new string(result_2_char);

            textBox4.Text = text3;

            for (int i = 0; i < result_2.Length; i++)
            {
                richTextBox1.AppendText(i + "\t" + Math.Round(data_out[i], 3) + "\t" + result_2[i] + "\n");
            }

            //Считаем ошибку          
            double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);

            for (int i = 0; i < Mse1.Length; i++)
            {
                richTextBox2.AppendText(i + "\t" + Math.Round(Mse1[i], 5) + "\n");
            }

            richTextBox2.AppendText("sum" + "\t" + Math.Round(Mse1.Sum(), 5) + "\n");

            richTextBox2.AppendText("\t" + Math.Round(Error_Func.MSE_ob(Mse1), 5) + "\n");



            label12.Text = "Готово";
        }

        /// <summary>
        /// Считать валидационный файл GRU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_valid = Convert.ToInt32(Convert.ToDouble(textBox7.Text) * 0.1);
            int length_full_array = Convert.ToInt32(textBox3.Text);// - Полная длина последовательности
            int RNN_length = Convert.ToInt32(textBox8.Text);// Длина RNN последовательности - 5
            int output_Length = Convert.ToInt32(textBox11.Text);//Длина выходного вектора - 1
            int input_Length = Convert.ToInt32(textBox12.Text);//Длина выходного вектора - 1


            File_Reader reader = new File_Reader();
            string[] valid_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data.txt", N_valid + 1);
            string[] valid_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data_result.txt", N_valid + 1);

            double[] data_in = new double[length_full_array];


            //////////////////////////////////////////
            ///Создаем слои и считываем веса
            layer_neural_network.Layer_RNN Layer = new layer_neural_network.Layer_RNN(data_in,
                 length_full_array, RNN_length, input_Length, output_Length);
            Layer.Inizialize_GRU_Cell();
            Layer.Build_RNN_Layer("Rekkurent_net\\gru5_1_reset_after_false\\bias0.txt",
                "Rekkurent_net\\gru5_1_reset_after_false\\kernel0.txt",
                "Rekkurent_net\\gru5_1_reset_after_false\\recurrent_kernel0.txt");
            ////////////////////////////////////////////////////////////////

            double sum_MSE = 0;

            for (int k = 0; k < N_valid; k++)
            {
                string text1 = valid_data[k];// исходные входные данные
                string text2 = valid_data_result[k];// исходные результаты

                char[] text2_char = text2.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];

                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text1);
                converter_String_Double.Convert_string_double();
                data_in = converter_String_Double.Get_cript_message();//Входные данные

                Layer.Set_X(data_in);

                //Получем первый слой
                Layer.Calculate_RNN_Layer();

                double[] data_out = data_out = Layer.Get_Y();


                //Пересчитываем результат для вывода на экран и выводим

                Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
                converter_Double_Result.Convertation();
                int[] result_2 = converter_Double_Result.Get_result();
                char[] result_2_char = converter_int_char(result_2);

                string text3 = new string(result_2_char);
               
                //Считаем ошибку

                double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);
                double s = Error_Func.MSE_ob(Mse1);

                sum_MSE = sum_MSE + s;

                richTextBox2.AppendText("\t" + Math.Round(s, 5) + "\n");
            }


            richTextBox2.AppendText("\n" + "Ошибка на число примеров" + "\t" + Math.Round((sum_MSE / N_valid), 5) + "\n");

            label12.Text = "Готово";
        }

        /// <summary>
        /// Создать файлы RNN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            int length_block_in_data = Convert.ToInt32(textBox8.Text);

            Education_RNN_Net _RNN_Net = new Education_RNN_Net(length_block_in_data, length_block_in_data, length_block_in_data, 0);

            _RNN_Net.Add_weight_1();
            _RNN_Net.Add_weight_2();

            _RNN_Net.Write_in_file_weight_1("RNN_net_data\\weight0.txt");
            _RNN_Net.Write_in_file_weight_2("RNN_net_data\\weight1.txt");

            _RNN_Net.Add_bias_0();
            _RNN_Net.Add_bias_1();

            _RNN_Net.Write_in_file_bias_0("RNN_net_data\\bias0.txt");
            _RNN_Net.Write_in_file_bias_1("RNN_net_data\\bias1.txt");

            _RNN_Net.Add_RNN();
            _RNN_Net.Write_in_file_state_RNN("RNN_net_data\\RNN_state.txt");

            _RNN_Net.Add_state_Matrix_RNN();
            _RNN_Net.Write_in_file_state_Matrix_RNN("RNN_net_data\\RNN_matrix.txt");

            label12.Text = "Готово";
        }


        /// <summary>
        /// Создать файлы LSTM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            label12.Text = "Готово";

        }

        /// <summary>
        /// Создать файлы GRU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            label12.Text = "";


            label12.Text = "Готово";
        }

        /// <summary>
        /// Создать файлы перпцетрон
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            int length_data = Convert.ToInt32(textBox3.Text);

            
            layer_neural_network.Layer_Perzertron Layer_1 = new layer_neural_network.Layer_Perzertron(length_data, length_data);

            Layer_1.Add_bias_0();
            Layer_1.Add_weight_1();

            Layer_1.Write_to_file_bias_1("Perzeptron_data\\bias0.txt");
            Layer_1.Write_to_file_weight_1("Perzeptron_data\\kernel0.txt");

            layer_neural_network.Layer_Perzeptron_Softmax Layer_2 = new layer_neural_network.Layer_Perzeptron_Softmax(length_data, length_data);

            Layer_2.Add_bias_0();
            Layer_2.Add_weight_1();

            Layer_2.Write_to_file_bias_1("Perzeptron_data\\bias1.txt");
            Layer_2.Write_to_file_weight_1("Perzeptron_data\\kernel1.txt");

            label12.Text = "Готово";

        }


        /// <summary>
        /// Считать перцептрон 1 строка вперед
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_elem = Convert.ToInt32(this.textBox3.Text);

            Generation_string generator1 = new Generation_string(N_elem);
            generator1.Generation_Message();
            string text1 = generator1.Get_message();
            textBox2.Text = text1;

            Generation_Test generator2 = new Generation_Test(text1);
            generator2.Made_Test_String_Out();
            string text2 = generator2.Get_Test_String_Out();

            textBox1.Text = text2;


            ////////////////////////////////////////////
           
            char[] text2_char = text2.ToCharArray();
            int[] text2_int = new int[text2_char.Length];
            double[] text2_double = new double[text2_char.Length];

            for (int i = 0; i < text2_char.Length; i++)
            {
                text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                text2_double[i] = Convert.ToDouble(text2_int[i]);
            }

            ////////////
            int length_full_array = Convert.ToInt32(textBox3.Text);// -

            double[] sloj1 = new double[length_full_array];
            double[] sloj2;
            double[] sloj3;

            Converter_string_double converter_String_Double = new Converter_string_double(text1);
            converter_String_Double.Convert_string_double();
            double[] data_in = converter_String_Double.Get_cript_message();//Входные данные
            double[] data_out = new double[length_full_array];

            //////////////////////////////////////////
            ///Создаем слои и считываем веса
            layer_neural_network.Layer_Perzertron Layer_1 = new layer_neural_network.Layer_Perzertron(length_full_array, length_full_array);

            Layer_1.Read_from_file_bias_1("Perzeptron_data\\bias0.txt");
            Layer_1.Read_from_file_weight_1("Perzeptron_data\\kernel0.txt");

            layer_neural_network.Layer_Perzeptron_Softmax Layer_2 = new layer_neural_network.Layer_Perzeptron_Softmax(length_full_array, length_full_array);

            Layer_2.Read_from_file_bias_1("Perzeptron_data\\bias1.txt");
            Layer_2.Read_from_file_weight_1("Perzeptron_data\\kernel1.txt");
         
            ////////////////////////////////////////////////////////////////

                //Получем первый слой
                sloj2 = Layer_1.Perzertron_forward(sloj1); 

                //Получаем второй слой
                sloj3 = Layer_2.Perzertron_forward(sloj2);
                data_out = sloj3;

        //Пересчитываем результат для вывода на экран и выводим

        Converter_double_result converter_Double_Result = new Converter_double_result(sloj3);
            converter_Double_Result.Convertation();
            int[] result_2 = converter_Double_Result.Get_result();
            char[] result_2_char = converter_int_char(result_2);

            string text3 = new string(result_2_char);

            textBox4.Text = text3;

            for (int i = 0; i < result_2.Length; i++)
            {
                richTextBox1.AppendText(i + "\t" + Math.Round(data_out[i], 3) + "\t" + result_2[i] + "\n");
            }

            //Считаем ошибку          
            double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);

            for (int i = 0; i < Mse1.Length; i++)
            {
                richTextBox2.AppendText(i + "\t" + Math.Round(Mse1[i], 5) + "\n");  
            }

            richTextBox2.AppendText("sum" + "\t" + Math.Round(Mse1.Sum(), 5) + "\n");

            richTextBox2.AppendText("\t" + Math.Round(Error_Func.MSE_ob(Mse1), 5) + "\n");

        }

        /// <summary>
        /// Считать перцептрон валидационный файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)//
        {
            label12.Text = "";

            int N_valid = Convert.ToInt32(Convert.ToDouble(textBox7.Text) * 0.1);
         //   N_valid = 10;

            File_Reader reader = new File_Reader();
            string[] valid_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data.txt", N_valid + 1);
            string[] valid_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data_result.txt", N_valid + 1);

            int length_full_array = Convert.ToInt32(textBox3.Text);// -length_block_in_data;
                      
           
            double[] sloj2;
           
            //////////////////////////////////////////
            ///Создаем слои и считываем веса
            layer_neural_network.Layer_Perzertron Layer_1 = new layer_neural_network.Layer_Perzertron(length_full_array, length_full_array);

            Layer_1.Read_from_file_bias_1("Perzeptron_data\\bias0.txt");
            Layer_1.Read_from_file_weight_1("Perzeptron_data\\kernel0.txt");

            layer_neural_network.Layer_Perzeptron_Softmax Layer_2 = new layer_neural_network.Layer_Perzeptron_Softmax(length_full_array, length_full_array);

            Layer_2.Read_from_file_bias_1("Perzeptron_data\\bias1.txt");
            Layer_2.Read_from_file_weight_1("Perzeptron_data\\kernel1.txt");

            ////////////////////////////////////////////////////////////////

            double sum_MSE = 0;

            for (int k = 0; k < N_valid; k++)
            {
                string text1 = valid_data[k];// исходные входные данные
                string text2 = valid_data_result[k];// исходные результаты

                char[] text2_char = text2.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];

                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }                

                Converter_string_double converter_String_Double = new Converter_string_double(text1);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                double[] data_out = new double[length_full_array];   

                //Получем первый слой
                sloj2 = Layer_1.Perzertron_forward(data_in);

                //Получаем второй слой
                data_out = Layer_2.Perzertron_forward(sloj2);


                //Пересчитываем результат для вывода на экран и выводим

                Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
                converter_Double_Result.Convertation();
                int[] result_2 = converter_Double_Result.Get_result();
                char[] result_2_char = converter_int_char(result_2);

                string text3 = new string(result_2_char);
                textBox4.Text = text3;

                //Считаем ошибку

                double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);
                double s = Error_Func.MSE_ob(Mse1);

                sum_MSE = sum_MSE + s;

                richTextBox2.AppendText("\t" + Math.Round(s, 5) + "\n");
            }
           

            richTextBox2.AppendText("\n"+"Ошибка на число примеров"+ "\t" + Math.Round((sum_MSE / N_valid), 5) + "\n");
        }

        /// <summary>
        /// Считать RNN 1 строка вперед
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_elem = Convert.ToInt32(this.textBox3.Text);

            Generation_string generator1 = new Generation_string(N_elem);
            generator1.Generation_Message();
            string text1 = generator1.Get_message();
            textBox2.Text = text1;

            Generation_Test generator2 = new Generation_Test(text1);
            generator2.Made_Test_String_Out();
            string text2 = generator2.Get_Test_String_Out();

            textBox1.Text = text2;

            ////////////////////////////////////////////

            char[] text2_char = text2.ToCharArray();
            int[] text2_int = new int[text2_char.Length];
            double[] text2_double = new double[text2_char.Length];

            for (int i = 0; i < text2_char.Length; i++)
            {
                text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                text2_double[i] = Convert.ToDouble(text2_int[i]);
            }

            ////////////
            int length_full_array = Convert.ToInt32(textBox3.Text);// - Полная длина последовательности
            int RNN_length = Convert.ToInt32(textBox8.Text);// Длина RNN последовательности - 5
            int output_Length = Convert.ToInt32(textBox11.Text);//Длина выходного вектора - 1
            int input_Length = Convert.ToInt32(textBox12.Text);//Длина выходного вектора - 1

            double[] sloj1 = new double[length_full_array];
         //   double[] sloj2;
        //   double[] sloj3;

            Converter_string_double converter_String_Double = new Converter_string_double(text1);
            converter_String_Double.Convert_string_double();
            double[] data_in = converter_String_Double.Get_cript_message();//Входные данные
                 
            //Обезразмериваем входные данные
            for (int i = 0; i < data_in.Length; i++)
            {
                data_in[i] = data_in[i] / 16.0;
            }

            double[] data_out = new double[length_full_array];

            //////////////////////////////////////////
            ///Создаем слои и считываем веса
          
            layer_neural_network.Layer_RNN Layer = new layer_neural_network.Layer_RNN(data_in, 
                length_full_array, RNN_length, input_Length, output_Length);
            Layer.Inizialize_RNN_Cell();
            Layer.Build_RNN_Layer("Rekkurent_net\\rnn5-1\\bias0.txt",
                "Rekkurent_net\\rnn5-1\\kernel0.txt", 
                "Rekkurent_net\\rnn5-1\\recurrent_kernel0.txt");
            Layer.Calculate_RNN_Layer();
            
            //Получаем второй слой
            data_out = Layer.Get_Y();
          
            //Пересчитываем результат для вывода на экран и выводим

            Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
            converter_Double_Result.Convertation();
            int[] result_2 = converter_Double_Result.Get_result();
            char[] result_2_char = converter_int_char(result_2);

            string text3 = new string(result_2_char);

            textBox4.Text = text3;

            for (int i = 0; i < result_2.Length; i++)
            {
                richTextBox1.AppendText(i + "\t" + Math.Round(data_out[i], 3) + "\t" + result_2[i] + "\n");
            }

            //Считаем ошибку          
            double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);

            for (int i = 0; i < Mse1.Length; i++)
            {
                richTextBox2.AppendText(i + "\t" + Math.Round(Mse1[i], 5) + "\n");
            }

            richTextBox2.AppendText("sum" + "\t" + Math.Round(Mse1.Sum(), 5) + "\n");

            richTextBox2.AppendText("\t" + Math.Round(Error_Func.MSE_ob(Mse1), 5) + "\n");



            label12.Text = "Готово";
        }

        /// <summary>
        /// Считать RNN Валидационный вперед
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            richTextBox1.Clear();
            richTextBox2.Clear();

            int N_valid = Convert.ToInt32(Convert.ToDouble(textBox7.Text) * 0.1);
            int length_full_array = Convert.ToInt32(textBox3.Text);// - Полная длина последовательности
            int RNN_length = Convert.ToInt32(textBox8.Text);// Длина RNN последовательности - 5
            int output_Length = Convert.ToInt32(textBox11.Text);//Длина выходного вектора - 1
            int input_Length = Convert.ToInt32(textBox12.Text);//Длина выходного вектора - 1


            File_Reader reader = new File_Reader();
            string[] valid_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data.txt", N_valid + 1);
            string[] valid_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data\\Valid_data_result.txt", N_valid + 1);

            double[] data_in = new double[length_full_array];


            //////////////////////////////////////////
            ///Создаем слои и считываем веса
            layer_neural_network.Layer_RNN Layer = new layer_neural_network.Layer_RNN(data_in,
                 length_full_array, RNN_length, input_Length, output_Length);
            Layer.Inizialize_RNN_Cell();
            Layer.Build_RNN_Layer("Rekkurent_net\\rnn5-1\\bias0.txt",
                "Rekkurent_net\\rnn5-1\\kernel0.txt",
                "Rekkurent_net\\rnn5-1\\recurrent_kernel0.txt");

            ////////////////////////////////////////////////////////////////

            double sum_MSE = 0;

            for (int k = 0; k < N_valid; k++)
            {
                string text1 = valid_data[k];// исходные входные данные
                string text2 = valid_data_result[k];// исходные результаты

                char[] text2_char = text2.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];

                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text1);
                converter_String_Double.Convert_string_double();
                data_in = converter_String_Double.Get_cript_message();//Входные данные

                Layer.Set_X(data_in);
              
                //Получем первый слой
                Layer.Calculate_RNN_Layer();
                               
                double[] data_out = data_out = Layer.Get_Y();


                //Пересчитываем результат для вывода на экран и выводим

                Converter_double_result converter_Double_Result = new Converter_double_result(data_out);
                converter_Double_Result.Convertation();
                int[] result_2 = converter_Double_Result.Get_result();
                char[] result_2_char = converter_int_char(result_2);

                string text3 = new string(result_2_char);
               
                //Считаем ошибку

                double[] Mse1 = Error_Func.MSE_calculate_and_return(text2_double, data_out);
                double s = Error_Func.MSE_ob(Mse1);

                sum_MSE = sum_MSE + s;

                richTextBox2.AppendText("\t" + Math.Round(s, 5) + "\n");
            }


            richTextBox2.AppendText("\n" + "Ошибка на число примеров" + "\t" + Math.Round((sum_MSE / N_valid), 5) + "\n");

            label12.Text = "Готово";
        }

        /// <summary>
        /// Создать обучающие примеры для рекурентной сети
        /// Для этого генерируем full_N_data примеров, разрезаем их на участки длительностью
        /// N_RNN и заисываем результат в папку samples_data_RNN.
        /// Затем переконвертруем получившиеся строки в массивы кодов типа double и записываем
        /// их в samples_data_double_RNN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            int N_elem1 = Convert.ToInt32(this.textBox3.Text);
            int N_RNN = Convert.ToInt32(this.textBox8.Text);
            int output = Convert.ToInt32(this.textBox11.Text);
            int N_elem = N_RNN;

            double result = (Convert.ToDouble(N_elem1)/ Convert.ToDouble(output))-Convert.ToDouble(N_RNN);

        
            double full_N_data = Convert.ToDouble(this.textBox7.Text);

            int learn_N_data = Convert.ToInt32(full_N_data * 0.7);
            int test_N_data = Convert.ToInt32(full_N_data * 0.2);
            int valid_N_data = Convert.ToInt32(full_N_data * 0.1);

            int learn_N_data_RNN = learn_N_data * Convert.ToInt32(result);
            int test_N_data_RNN = test_N_data * Convert.ToInt32(result);
            int valid_N_data_RNN = valid_N_data * Convert.ToInt32(result);


            double[,] learn_data_double = new double[learn_N_data_RNN, N_elem];
            double[,] learn_data_result_double = new double[learn_N_data_RNN, output];

            double[,] test_data_double = new double[test_N_data_RNN, N_elem];
            double[,] test_data_result_double = new double[test_N_data_RNN, output];

            double[,] valid_data_double = new double[valid_N_data_RNN, N_elem];
            double[,] valid_data_result_double = new double[valid_N_data_RNN, output];

            Generation_string generator1 = new Generation_string(N_elem1);
            Generation_Test generator2 = new Generation_Test();

            StreamWriter rw1 = new StreamWriter("Samples_from_education\\samples_data_RNN\\Learn_data.txt");
            StreamWriter r2w1 = new StreamWriter("Samples_from_education\\samples_data_RNN\\Learn_data_result.txt");


            for (int i = 0; i < learn_N_data; i++)
            {
                generator1.Generation_Message();
                string text1 = generator1.Get_message();

                for (int j = 0; j < text1.Length-N_RNN; j++)
                {
                    string text1_2 = text1.Substring(j, N_RNN);
                    rw1.WriteLine(text1_2);

                    generator2.Set_Test_String_In(text1_2);
                    generator2.Made_Test_One_Element();
                    char a = generator2.Get_Test_String_One_Element();
                    r2w1.WriteLine(a);

                }   
            }
            rw1.Close();
            r2w1.Close();

            ///////////////////////////////////////////////////////

            File_Reader reader = new File_Reader();
            string[] learn_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Learn_data.txt", learn_N_data_RNN + 1);
            string[] learn_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Learn_data_result.txt", learn_N_data_RNN + 1);

            for (int k = 0; k < learn_N_data_RNN; k++)
            {
                string text11 = learn_data[k];// исходные входные данные
                string text22 = learn_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    learn_data_double[k, i] = data_in[i];
                }
                for (int i = 0; i < output; i++)
                {                
                    learn_data_result_double[k, i] = text2_double[i];
                }

            }


            Writer_weight writer_Weight1 = new Writer_weight("Samples_from_education\\samples_data_double_RNN\\Learn_data_double.txt");
            writer_Weight1.Set_in_file_weight_1(learn_N_data_RNN, N_elem, learn_data_double);

            Writer_weight writer_Weight2 = new Writer_weight("Samples_from_education\\samples_data_double_RNN\\Learn_data_result_double.txt");
            writer_Weight2.Set_in_file_weight_1(learn_N_data_RNN, output, learn_data_result_double);

            ///////////////////
            StreamWriter rw2 = new StreamWriter("Samples_from_education\\samples_data_RNN\\Test_data.txt");
            StreamWriter r2w2 = new StreamWriter("Samples_from_education\\samples_data_RNN\\Test_data_result.txt");


            for (int i = 0; i < test_N_data; i++)
            {
                generator1.Generation_Message();
                string text1 = generator1.Get_message();

                for (int j = 0; j < text1.Length - N_RNN; j++)
                {
                    string text1_2 = text1.Substring(j, N_RNN);
                    rw2.WriteLine(text1_2);

                    generator2.Set_Test_String_In(text1_2);
                    generator2.Made_Test_One_Element();
                    char a = generator2.Get_Test_String_One_Element();
                    r2w2.WriteLine(a);
                }
            }
            rw2.Close();
            r2w2.Close();

            ///////////////////////////////////////////////////////

            File_Reader reader2 = new File_Reader();
            string[] test_data = reader2.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Test_data.txt", test_N_data_RNN + 1);
            string[] test_data_result = reader2.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Test_data_result.txt", test_N_data_RNN + 1);

            for (int k = 0; k < test_N_data_RNN; k++)
            {
                string text11 = test_data[k];// исходные входные данные
                string text22 = test_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    test_data_double[k, i] = data_in[i];
                }
                for (int i = 0; i < output; i++)
                {
                    test_data_result_double[k, i] = text2_double[i];
                }

            }


            Writer_weight writer_Weight3 = new Writer_weight("Samples_from_education\\samples_data_double_RNN\\Test_data_double.txt");
            writer_Weight3.Set_in_file_weight_1(test_N_data_RNN, N_elem, test_data_double);

            Writer_weight writer_Weight4 = new Writer_weight("Samples_from_education\\samples_data_double_RNN\\Test_data_result_double.txt");
            writer_Weight4.Set_in_file_weight_1(test_N_data_RNN, output, test_data_result_double);
            //////////////////////////////////////////////////////////////////////////
            File_Reader reader3 = new File_Reader();
            StreamWriter rw3 = new StreamWriter("Samples_from_education\\samples_data_RNN\\Valid_data.txt");
            StreamWriter r2w3 = new StreamWriter("Samples_from_education\\samples_data_RNN\\Valid_data_result.txt");


            for (int i = 0; i < valid_N_data; i++)
            {
                generator1.Generation_Message();
                string text1 = generator1.Get_message();

                for (int j = 0; j < text1.Length - N_RNN; j++)
                {
                    string text1_2 = text1.Substring(j, N_RNN);
                    rw3.WriteLine(text1_2);

                    generator2.Set_Test_String_In(text1_2);
                    generator2.Made_Test_One_Element();
                    char a = generator2.Get_Test_String_One_Element();
                    r2w3.WriteLine(a);

                }
            }
            rw3.Close();
            r2w3.Close();

            ///////////////////////////////////////////////////////


            string[] valid_data = reader3.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Valid_data.txt", valid_N_data_RNN + 1);
            string[] valid_data_result = reader3.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Valid_data_result.txt", valid_N_data_RNN + 1);

            for (int k = 0; k < valid_N_data_RNN; k++)
            {
                string text11 = valid_data[k];// исходные входные данные
                string text22 = valid_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    valid_data_double[k, i] = data_in[i];
                }
                for (int i = 0; i < output; i++)
                {
                    valid_data_result_double[k, i] = text2_double[i];
                }

            }

            Writer_weight writer_Weight5 = new Writer_weight("Samples_from_education\\samples_data_double_RNN\\Valid_data_double.txt");
            writer_Weight5.Set_in_file_weight_1(valid_N_data_RNN, N_elem, valid_data_double);

            Writer_weight writer_Weight6 = new Writer_weight("Samples_from_education\\samples_data_double_RNN\\Valid_data_result_double.txt");
            writer_Weight6.Set_in_file_weight_1(valid_N_data_RNN, output, valid_data_result_double);

            label12.Text = "Готово";
        }

        /// <summary>
        /// Очистить richTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            label12.Text = "";

            richTextBox1.Clear();
            richTextBox2.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
        }
        /// <summary>
        /// Перемешать примеры для реккурентой сети
        /// 
        /// Для этого считываем full_N_data примеров, из папки samples_data_RNN.
        /// Затем переконвертруем получившиеся строки в массивы кодов типа double и 
        /// перебираем их так, чтобы положительные и отрицательные ответы чередовались,
        /// и затем записываем их в папку samples_data_double_RNN_equal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            int N_elem1 = Convert.ToInt32(this.textBox3.Text);
            int N_RNN = Convert.ToInt32(this.textBox8.Text);
            int output = Convert.ToInt32(this.textBox11.Text);
            int N_elem = N_RNN;

            double result = (Convert.ToDouble(N_elem1) / Convert.ToDouble(output)) - Convert.ToDouble(N_RNN);


            double full_N_data = Convert.ToDouble(this.textBox7.Text);

            int learn_N_data = Convert.ToInt32(full_N_data * 0.7);
            int test_N_data = Convert.ToInt32(full_N_data * 0.2);
            int valid_N_data = Convert.ToInt32(full_N_data * 0.1);

            int learn_N_data_RNN = learn_N_data * Convert.ToInt32(result);
            int test_N_data_RNN = test_N_data * Convert.ToInt32(result);
            int valid_N_data_RNN = valid_N_data * Convert.ToInt32(result);


            double[,] learn_data_double = new double[learn_N_data_RNN, N_elem];
            double[,] learn_data_result_double = new double[learn_N_data_RNN, output];
                      
            double[,] test_data_double = new double[test_N_data_RNN, N_elem];
            double[,] test_data_result_double = new double[test_N_data_RNN, output];

            double[,] valid_data_double = new double[valid_N_data_RNN, N_elem];
            double[,] valid_data_result_double = new double[valid_N_data_RNN, output];

            Generation_string generator1 = new Generation_string(N_elem1);
            Generation_Test generator2 = new Generation_Test();


            File_Reader reader = new File_Reader();
            string[] learn_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Learn_data.txt", learn_N_data_RNN + 1);
            string[] learn_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Learn_data_result.txt", learn_N_data_RNN + 1);

            int N_one = 0;
            for (int k = 0; k < learn_N_data_RNN; k++)
            {
                

                string text11 = learn_data[k];// исходные входные данные
                string text22 = learn_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    learn_data_double[k, i] = data_in[i];                   

                }
                for (int i = 0; i < output; i++)
                {
                    learn_data_result_double[k, i] = text2_double[i];

                    if (text2_double[i]==1)
                    {
                        N_one++;
                    }
                }
            }
            double[,] learn_data_double_sum = new double[N_one+N_one, N_elem];
            double[,] learn_data_result_double_sum = new double[N_one+N_one, output];

            int s = 0;
            for (int k = 0; k < learn_N_data_RNN; k++)
            {
                bool a = false;
                for (int i = 0; i < output; i++)
                {                   
                    if (learn_data_result_double[k, i] == 1.0)
                    {
                        a= true;
                        break;
                    }
                }

                if (a)
                {
                    for (int i = 0; i < N_elem; i++)
                    {
                        learn_data_double_sum[s, i] = learn_data_double[k, i];
                    }
                    for (int i = 0; i < output; i++)
                    {
                        learn_data_result_double_sum[s, i] = learn_data_result_double[k, i];
                    }

                    s++;
                }
            }

            for (int k = 0; k < learn_N_data_RNN; k++)
            {
                bool a = false;
                for (int i = 0; i < output; i++)
                {
                    if (learn_data_result_double[k, i] == 0.0)
                    {
                        a = true;
                        break;
                    }
                }

                if (a)
                {
                    for (int i = 0; i < N_elem; i++)
                    {
                        learn_data_double_sum[s, i] = learn_data_double[k, i];
                    }
                    for (int i = 0; i < output; i++)
                    {
                        learn_data_result_double_sum[s, i] = learn_data_result_double[k, i];
                    }

                    s++;
                    if (s>=(N_one+N_one))
                    {
                        break;
                    }
                }
            }
            double[] shifter1 = new double[N_elem];
            double[] shifter2 = new double[output];
            for (int i = 1; i < (s/2-1); i+=2)
            {
                for (int j = 0; j < N_elem; j++)
                {
                    shifter1[j] = learn_data_double_sum[i, j];
                    learn_data_double_sum[i, j] = learn_data_double_sum[s-i, j];
                    learn_data_double_sum[s - i, j] = shifter1[j];
                }
                for (int j = 0; j < output; j++)
                {
                    shifter2[j] = learn_data_result_double_sum[i, j];
                    learn_data_result_double_sum[i, j] = learn_data_result_double_sum[s - i, j];
                    learn_data_result_double_sum[s - i, j] = shifter2[j];
                }
            }

            s = 0;

            Writer_weight writer_Weight1 = new Writer_weight("Samples_from_education\\samples_data_double_RNN_equal\\Learn_data_double.txt");
            writer_Weight1.Set_in_file_weight_1((N_one + N_one), N_elem, learn_data_double_sum);

            Writer_weight writer_Weight2 = new Writer_weight("Samples_from_education\\samples_data_double_RNN_equal\\Learn_data_result_double.txt");
            writer_Weight2.Set_in_file_weight_1((N_one + N_one), output, learn_data_result_double_sum);

            label12.Text = $"Всего {N_one + N_one} элементов";
            //////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            string[] test_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Test_data.txt", test_N_data_RNN + 1);
            string[] test_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Test_data_result.txt", test_N_data_RNN + 1);

            N_one = 0;
            for (int k = 0; k < test_N_data_RNN; k++)
            {
                string text11 = test_data[k];// исходные входные данные
                string text22 = test_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    test_data_double[k, i] = data_in[i];

                }
                for (int i = 0; i < output; i++)
                {
                    test_data_result_double[k, i] = text2_double[i];

                    if (text2_double[i] == 1)
                    {
                        N_one++;
                    }
                }
            }
            double[,] test_data_double_sum = new double[N_one + N_one, N_elem];
            double[,] test_data_result_double_sum = new double[N_one + N_one, output];

            s = 0;
            for (int k = 0; k < test_N_data_RNN; k++)
            {
                bool a = false;
                for (int i = 0; i < output; i++)
                {
                    if (test_data_result_double[k, i] == 1.0)
                    {
                        a = true;
                        break;
                    }
                }

                if (a)
                {
                    for (int i = 0; i < N_elem; i++)
                    {
                        test_data_double_sum[s, i] = test_data_double[k, i];
                    }
                    for (int i = 0; i < output; i++)
                    {
                        test_data_result_double_sum[s, i] = test_data_result_double[k, i];
                    }

                    s++;
                }
            }

            for (int k = 0; k < test_N_data_RNN; k++)
            {
                bool a = false;
                for (int i = 0; i < output; i++)
                {
                    if (test_data_result_double[k, i] == 0.0)
                    {
                        a = true;
                        break;
                    }
                }

                if (a)
                {
                    for (int i = 0; i < N_elem; i++)
                    {
                        test_data_double_sum[s, i] = test_data_double[k, i];
                    }
                    for (int i = 0; i < output; i++)
                    {
                        test_data_result_double_sum[s, i] = test_data_result_double[k, i];
                    }

                    s++;
                    if (s >= (N_one + N_one))
                    {
                        break;
                    }
                }
            }


            for (int i = 1; i < (s / 2 - 1); i += 2)
            {
                for (int j = 0; j < N_elem; j++)
                {
                    shifter1[j] = test_data_double_sum[i, j];
                    test_data_double_sum[i, j] = test_data_double_sum[s - i, j];
                    test_data_double_sum[s - i, j] = shifter1[j];
                }
                for (int j = 0; j < output; j++)
                {
                    shifter2[j] = test_data_result_double_sum[i, j];
                    test_data_result_double_sum[i, j] = test_data_result_double_sum[s - i, j];
                    test_data_result_double_sum[s - i, j] = shifter2[j];
                }
            }
            s = 0;

            Writer_weight writer_Weight3 = new Writer_weight("Samples_from_education\\samples_data_double_RNN_equal\\Test_data_double.txt");
            writer_Weight3.Set_in_file_weight_1((N_one + N_one), N_elem, test_data_double_sum);

            Writer_weight writer_Weight4 = new Writer_weight("Samples_from_education\\samples_data_double_RNN_equal\\Test_data_result_double.txt");
            writer_Weight4.Set_in_file_weight_1((N_one + N_one), output, test_data_result_double_sum);

            //////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            string[] valid_data = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Valid_data.txt", valid_N_data_RNN + 1);
            string[] valid_data_result = reader.Read_file_line_by_line("Samples_from_education\\samples_data_RNN\\Valid_data_result.txt", valid_N_data_RNN + 1);

            N_one = 0;
            for (int k = 0; k < valid_N_data_RNN; k++)
            {
                string text11 = valid_data[k];// исходные входные данные
                string text22 = valid_data_result[k];// исходные результаты

                char[] text2_char = text22.ToCharArray();
                int[] text2_int = new int[text2_char.Length];
                double[] text2_double = new double[text2_char.Length];


                for (int i = 0; i < text2_char.Length; i++)
                {
                    text2_int[i] = (int)Char.GetNumericValue(text2_char[i]);
                    text2_double[i] = Convert.ToDouble(text2_int[i]);
                }

                Converter_string_double converter_String_Double = new Converter_string_double(text11);
                converter_String_Double.Convert_string_double();
                double[] data_in = converter_String_Double.Get_cript_message();//Входные данные

                for (int i = 0; i < N_elem; i++)
                {
                    valid_data_double[k, i] = data_in[i];

                }
                for (int i = 0; i < output; i++)
                {
                    valid_data_result_double[k, i] = text2_double[i];

                    if (text2_double[i] == 1)
                    {
                        N_one++;
                    }
                }
            }
            double[,] valid_data_double_sum = new double[N_one + N_one, N_elem];
            double[,] valid_data_result_double_sum = new double[N_one + N_one, output];

            s = 0;
            for (int k = 0; k < valid_N_data_RNN; k++)
            {
                bool a = false;
                for (int i = 0; i < output; i++)
                {
                    if (valid_data_result_double[k, i] == 1.0)
                    {
                        a = true;
                        break;
                    }
                }

                if (a)
                {
                    for (int i = 0; i < N_elem; i++)
                    {
                        valid_data_double_sum[s, i] = valid_data_double[k, i];
                    }
                    for (int i = 0; i < output; i++)
                    {
                        valid_data_result_double_sum[s, i] = valid_data_result_double[k, i];
                    }

                    s++;
                }
            }

            for (int k = 0; k < valid_N_data_RNN; k++)
            {
                bool a = false;
                for (int i = 0; i < output; i++)
                {
                    if (valid_data_result_double[k, i] == 0.0)
                    {
                        a = true;
                        break;
                    }
                }

                if (a)
                {
                    for (int i = 0; i < N_elem; i++)
                    {
                        valid_data_double_sum[s, i] = valid_data_double[k, i];
                    }
                    for (int i = 0; i < output; i++)
                    {
                        valid_data_result_double_sum[s, i] = valid_data_result_double[k, i];
                    }

                    s++;
                    if (s >= (N_one + N_one))
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i < (s / 2 - 1); i += 2)
            {
                for (int j = 0; j < N_elem; j++)
                {
                    shifter1[j] = valid_data_double_sum[i, j];
                    valid_data_double_sum[i, j] = valid_data_double_sum[s - i, j];
                    valid_data_double_sum[s - i, j] = shifter1[j];
                }
                for (int j = 0; j < output; j++)
                {
                    shifter2[j] = valid_data_result_double_sum[i, j];
                    valid_data_result_double_sum[i, j] = valid_data_result_double_sum[s - i, j];
                    valid_data_result_double_sum[s - i, j] = shifter2[j];
                }
            }

            s = 0;

            Writer_weight writer_Weight5 = new Writer_weight("Samples_from_education\\samples_data_double_RNN_equal\\Valid_data_double.txt");
            writer_Weight5.Set_in_file_weight_1((N_one + N_one), N_elem, valid_data_double_sum);

            Writer_weight writer_Weight6 = new Writer_weight("Samples_from_education\\samples_data_double_RNN_equal\\Valid_data_result_double.txt");
            writer_Weight6.Set_in_file_weight_1((N_one + N_one), output, valid_data_result_double_sum);

            label13.Text = "Готово.";
        }
    }
}
