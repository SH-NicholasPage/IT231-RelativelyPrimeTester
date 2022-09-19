/*
 * You can add other methods to this file if you would like, but do not modify existing methods.
 */

using RelativelyPrime;
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
                return RunProcess(val, null);
            }
        }

        public static ProcessResult RunProcess(String? val1 = null, String? val2 = null)
        {
            int num1 = int.MinValue;
            int num2 = int.MinValue;

            int.TryParse(val1, out num1);
            int.TryParse(val2, out num2);

            if (val1 != null && val2 != null && num1 != int.MinValue && num2 != int.MinValue)
            {
                Program.NumChecked(num1, num2);
            }

            bool mute = (processRan >= MUTE_OUTPUT_AFTER_X_RUNS) ? true : false;

            if (processRan < MUTE_OUTPUT_AFTER_X_RUNS)
            {
                processRan++;
            }

            ProcessResult result;

            if (OperatingSystem.IsMacOS())//MacOS hosts only
            {
                int returnCode = g.MainEmu(new String[] { val1!, val2! });

                if (returnCode > 10)//Crashed
                {
                    Program.RecieveResult(returnCode);
                    if (mute == false)
                    {
                        Console.WriteLine("Relatively Prime application crashed!");
                    }
                    result = ProcessResult.Crashed;
                }
                else
                {
                    result = (ProcessResult)returnCode;
                }
            }
            else//Windows and Linux OS
            {
                Process process = new Process();
                if (OperatingSystem.IsWindows())
                {
                    process.StartInfo.FileName = "RelativelyPrime/Win/RelativelyPrime.exe";
                }
                else if (OperatingSystem.IsLinux())
                {
                    process.StartInfo.FileName = "RelativelyPrime/Linux/RelativelyPrime";
                }

                process.StartInfo.Arguments = val1 + " " + val2;
                process.Start();
                process.WaitForExit();

                if (process.ExitCode < 0 || process.ExitCode > 10)//Backwards compatible change. Delete negatives next semester//Crashed
                {
                    Program.RecieveResult(process.ExitCode);
                    if (mute == false)
                    {
                        Console.WriteLine("Relatively Prime application crashed!");
                    }
                    result = ProcessResult.Crashed;
                }
                else
                {
                    result = (ProcessResult)process.ExitCode;
                }
            }

            if (result != ProcessResult.Crashed && mute == false)
            {
                Console.WriteLine("Relatively Prime application executed without issue. Comnination was reported as " + ((result == ProcessResult.CoPrime) ? " coprime." : "not coprime."));
            }

            return result;
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
