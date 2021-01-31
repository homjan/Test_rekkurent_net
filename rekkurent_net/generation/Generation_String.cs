using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Generation_string
    {
        private int N_elements;
        private string message = null;
        private char element_message;
        private char[] message_char;

        private bool test_sigh = false;

        public string Get_message() {
            return message;
        }

        /// <summary>
        /// Конструктор генератора строк выбранной длины
        /// </summary>
        /// <param name="nelements">Длина</param>
       internal Generation_string(int nelements)
        {
            this.N_elements = nelements;
            message_char = new char[N_elements];
        }

        public char Element_message { get => element_message; set => element_message = value; }

        /// <summary>
        /// Сгенерировать строку выбранной длины
        /// </summary>
        public void Generation_Message() {

           
            Generation_Symbol generator = new Generation_Symbol();
            generator.Generation_element_1();
            element_message = generator.get_first_element();
            message_char[0] = element_message;

            int N_brackets = 0;
            int n_not_zero_element = N_elements;

            for (int i = 1; i < N_elements; i++) {

                N_brackets = generator.get_counter_brackets();

                if (element_message == '+' || element_message == '-' ||
                    element_message == '*' || element_message == '/' || element_message == '^')
                {
                    generator.Generation_Numeral_After_Sigh();
                    test_sigh = true;

                }
                else if (element_message == '(')
                {
                    generator.Generation_Numeral_After_Open_Bracket();
                    test_sigh = false;

                }
                else if (element_message == ')')
                {
                    generator.Generation_Numeral_After_Close_Bracket();
                }
                else if ((element_message == '0' || element_message == '1' ||
                  element_message == '2' || element_message == '3' || element_message == '4' ||
                  element_message == '5' || element_message == '6' ||
                  element_message == '7' || element_message == '8' || element_message == '9') && N_brackets > 0)
                {
                    if (test_sigh)
                    {
                        generator.Generation_Numeral_After_Numeral_Bracket();
                    }
                    else {
                        generator.Generation_Numeral_After_Numeral_No_Bracket();
                    }


                }
                else if ((element_message == '0' || element_message == '1' ||
                 element_message == '2' || element_message == '3' || element_message == '4' ||
                 element_message == '5' || element_message == '6' ||
                 element_message == '7' || element_message == '8' || element_message == '9') && N_brackets == 0)
                {

                    generator.Generation_Numeral_After_Numeral_No_Bracket();

                }
                else {
                    continue;
                }


                element_message = generator.get_first_element();
                message_char[i] = element_message;
              
                N_brackets = generator.get_counter_brackets();

                if ((element_message == '+' || element_message == '-' ||
                    element_message == '*' || element_message == '/' || element_message == '^') &&
                    (N_brackets == (N_elements - i+1)))
                {
                    n_not_zero_element = i;
                    break;
                }

                if (N_brackets==(N_elements-i))
                {
                    n_not_zero_element = i;
                    break;
                }

            }

            String end_message = null;

            if (element_message == '+' || element_message == '-' ||
                    element_message == '*' || element_message == '/' || element_message == '^')
            {
               // end_message = "f";
                message_char[N_elements - 1] = '1';
            }

            if (N_brackets > 0) {

                for (int i = 0; i < N_brackets; i++) {
                    end_message  += ")";
                }

            }

            char[] new_massage_name = new char[n_not_zero_element];

            for (int i = 0; i < n_not_zero_element; i++)
            {
                new_massage_name[i] = message_char[i];
            }

            message = new string(new_massage_name) + end_message;//Convert.ToString( message_char);

            if (message.Length!=100)
            {
                Generation_Message();
            }

        }



    }
}
