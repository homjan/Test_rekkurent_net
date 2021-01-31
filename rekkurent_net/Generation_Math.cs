using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rekkurent_net
{
    class Generation_Math
    {       
        public static Random rng = new Random();

       internal Generation_Math()
        {             
        }
        /// <summary>
        /// Сгенерировать число от 0 до 9 (включительно)
        /// </summary>
        /// <returns></returns>
        internal static int GenerateDigit_10()
        {            
            lock (rng)
            {
                return rng.Next(10);
            }
        }
        /// <summary>
        /// Сгенерировать число от 0 до 99 (включительно)
        /// </summary>
        /// <returns></returns>
        internal static int GenerateDigit_100()
        {            
            lock (rng)
            {
                return rng.Next(100);
            }
        }

        /// <summary>
        /// Сгенерировать число от 0 до MaxValue
        /// </summary>
        /// <param name="MaxValue"></param>
        /// <returns></returns>
        internal static int GenerateDigitBorder( int MaxValue) 
        {                      
            lock (rng)
            {
                return rng.Next(MaxValue);
            }
        }
    }
}
