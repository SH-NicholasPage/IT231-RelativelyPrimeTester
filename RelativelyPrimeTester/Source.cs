/*
* Name: [YOUR NAME HERE]
* South Hills Username: [YOUR SOUTH HILLS USERNAME HERE]
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RelativelyPrimeTester.Program;

namespace RelativelyPrimeTester;

// If using Linux, navigate to RealtvielyPrime/Linux and give the exe file execute permission.
//    If you've already built, do a clean build.

public class Source
{
    public static void Tester()
    {
        // TODO: Edit the code here to test the Relatively Prime program for errors.
        //  There are six different errors you can find, but you only need to find five.

        Result example1 = Process.Run(24, 32);
        Result example2 = Process.Run("24 32");//In this format, numbers must be space separated.

        // Hint: Use breakpoints to check the values of variables during runtime and keep checking the console.
    }

    public static void ExhaustiveTest()
    {
        int logicErrors = 0; // Do not remove

        // TODO: Write your exhaustive test here.
        // Don't worry about timing it, there are functions in place that will time it for you.

        Console.WriteLine("Logic errors found: " + logicErrors); // Do not remove
    }
}

