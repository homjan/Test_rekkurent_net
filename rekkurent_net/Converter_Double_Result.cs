using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Converter_double_result
    {
        const double border_zero = 0.8;

        private double[] data;
        private int[] result;

        public int[] Get_result() {

            return result;
        }

        public Converter_double_result(double[] new_data)
        {
            data = new_data;
            result = new int[new_data.Length];
        }
        /// <summary>
        /// Конвертация результатов работы сети (Вероятности нахождения знака в данном состоянии)
        /// в двоичный да-нет сигнал
        /// </summary>
        public void Convertation() {

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > border_zero) {
                    result[i] = 1;
                }
                else
                {
                    result[i] = 0;
                }
            } 
        }
    }
}
