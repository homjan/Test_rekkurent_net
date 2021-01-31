using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Reader_weight
    {
        private String name_file;

        public Reader_weight(string name_file)
        {
            this.name_file = name_file;
        }

        public void Set_name_file( string new_name_file) {
            this.name_file = new_name_file;  
        }
        /// <summary>
        /// Считать из файла веса смещения и записать их в массив bias
        /// </summary>
        /// <returns></returns>
        public double[] Read_in_file_bias_1()
        {
            ArrayList list = new ArrayList();

            StringBuilder buffer = new StringBuilder();
            int a = 0;
            int b = 0;//счетчик строк
          //  double[] bias0;

            string n1;

            int l1;
            int j = 0;// счетчик строк 10 
            int m = 0;//смещение буффера

            StreamReader sw = new StreamReader(name_file);

            while (sw.Peek() != -1)
            {
                l1 = sw.Read();

                if (l1 == 13)
                {
                    buffer.Replace('.', ',');
                    n1 = buffer.ToString(); // пищем цифру в строку

                    buffer.Remove(0, n1.Length); //очищаем буффер

                    if (n1 != "")
                    {
                        list.Add(System.Convert.ToDouble(n1));
                    //    bias0[j] = System.Convert.ToDouble(n1);// пишем в массив
                    }
                    j++; // переходим на следующую строку

                    m = 0;
                    b++;
                }

                if (l1 == 48 || l1 == 49 || l1 == 50 || l1 == 51 || l1 == 52 || l1 == 53 || l1 == 54 || l1 == 55 || l1 == 56 || l1 == 57 || l1 == 46 || l1 == 44)
                {
                    buffer.Insert(m, System.Convert.ToChar(l1)); // пишем символ
                    m++;
                }
                else
                {
                    a++;
                    continue;
                }

            }

            sw.Close();

            double[] bias = (Double[]) list.ToArray(typeof( double));

            return bias;

        }
        /// <summary>
        /// Считать из файла веса и записать их в массив weight
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public double[,] Read_in_file_weight_1(double[,] weight)
        {
            StringBuilder buffer = new StringBuilder();
            int a = 0;
            int b = 0;//счетчик строк

            string n1;

            int l1;
            int j = 0;// счетчик строк 10
            int k = 0;//счетчик столбцов 2
            int m = 0;//смещение буффера

            StreamReader sw = new StreamReader(name_file);

            while (sw.Peek() != -1)
            {
                l1 = sw.Read();


                if (l1 == 13)
                {
                    buffer.Replace('.', ',');
                    n1 = buffer.ToString(); // пищем цифру в строку
                    buffer.Remove(0, n1.Length); //очищаем буффер
                                                 // rw2.WriteLine(n1);
                    if (n1 != "")
                    {
                        weight[j, k] = System.Convert.ToDouble(n1);// пишем в массив
                    }
                    j++; // переходим на следующую строку
                    k = 0; // переходим на первый столбец
                    m = 0;
                    b++;
                }
                if (l1 == 9)
                {
                    buffer.Replace('.', ',');
                    n1 = buffer.ToString(); // пищем цифру в строку
                    buffer.Remove(0, n1.Length); //очищаем буффер
                    if (n1 != "")
                    {
                        weight[j, k] = System.Convert.ToDouble(n1);// пишем в массив
                    }
                    k++; // переходим на следующий столбец
                    m = 0;
                }
                if (l1 == 48 || l1 == 49 || l1 == 50 || l1 == 51 || l1 == 52 || l1 == 53 || l1 == 54 || l1 == 55 || l1 == 56 || l1 == 57 || l1 == 46 || l1 == 48 || l1 == 44)
                {
                    buffer.Insert(m, System.Convert.ToChar(l1)); // пишем символ
                    m++;
                }
                else
                {
                    a++;
                    continue;
                }
            }
            sw.Close();

            return weight;
        }


    }
}
