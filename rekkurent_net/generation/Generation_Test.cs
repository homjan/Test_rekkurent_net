using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net
{    
    class Generation_Test
    {
        private char origin = ')';

        private String test_string_in;
        private String test_string_out;

        private char test_string_one_element;

        public string Get_Test_String_Out() {
            return test_string_out;
        }

        public char Get_Test_String_One_Element() {

            return test_string_one_element;
        }

        public void Set_Test_String_In(string test_in) {

            this.test_string_in = test_in;
        }

        internal Generation_Test(String test1) {

            this.test_string_in = test1;
        }

        public Generation_Test()
        {

        }
        /// <summary>
        /// Сделать и показать расположение выбраного символа
        /// </summary>
        internal void Made_Test_String_Out() {

            char[] test_in = test_string_in.ToCharArray();

            char[] test_out = new char[test_in.Length];

            for (int i = 0; i < test_in.Length; i++) {

                if (test_in[i] == origin)
                {
                    test_out[i] = '1';
                }
                else {
                    test_out[i] = '0';
                }
            }

            string v = new string(test_out);
            test_string_out = v;


        }

        internal void Made_Test_One_Element() 
        {
            char[] test_in = test_string_in.ToCharArray();         

                if (test_in[test_in.Length-1] == origin)
                {
                test_string_one_element = '1';
                }
                else
                {
                test_string_one_element = '0';
                }           
        }

    }
}
