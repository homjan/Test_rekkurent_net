using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Converter_string_double
    {
        private String message;
        private double[] cript_message;

        public Converter_string_double(String mes) {
            this.message = mes;
            cript_message = new double[message.Length];

        }
        /// <summary>
        /// Перевести строку в массив символов
        /// </summary>
        /// <returns></returns>
        public char[] Get_convert_string_char() {

            char[] message_char = message.ToCharArray();

            return message_char;

        }
        /// <summary>
        /// Вернуть строку-сообщение
        /// </summary>
        /// <returns></returns>
        public double[] Get_cript_message(){
            return cript_message;
        }
        /// <summary>
        /// Перевести строку в код из чисел(double) 
        /// </summary>
        public void Convert_string_double() {

            int[] cript_message_int = new int[message.Length];

            char[] message_char = message.ToCharArray();


            for (int i = 0; i < message.Length; i++) {

                char element = message_char[i];

                switch (element)
                {
                    case '0':
                        cript_message_int[i] = 0;
                        break;
                    case '1':
                        cript_message_int[i] = 1;
                        break;
                    case '2':
                        cript_message_int[i] = 2;
                        break;
                    case '3':
                        cript_message_int[i] = 3;
                        break;
                    case '4':
                        cript_message_int[i] = 4;
                        break;
                    case '5':
                        cript_message_int[i] = 5; 
                        break;
                    case '6':
                        cript_message_int[i] = 6;
                        break;
                    case '7':
                        cript_message_int[i] = 7;
                        break;
                    case '8':
                        cript_message_int[i] = 8;
                        break;
                    case '9':
                        cript_message_int[i] = 9;
                        break;
                    case '(':
                        cript_message_int[i] = 10;
                        break;
                    case ')':
                        cript_message_int[i] = 11;
                        break;
                    case '/':
                        cript_message_int[i] = 12;
                        break;
                    case '*':
                        cript_message_int[i] = 13;
                        break;
                    case '+':
                        cript_message_int[i] = 14;
                        break;
                    case '-':
                        cript_message_int[i] = 15;
                        break;
                    case '^':
                        cript_message_int[i] = 16;
                        break;
                    default:
                        break;
                }

            }

            for (int i = 0; i < message.Length; i++)
            {
                cript_message[i] = Convert.ToDouble(cript_message_int[i]);              
            }
           
        }      

    }
}
