using RelativelyPrime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelativelyPrimeTester.Program;

namespace RelativelyPrimeTester;

public static class Process
{
    //IO is VERY slow, so it's worth muting after enough runs, especially for the exhaustive check
    public const int MUTE_OUTPUT_AFTER_N_RUNS = 20;
    private static int processRan = 0;//Increases every time RunProcess is called

    // You may read the code below, but it will not
    //  provide additional insight for the assignment.

    public static Result Run(params int?[] nums)
    {
        StringBuilder sb = new StringBuilder(String.Empty);

        foreach (int? n in nums)
        {
            if (n == null)
            {
                sb.Append(String.Empty);
            }
            else
            {
                sb.Append(n.Value.ToString() + " ");
            }
        }

        return Run(sb.ToString().Trim());
    }

    public static Result Run(String? val)
    {
        List<String> args = [];

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

        bool mute = processRan >= Process.MUTE_OUTPUT_AFTER_N_RUNS;

        if(processRan == Process.MUTE_OUTPUT_AFTER_N_RUNS)
        {
            Console.WriteLine("Muting output to reduce IO cost...");
            processRan++;
        }

        if (processRan < Process.MUTE_OUTPUT_AFTER_N_RUNS)
        {
            processRan++;
        }

        ProcessResult result;
        int? errorCode = null;

        if (OperatingSystem.IsMacOS())//MacOS hosts only
        {
            int returnCode = g.MainEmu(args.ToArray());

            if (returnCode > 10)//Crashed
            {
                errorCode = returnCode - 10;
                Program.ReceiveResult(returnCode);
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

            if (process.ExitCode > 10)
            {
                errorCode = process.ExitCode - 10;
                Program.ReceiveResult(process.ExitCode);
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

        return new Result(result, errorCode);
    }
}
