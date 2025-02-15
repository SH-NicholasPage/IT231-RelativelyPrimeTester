using RelativelyPrime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelativelyPrimeTester.Program;

namespace RelativelyPrimeTester
{
    public class Process
    {
        //IO is VERY slow, so it's worth muting after enough runs, especially for the exhaustive check
        public const int MUTE_OUTPUT_AFTER_X_RUNS = 20;
        private static int processRan = 0;//Increases every time RunProcess is called


        public static ProcessResult Run()
        {
            return Run(null);
        }

        public static ProcessResult Run(int num1, int? num2 = null)
        {
            if (num2 == null)
            {
                return Run(num1.ToString(), null);
            }
            else
            {
                return Run(num1.ToString(), num2.ToString());
            }
        }

        public static ProcessResult Run(String? val1 = null, String? val2 = null)
        {
            if(val1 == null && val2 == null)
            {
                return Run(null);
            }

            StringBuilder sb = new StringBuilder();
            if(val1 != null)
            {
                sb.Append(val1);
            }

            if(val1 != null && val2 != null)
            {
                sb.Append(' ');
            }

            if(val2 != null)
            {
                sb.Append(val2);
            }

            return Run(sb.ToString());
        }

        public static ProcessResult Run(String? val)
        {
            List<String> args = new List<String>();

            if (val != null)
            {
                if (val.Split(" ").Length >= 2)
                {
                    args.AddRange(val.Split(" ").Select(x => x.Trim()).ToList());
                }
                else
                {
                    args.Add(val);
                }
            }

            if (args.Count == 2)
            {
                int num1 = int.MinValue;
                int num2 = int.MinValue;

                int.TryParse(args[0], out num1);
                int.TryParse(args[1], out num2);

                if (num1 != int.MinValue && num2 != int.MinValue)
                {
                    Program.NumChecked(num1, num2);
                }
            }

            bool mute = (processRan >= Process.MUTE_OUTPUT_AFTER_X_RUNS) ? true : false;

            if(processRan == Process.MUTE_OUTPUT_AFTER_X_RUNS)
            {
                Console.WriteLine("Muting output to reduce IO cost...");
                processRan++;
            }

            if (processRan < Process.MUTE_OUTPUT_AFTER_X_RUNS)
            {
                processRan++;
            }

            ProcessResult result;

            if (OperatingSystem.IsMacOS())//MacOS hosts only
            {
                int returnCode = g.MainEmu(args.ToArray());

                if (returnCode > 10)//Crashed
                {
                    Program.RecieveResult(returnCode);
                    if (mute == false)
                    {
                        Console.WriteLine("Relatively Prime application crashed! Exit code " + returnCode);
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
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                if (OperatingSystem.IsWindows())
                {
                    process.StartInfo.FileName = "RelativelyPrime/Win/RelativelyPrime.exe";
                }
                else if (OperatingSystem.IsLinux())
                {
                    process.StartInfo.FileName = "RelativelyPrime/Linux/RelativelyPrime";
                }

                process.StartInfo.Arguments = String.Join(" ", args);
                process.Start();
                process.WaitForExit();

                if (process.ExitCode < 0 || process.ExitCode > 10)//Backwards compatible change. Delete negatives next semester//Crashed
                {
                    Program.RecieveResult(process.ExitCode);
                    if (mute == false)
                    {
                        Console.WriteLine("Relatively Prime application crashed! Exit code " + process.ExitCode);
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
                Console.WriteLine("Relatively Prime application executed without issue. Comnination was reported as " + ((result == ProcessResult.CoPrime) ? "coprime." : "not coprime."));
            }

            return result;
        }
    }
}
