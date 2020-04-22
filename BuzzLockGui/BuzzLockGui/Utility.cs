using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzLockGui
{
    class Utility
    {
        public static string Pluralize(int number, string unit)
        {
            if (number == 1)
            {
                return number + " " + unit;
            }
            return number + " " + unit + "s";
        }
    }
}
