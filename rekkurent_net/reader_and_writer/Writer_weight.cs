using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Writer_weight
    {
        private String name_file;
        /// <summary>
        /// Конструктор для записи 
        /// </summary>
        /// <param name="name_file">Имя файла</param>
        public Writer_weight(string name_file)
        {
            this.name_file = name_file;
        }

        public void Set_name_file(string new_name_file)
        {
            this.name_file = new_name_file;
        }
        /// <summary>
        /// Записать в файл веса weight_1
        /// </summary>
        /// <param name="size_layer_1_in"></param>
        /// <param name="size_data_in"></param>
        /// <param name="weight_1"></param>
        public virtual void Set_in_file_weight_1(int size_layer_1_in, int size_data_in, double[,] weight_1)
        {
            StreamWriter rw = new StreamWriter(name_file);

            for (int i = 0; i < size_layer_1_in; i++)
            {
                for (int j = 0; j < size_data_in; j++)
                {
                    if (j == size_data_in - 1)
                    {
                        rw.Write(weight_1[i, j]);
                    }
                    else
                    {
                        rw.Write(weight_1[i, j] + "\t");
                    }
                }
                rw.WriteLine();
            }
            rw.Close();
        }

        /// <summary>
        /// Записать в файл веса смещения bias_1
        /// </summary>
        /// <param name="size_data_in"></param>
        /// <param name="bias_1"></param>
        public virtual void Set_in_file_bias_1(int size_data_in, double[] bias_1) {

            StreamWriter rw = new StreamWriter(name_file);

            for (int i = 0; i < size_data_in; i++)
            {
                rw.WriteLine(bias_1[i]);
            }
            rw.Close();

        }

    }
}
