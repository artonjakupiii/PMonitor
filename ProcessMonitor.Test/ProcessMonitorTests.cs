using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace ProcessMonitor.Test
{
  [TestFixture]
  public class ProcessMonitorTests
  {
    [Test]
    public void TestProcessMonitorWithValidArgs()
    {
      // Arrange
      string processName = "notepad";
      int maxRunTime = 1;
      int checkInterval = 1;

      // Act
      using (Process process = Process.Start(processName))
      {
        // Start a new task to run the ProcessMonitor program
        Task task = Task.Run(() => ProcessMonitor.Program.Main(new string[] { processName, maxRunTime.ToString(), checkInterval.ToString() }));

        // Wait before killing the program
        System.Threading.Thread.Sleep((maxRunTime * 60 * 1000) + 5000);
      }

      // Assert
      // The process should have been killed by the monitor
      Process[] processes = Process.GetProcessesByName(processName);
      Assert.AreEqual(0, processes.Length);
    }

    [Test]
    public void TestProcessMonitorWithNoArgs()
    {
      // Arrange
      string[] args = new string[] { };

      // Act
      var stringWriter = new StringWriter();
      Console.SetOut(stringWriter);
      ProcessMonitor.Program.Main(args);

      // Assert
      // The output should contain the usage message
      Assert.That(stringWriter.ToString(), Does.Contain("Usage: ProcessMonitor.exe <process name> <maximum run time (minutes)> <check interval (minutes)>"));
    }

    [Test]
    public void TestProcessMonitorWithInvalidMaxRunTimeArg()
    {
      // Arrange
      string processName = "notepad";
      string maxRunTime = "invalid";
      int checkInterval = 1;

      // Act
      var stringWriter = new StringWriter();
      Console.SetOut(stringWriter);
      ProcessMonitor.Program.Main(new string[] { processName, maxRunTime, checkInterval.ToString() });

      // Assert
      // The output should contain an error message about the invalid input
      Assert.That(stringWriter.ToString(), Does.Contain("Input string was not in a correct format."));
    }

    [Test]
    public void TestProcessMonitorWithInvalidCheckIntervalArg()
    {
      // Arrange
      string processName = "notepad";
      int maxRunTime = 5;
      string checkInterval = "invalid";

      // Act
      var stringWriter = new StringWriter();
      Console.SetOut(stringWriter);
      ProcessMonitor.Program.Main(new string[] { processName, maxRunTime.ToString(), checkInterval });

      // Assert
      // The output should contain an error message about the invalid input
      Assert.That(stringWriter.ToString(), Does.Contain("Input string was not in a correct format."));
    }

    [Test]
    public void TestProcessMonitorWithInvalidProcessNameArg()
    {
      // Arrange
      string processName = "";
      int maxRunTime = 5;
      int checkInterval = 1;

      // Act
      var stringWriter = new StringWriter();
      Console.SetOut(stringWriter);
      ProcessMonitor.Program.Main(new string[] { processName, maxRunTime.ToString(), checkInterval.ToString() });

      // Assert
      // The output should contain an error message about the empty process name
      Assert.That(stringWriter.ToString(), Does.Contain("The process name cannot be empty."));
    }
  }
}
