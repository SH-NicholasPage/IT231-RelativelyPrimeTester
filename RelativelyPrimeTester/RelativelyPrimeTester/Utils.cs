/*
 * You can add other methods to this file if you would like, but do not modify existing methods.
 */

using RelativelyPrime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static RelativelyPrimeTester.Program;

namespace RelativelyPrimeTester
{
    public class Utils
    {
        /// <summary>
        /// Accurately determines what is coprime and returns true if coprime, false if not coprime.
        /// </summary>
        public static bool IsCoPrime(int val1, int val2)
        {
            //Can't be coprime if one is positive and one is negative
            if ((val1 < 0 && val2 > 0) || val2 > 0 && val1 < 0)
            {
                return false;
            }

            val1 = Math.Abs(val1);
            val2 = Math.Abs(val2);

            return (GCD(val1, val2) == 1) ? true : false;
        }

        //Recursive
        private static int GCD(int a, int b)
        {
            //Everything divides 0
            if (a == 0 || b == 0)
            {
                return 0;
            }

            //Base case
            if (a == b)
            {
                return a;
            }

            //A is greater
            if (a > b)
            {
                return GCD(a - b, b);
            }

            return GCD(a, b - a);
        }
    }
}
