using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.reader_and_writer
{
    class File_Reader
    {

        public File_Reader()
        {
            
        }
        /// <summary>
        /// Прочитать файл и записать числа в двумерный массив
        /// </summary>
        /// <param name="name_file"></param>
        /// <param name="number_column"></param>
        /// <param name="N_nejron"></param>
        /// <returns></returns>
        public long[,] Read_file_and_write_massiv(String name_file, int number_column, int N_nejron)
        {

            StringBuilder buffer = new StringBuilder();
            int a = 0;
            int b = 0;//счетчик строк

            string n1;

            int l1;
            int j = 0;// счетчик строк 10
            int k = 0;//счетчик столбцов 2
            int m = 0;//смещение буффера

            long[,] rowx = new long[number_column, N_nejron];

            StreamReader sw = new StreamReader(name_file);

            while (sw.Peek() != -1)
            {
                l1 = sw.Read();

                if (l1 == 13)
                {
                    n1 = buffer.ToString(); // пищем цифру в строку
                    buffer.Remove(0, n1.Length); //очищаем буффер
                                                 // rw2.WriteLine(n1);
                    if (n1 != "")
                    {
                        rowx[j, k] = System.Convert.ToInt64(n1);// пишем в массив
                    }
                    j++; // переходим на следующую строку
                    k = 0; // переходим на первый столбец
                    m = 0;
                    b++;
                }
                if (l1 == 9)
                {
                    n1 = buffer.ToString(); // пищем цифру в строку
                    buffer.Remove(0, n1.Length); //очищаем буффер
                    if (n1 != "" || j < 100)
                    {
                        rowx[j, k] = System.Convert.ToInt64(n1);// пишем в массив
                    }
                    k++; // переходим на следующий столбец
                    m = 0;
                }
                if (l1 == 48 || l1 == 49 || l1 == 50 || l1 == 51 || l1 == 52 || l1 == 53 || l1 == 54 || l1 == 55 || l1 == 56 || l1 == 57)
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
           
            return rowx;
        }

        /// <summary>
        /// Получить одну строку двумерного массива
        /// </summary>
        /// <param name="sloj"></param>
        /// <param name="N_nejron"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public double[] Get_one_line(double[,] sloj, int N_nejron, int x)
        {
            double[] y = new double[N_nejron];

            for (int j = 0; j < N_nejron; j++)
            {
                y[j] = sloj[x, j];
            }

            return y;

        }
        /// <summary>
        /// Прочитать файл и записать его в строковый массив
        /// </summary>
        /// <param name="name_file"></param>
        /// <param name="number_rows">Число элементов массива</param>
        /// <returns></returns>
        public string[] Read_file_line_by_line(String name_file, int number_rows) {

          //   = new string[number_rows];

            List<string> list = new List<string>();

            StreamReader sw = new StreamReader(name_file);
            int i = 0;

            while (true)
            {
                // Читаем строку из файла во временную переменную.
                string temp = sw.ReadLine();
                list.Add(temp);
              //  result[i] = temp;
                // Если достигнут конец файла, прерываем считывание.
                if (temp == null) break;

                i++;
            }

            sw.Close();

            string[] result = list.ToArray();

            return result;
        }


    }
}
