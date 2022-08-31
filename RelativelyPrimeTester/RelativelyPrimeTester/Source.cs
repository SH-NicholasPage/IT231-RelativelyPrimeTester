/*
* Name: [YOUR NAME HERE]
* South Hills Username: [YOUR SOUTH HILLS USERNAME HERE]
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelativelyPrimeTester.Program;

namespace RelativelyPrimeTester
{
    public class Source
    {
        public static void Tester()
        {
            //TODO: Edit the code here to test the Relatively Prime program for errors. There are a minimum number of errors you need to find.

            ProcessResult example1 = Utils.RunProcess(24, 32);
            ProcessResult example2 = Utils.RunProcess("24 32");//In this format, numbers must be space separated.

            //Hint: Use breakpoints to check the values of variables mid-runtime and keep checking the console.
        }

        public static void ExhaustiveTest()
        {
            //TODO: Write your exhaustive test here. Don't worry about timing it, there are functions in place that will time it for you.
        }
    }
}
