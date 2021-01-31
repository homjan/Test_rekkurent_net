using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Generation_Symbol
    {

        private int element;

        private char first_element;
        private char second_element;

        private Int32 counter_brackets;

        public char get_first_element() {
            return first_element;
        }

        public Int32 get_counter_brackets()
        {
            return counter_brackets;
        }

        internal Generation_Symbol() {   
            counter_brackets = 0;
        }
        /// <summary>
        /// Сгенерировать первый элемент последовательности
        /// </summary>
        internal void Generation_element_1() {

            element = Generation_Math.GenerateDigit_10();

            second_element = first_element;

            switch (element)
            {
                case 1:
                    first_element = '1';
                    break;
                case 2:
                    first_element = '2';
                    break;
                case 3:
                    first_element = '3';
                    break;
                case 4:
                    first_element = '4';
                    break;
                case 5:
                    first_element = '5';
                    break;
                case 6:
                    first_element = '6';
                    break;
                case 7:
                    first_element = '7';
                    break;
                case 8:
                    first_element = '8';
                    break;
                case 9:
                    first_element = '9';
                    break;
                case 0:
                    first_element = '(';
                    counter_brackets++;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Сгенерировать элемент последовательности если нет открытых скобок
        /// </summary>
        internal void Generation_Numeral_After_Numeral_No_Bracket() {
            
            element = Generation_Math.GenerateDigitBorder(15);
            second_element = first_element;

            switch (element)
            {
                case 0:
                    first_element = '0';
                    break;
                case 1:
                    first_element = '1';
                    break;
                case 2:
                    first_element = '2';
                    break;
                case 3:
                    first_element = '3';
                    break;
                case 4:
                    first_element = '4';
                    break;
                case 5:
                    first_element = '5';
                    break;
                case 6:
                    first_element = '6';
                    break;
                case 7:
                    first_element = '7';
                    break;
                case 8:
                    first_element = '8';
                    break;
                case 9:
                    first_element = '9';
                    break;
                case 10:
                    first_element = '+';
                    break;
                case 11:
                    first_element = '-';
                    break;
                case 12:
                    first_element = '*';
                    break;
                case 13:
                    first_element = '/';
                    break;
                case 14:
                    first_element = '^';
                    break;
                default:
                    break;
            }

        }

        internal void Generation_Numeral_After_Numeral_Bracket()
        {

            element = Generation_Math.GenerateDigitBorder(16);
            second_element = first_element;

            switch (element)
            {
                case 0:
                    first_element = '0';
                    break;
                case 1:
                    first_element = '1';
                    break;
                case 2:
                    first_element = '2';
                    break;
                case 3:
                    first_element = '3';
                    break;
                case 4:
                    first_element = '4';
                    break;
                case 5:
                    first_element = '5';
                    break;
                case 6:
                    first_element = '6';
                    break;
                case 7:
                    first_element = '7';
                    break;
                case 8:
                    first_element = '8';
                    break;
                case 9:
                    first_element = '9';
                    break;
                case 10:
                    first_element = '+';
                    break;
                case 11:
                    first_element = '-';
                    break;
                case 12:
                    first_element = '*';
                    break;
                case 13:
                    first_element = '/';
                    break;
                case 14:
                    first_element = '^';
                    break;
                case 15:
                    first_element = ')';
                    counter_brackets--;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Сгенерировать элемент после математического знака
        /// </summary>
        internal void Generation_Numeral_After_Sigh()
        {

            element = Generation_Math.GenerateDigitBorder(10);
            second_element = first_element;

            switch (element)
            {
                case 1:
                    first_element = '1';
                    break;
                case 2:
                    first_element = '2';
                    break;
                case 3:
                    first_element = '3';
                    break;
                case 4:
                    first_element = '4';
                    break;
                case 5:
                    first_element = '5';
                    break;
                case 6:
                    first_element = '6';
                    break;
                case 7:
                    first_element = '7';
                    break;
                case 8:
                    first_element = '8';
                    break;
                case 9:
                    first_element = '9';
                    break;
                case 0:
                    first_element = '(';
                    counter_brackets++;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Сгенерировать элемент после открытой скобки
        /// </summary>
        internal void Generation_Numeral_After_Open_Bracket()
        {

            element = Generation_Math.GenerateDigit_10();
            second_element = first_element;

            switch (element)
            {
                case 1:
                    first_element = '1';
                    break;
                case 2:
                    first_element = '2';
                    break;
                case 3:
                    first_element = '3';
                    break;
                case 4:
                    first_element = '4';
                    break;
                case 5:
                    first_element = '5';
                    break;
                case 6:
                    first_element = '6';
                    break;
                case 7:
                    first_element = '7';
                    break;
                case 8:
                    first_element = '8';
                    break;
                case 9:
                    first_element = '9';
                    break;
                case 0:
                    first_element = '(';
                    counter_brackets++;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Сгенерировать элемент после закрытой скобки
        /// </summary>
        internal void Generation_Numeral_After_Close_Bracket()
        {

            element = Generation_Math.GenerateDigitBorder(6);
            second_element = first_element;

            switch (element)
            {
                case 1:
                    first_element = '+';
                    break;
                case 2:
                    first_element = '-';
                    break;
                case 3:
                    first_element = '*';
                    break;
                case 4:
                    first_element = '/';
                    break;
                case 5:
                    first_element = '^';
                    break;
                case 0:
                    first_element = ')';
                    counter_brackets--;
                    break;
                default:
                    break;
            }

        }


    }
}
