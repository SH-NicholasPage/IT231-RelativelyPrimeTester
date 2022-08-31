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
            ProcessResult pr1 = Utils.RunProcess(0, 2);
            ProcessResult pr2 = Utils.RunProcess("1");
            ProcessResult pr3 = Utils.RunProcess("1 s");
            ProcessResult pr4 = Utils.RunProcess("s");
            ProcessResult pr5 = Utils.RunProcess("");
            ProcessResult pr6 = Utils.RunProcess("12 12");
            ProcessResult pr7 = Utils.RunProcess("12 12 5");
            ProcessResult pr8 = Utils.RunProcess("99999999999999999999999999 12");

            int t = 0;
        }

        public static void ExhaustiveTest()
        {
            
        }
    }
}
