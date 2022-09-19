/*
 * You can add other methods to this file if you would like, but do not modify existing methods.
 */

using System;
using System.Diagnostics;
using static RelativelyPrimeTester.Program;

namespace RelativelyPrimeTester
{
    public class Utils
    {
        //IO is VERY slow, so it's worth muting after enough runs, especially for the exhaustive check
        private static readonly int MUTE_OUTPUT_AFTER_X_RUNS = 20;
        private static int processRan = 0;//Increases every time RunProcess is called

        public static ProcessResult RunProcess()
        {
            return RunProcess(String.Empty, String.Empty);
        }

        public static ProcessResult RunProcess(int? num1 = null, int? num2 = null)
        {
            return RunProcess(num1.ToString(), num2.ToString());
        }

        public static ProcessResult RunProcess(String val)
        {
            if(val.Split().Length >= 2)
            {
                return RunProcess(val.Split()[0].Trim(), val.Split()[1].Trim());
            }
            else
            {
                return RunProcess(val);
            }
        }

        public static ProcessResult RunProcess(String? val1 = null, String? val2 = null)
        {
            int num1 = int.MinValue;
            int num2 = int.MinValue;

            int.TryParse(val1, out num1);
            int.TryParse(val2, out num2);

            if (num1 != int.MinValue && num2 != int.MinValue)
            {
                Program.NumChecked(num1, num2);
            }

            bool mute = false;

            if (processRan > MUTE_OUTPUT_AFTER_X_RUNS)
            {
                mute = true;
            }
            else
            {
                processRan++;
            }

            Process process = new Process();
            if (OperatingSystem.IsWindows())
            {
                process.StartInfo.FileName = "RelativelyPrime/Win/RelativelyPrime.exe";
            }
            else if(OperatingSystem.IsLinux())
            {
                process.StartInfo.FileName = "RelativelyPrime/Linux/RelativelyPrime";
            }
            else if(OperatingSystem.IsMacOS())
            {
                process.StartInfo.FileName = "RelativelyPrime/macOS/RelativelyPrime";
                process.StartInfo.UseShellExecute = true;
            }
            process.StartInfo.Arguments = val1 + " " + val2;
            process.Start();
            process.WaitForExit();

            if (process.ExitCode < 0 || process.ExitCode > 10)//Backwards compatible change. Delete negatives next semester
            {
                Program.RecieveResult(process.ExitCode);
                Console.WriteLine("Relatively Prime application crashed!");
                return ProcessResult.Crashed;
            }

            if (mute == false)
            {
                Console.WriteLine("Relatively Prime application executed without issue. Comnination was reported as " + ((process.ExitCode == 1) ? " coprime." : "not coprime."));
            }

            return (ProcessResult)process.ExitCode;
        }

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
