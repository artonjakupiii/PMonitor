using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ProcessMonitor
{
  public class Program
  {
    public static void Main(string[] args)
    {
      if (args.Length != 3)
      {
        Console.WriteLine("Usage: ProcessMonitor.exe <process name> <maximum run time (minutes)> <check interval (minutes)>");
        return;
      }

      try
      {
        string logFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
        string logFilePath = Path.Combine(Environment.CurrentDirectory, logFileName);

        using (StreamWriter writer = File.AppendText(logFilePath))
        {
          string processName = args[0];
          int maxRunTime = int.Parse(args[1]);
          int checkInterval = int.Parse(args[2]);

          if (string.IsNullOrEmpty(processName))
          {
            Console.WriteLine("The process name cannot be empty.");
            return;
          }

          while (true)
          {
            // Get all processes with the specified name
            Process[] processes = Process.GetProcessesByName(processName);

            // Kill any processes that have been running longer than the specified time
            foreach (Process process in processes)
            {
              if ((DateTime.Now - process.StartTime).TotalMinutes > maxRunTime)
              {
                writer.WriteLine("Killing process {0} (PID {1}) because it has been running for longer than {2} minutes", process.ProcessName, process.Id, maxRunTime);
                process.Kill();
              }
            }

            // Wait for the specified interval before checking again
            for (int i = 0; i < checkInterval * 60; i++)
            {
              if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
              {
                return;
              }

              Thread.Sleep(1000);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
